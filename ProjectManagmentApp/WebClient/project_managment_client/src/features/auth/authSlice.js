import { createSlice } from "@reduxjs/toolkit";

const authSlice = createSlice({
    name: 'auth',
    initialState: {user: null, token: null},
    reducers: {
        setCredentials: (state, action) => {
            console.log(action);
            const { user_name, access_token } = action.payload;
            state.user = user_name;
            state.token = access_token;
        },
        logOut: (state, action) => {
            state.user = null;
            state.token = null;
        }
    }
})

export const {setCredentials, logOut} = authSlice.actions;

export default authSlice.reducer;

export const selectCurrentUser = (state) => state.auth.user;
export const selectCurrentToken = (state) => state.auth.token;