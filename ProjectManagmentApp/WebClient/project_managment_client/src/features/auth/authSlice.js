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
            const { user_name, access_token, refresh_token ,user_id } = action.payload;
            
            state.user_id = user_id;
            state.user_name = user_name;

            let expires = new Date();
            expires.setTime(expires.getTime() + (15 * 1000000));

            cookies.remove('access_token', { path: '/' });
            cookies.remove('refresh_token', { path: '/' });
            localStorage.removeItem('user_id');
            localStorage.removeItem('user_name');

            cookies.set('access_token', access_token, expires);
            cookies.set('refresh_token', refresh_token, expires);
            localStorage.setItem('user_id',user_id);
            localStorage.setItem('user_name',user_name);
        },
        logOut: (state,action) => {
            state.user_id = null;
            state.user_name = null;
            cookies.remove('access_token', { path: '/' });
            cookies.remove('refresh_token', { path: '/' });
            localStorage.removeItem('user_id');
            localStorage.removeItem('user_name');
        }
    }
})

export const {setCredentials, logOut} = authSlice.actions;

export default authSlice.reducer;

export const selectCurrentUserName = () => localStorage.getItem('user_name');
export const selectCurrentToken = () => cookies.get('access_token');
export const selectCurrentRefreshToken = () => cookies.get('refresh_token');
export const selectCurrentUserId = () =>  localStorage.getItem('user_id');