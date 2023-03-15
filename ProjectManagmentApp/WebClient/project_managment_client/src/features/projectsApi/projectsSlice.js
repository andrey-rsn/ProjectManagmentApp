import { createSlice } from "@reduxjs/toolkit";

const initialState ={}

export const projectsSlice = createSlice({
    name: 'projects',
    initialState,
    reducers: {
        setProjectInfo: (state, action) => {
            state = action.payload;
        }
    }
})

export const { setProjectInfo } = projectsSlice.actions;

export default projectsSlice.reducer;