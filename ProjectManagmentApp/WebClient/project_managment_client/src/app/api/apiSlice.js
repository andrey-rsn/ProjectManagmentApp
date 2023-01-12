import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';
import { setCredentials,logOut } from '../../features/auth/authSlice';


const baseQuery = fetchBaseQuery({
    baseUrl: 'http://localhost:5069',
    setCredentials: 'include',
    prepareHeaders: (headers, {getState}) => {
        const token = getState().auth.token
        if(token){
            headers.set('Authorization', `Bearer ${token}`);
        }
        return headers;
    }
})

const baseQueryWithReauth = async (args, api, extraOptions) =>{
    let result = await baseQuery(args, api, extraOptions);
    if(result?.error?.originalStatus === 403){
        console.log('sending refresh token');

        const refreshResult = await baseQuery('/api/v1/identity/refresh', api, extraOptions);

        if(refreshResult?.data) {
            const user = api.getState().auth.user;

            api.dispatch(setCredentials(...refreshResult.data, user));

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