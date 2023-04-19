import { createSlice } from "@reduxjs/toolkit";
import Cookies from 'universal-cookie';

const cookies = new Cookies();

const initialState = {
    userInfo:{
        firstName:"",
        secondName:"",
        patronymic:""
    }
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
            localStorage.removeItem('firstName');
            localStorage.removeItem('secondName');
            localStorage.removeItem('patronymic');
        },
        setUserInfo: (state, action) => {
            const {firstName, secondName, patronymic} = action.payload;

            localStorage.removeItem('firstName');
            localStorage.removeItem('secondName');
            localStorage.removeItem('patronymic');

            localStorage.setItem('firstName', firstName) ;
            localStorage.setItem('secondName', secondName);
            localStorage.setItem('patronymic', patronymic);

            state.userInfo.firstName = localStorage.getItem('firstName');
            state.userInfo.secondName = localStorage.getItem('secondName');
            state.userInfo.patronymic = localStorage.getItem('patronymic');
        }
    }
})

export const {setCredentials, logOut, setUserInfo} = authSlice.actions;

export default authSlice.reducer;

export const selectCurrentUserName = () => JSON.parse(atob(cookies.get('access_token').split('.')[1]))['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
export const selectCurrentToken = () => cookies.get('access_token');
export const selectCurrentRefreshToken = () => cookies.get('refresh_token');
export const selectCurrentUserId = () =>  JSON.parse(atob(cookies.get('access_token').split('.')[1])).UserId;
export const selectCurrentUserRole = () =>  JSON.parse(atob(cookies.get('access_token').split('.')[1])).UserRole;