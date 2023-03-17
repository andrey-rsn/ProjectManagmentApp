import { createSlice } from "@reduxjs/toolkit";

const initialState ={
    projectId: null,
    name:null,
    description:null
}

export const projectsSlice = createSlice({
    name: 'projects',
    initialState,
    reducers: {
        setProjectInfo: (state, action) => {
            state = action.payload;
            console.log(state);
        }
    }
})

export const { setProjectInfo } = projectsSlice.actions;

export default projectsSlice.reducer;