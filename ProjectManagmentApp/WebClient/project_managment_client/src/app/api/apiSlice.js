import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';
import { setCredentials,logOut } from '../../features/auth/authSlice';
import Cookies from 'universal-cookie';

const cookies = new Cookies();

const baseQuery = fetchBaseQuery({
    baseUrl: 'http://localhost:1000',
    setCredentials: 'include',
    prepareHeaders: (headers, {getState}) => {

        const token = cookies.get('access_token');

        if(token){
            headers.set('Authorization', `Bearer ${token}`);
        }

        return headers;
    }
})

const baseQueryWithReauth = async (args, api, extraOptions) =>{

    let result = await baseQuery(args, api, extraOptions);

    if(result?.error?.status === 401){
        const refreshToken = cookies.get('refresh_token');
        const refreshResult = await baseQuery({url:`/api/v1/identity/refresh?RefreshToken=${refreshToken}`, method:'POST'}, api, extraOptions);

        if(refreshResult?.data && refreshResult?.error?.status != 401 ) {

            api.dispatch(setCredentials(refreshResult.data));

            result = await baseQuery(args, api, extraOptions);
        } else {
            api.dispatch(logOut());
        }
    }

    return result;
}

export const apiSlice = createApi({
    baseQuery: baseQueryWithReauth,
    endpoints: builder => ({})
})