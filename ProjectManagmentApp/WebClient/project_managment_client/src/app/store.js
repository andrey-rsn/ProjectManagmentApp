import { configureStore } from "@reduxjs/toolkit";
import { apiSlice } from "./api/apiSlice";
import authReducer from '../features/auth/authSlice';
import workTimeReducer from '../features/workTimeApi/workTimeSlice';
import tasksReducer from '../features/tasksApi/tasksSlice';
import projectsReducer from '../features/projectsApi/projectsSlice';
import { workTimeApiSlice } from "../features/workTimeApi/workTimeApiSlice";


export const store = configureStore({
    reducer: {
        [apiSlice.reducerPath]: apiSlice.reducer,
        auth: authReducer,
        [workTimeApiSlice.reducerPath]: workTimeApiSlice.reducer,
        workTime: workTimeReducer,
        tasks: tasksReducer,
        projects: projectsReducer
    },
    middleware: getDefaultMiddleware =>
        getDefaultMiddleware().concat(apiSlice.middleware),
        devTools: true
})
