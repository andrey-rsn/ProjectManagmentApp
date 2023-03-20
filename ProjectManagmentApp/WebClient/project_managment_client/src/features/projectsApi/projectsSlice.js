import { createSlice } from "@reduxjs/toolkit";

const initialState ={
    projectId: localStorage.getItem('currentProjectId'),
    name: localStorage.getItem('currentProjectName'),
    description: localStorage.getItem('currentProjectDescription')
}

export const projectsSlice = createSlice({
    name: 'projects',
    initialState,
    reducers: {
        setProjectInfo: (state, action) => {
            const {projectId, name, description} = action.payload;

            localStorage.removeItem('currentProjectId');
            localStorage.removeItem('currentProjectName');
            localStorage.removeItem('currentProjectDescription');

            localStorage.setItem('currentProjectId', projectId) ;
            localStorage.setItem('currentProjectName', name);
            localStorage.setItem('currentProjectDescription', description);

            state.projectId = localStorage.getItem('currentProjectId');
            state.name = localStorage.getItem('currentProjectName');
            state.description = localStorage.getItem('currentProjectDescription');
        }
    }
})

export const { setProjectInfo } = projectsSlice.actions;

export default projectsSlice.reducer;