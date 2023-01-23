import { createSlice } from "@reduxjs/toolkit";
import Cookies from 'universal-cookie';

const cookies = new Cookies();

const authSlice = createSlice({
    name: 'auth',
    initialState: {user: null, token: null},
    reducers: {
        setCredentials: (state, action) => {
            const { user_name, access_token, refresh_token ,user_id } = action.payload;

            let expires = new Date();
            expires.setTime(expires.getTime() + (15 * 1000000));

            cookies.set('access_token', access_token, expires);
            cookies.set('refresh_token', refresh_token, expires);
            cookies.set('user_id', user_id, expires);
            cookies.set('user_name', user_name, expires);
        },
        logOut: (state,action) => {
            cookies.remove('access_token');
            cookies.remove('refresh_token');
            cookies.remove('user_id');
            cookies.remove('user_name');
        }
    }
})

export const {setCredentials, logOut} = authSlice.actions;

export default authSlice.reducer;

export const selectCurrentUser = () => cookies.get('user_name');
export const selectCurrentToken = () => cookies.get('access_token');
export const selectCurrentRefreshToken = () => cookies.get('refresh_token');
export const selectCurrentUserId = () => cookies.get('user_id');