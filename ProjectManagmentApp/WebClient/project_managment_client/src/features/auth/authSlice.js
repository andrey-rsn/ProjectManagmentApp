import { createSlice } from "@reduxjs/toolkit";
import Cookies from 'universal-cookie';

const cookies = new Cookies();

const initialState = {
    user_name: null,
    user_id: null
}

const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        setCredentials: (state, action) => {
            const { access_token, refresh_token } = action.payload;

            let expires = new Date();
            expires.setTime(expires.getTime() + (15 * 1000000));

            cookies.remove('access_token', { path: '/' });
            cookies.remove('refresh_token', { path: '/' });
            cookies.set('access_token', access_token, expires);
            cookies.set('refresh_token', refresh_token, expires);
            console.log(JSON.parse(atob(access_token.split('.')[1])))
        },
        logOut: (state,action) => {
            cookies.remove('access_token', { path: '/' });
            cookies.remove('refresh_token', { path: '/' });
        }
    }
})

export const {setCredentials, logOut} = authSlice.actions;

export default authSlice.reducer;

export const selectCurrentUserName = () => JSON.parse(atob(cookies.get('access_token').split('.')[1]))['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
export const selectCurrentToken = () => cookies.get('access_token');
export const selectCurrentRefreshToken = () => cookies.get('refresh_token');
export const selectCurrentUserId = () =>  JSON.parse(atob(cookies.get('access_token').split('.')[1])).UserId;
export const selectCurrentUserRole = () =>  JSON.parse(atob(cookies.get('access_token').split('.')[1])).UserRole;