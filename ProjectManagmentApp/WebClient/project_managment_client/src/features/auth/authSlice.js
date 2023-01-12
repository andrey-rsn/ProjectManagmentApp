import { createSlice } from "@reduxjs/toolkit";
import Cookies from 'universal-cookie';

const cookies = new Cookies();

const authSlice = createSlice({
    name: 'auth',
    initialState: {user: null, token: null},
    reducers: {
        setCredentials: (state, action) => {
            const { user_name, access_token,user_Id } = action.payload;

            let expires = new Date();
            expires.setTime(expires.getTime() + (15 * 1000));

            cookies.set('access_token', access_token, expires);
            cookies.set('user_id', user_Id, expires);
            cookies.set('user_name', user_name, expires);
        },
        logOut: () => {
            cookies.remove('access_token');
            cookies.remove('user_id');
            cookies.remove('user_name');
        }
    }
})

export const {setCredentials, logOut} = authSlice.actions;

export default authSlice.reducer;

export const selectCurrentUser = (state) => cookies.get('user_name');
export const selectCurrentToken = (state) => cookies.get('access_token');