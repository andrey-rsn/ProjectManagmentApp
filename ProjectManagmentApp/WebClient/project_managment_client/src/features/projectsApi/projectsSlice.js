import { createSlice } from "@reduxjs/toolkit";

const initialState ={}

export const projectsSlice = createSlice({
    name: 'projects',
    initialState,
    reducers: {
        setData: (state, action) => {
            state = action.payload;
        }
    }
})

export const { setData } = projectsSlice.actions;

export default projectsSlice.reducer;