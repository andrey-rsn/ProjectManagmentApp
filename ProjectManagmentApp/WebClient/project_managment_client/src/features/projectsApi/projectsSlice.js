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
            const {projectId, name, description} = action.payload;
            state.projectId = projectId;
            state.name = name;
            state.description = description;
            console.log(state);
        }
    }
})

export const { setProjectInfo } = projectsSlice.actions;

export default projectsSlice.reducer;