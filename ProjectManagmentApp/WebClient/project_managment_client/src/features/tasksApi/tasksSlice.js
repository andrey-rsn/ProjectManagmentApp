import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    data: [
        { id: 1, name: 'Snow', status: 'Jon', changeDate: 35, assignedTo: 'Admin', assignedUserId: 1 },
        { id: 2, name: 'Lannister', status: 'Cersei', changeDate: 42, assignedTo: 'Admin', assignedUserId: 1 },
        { id: 3, name: 'Lannister', status: 'Jaime', changeDate: 45, assignedTo: 'Admin', assignedUserId: 1 },
        { id: 4, name: 'Stark', status: 'Arya', changeDate: 16, assignedTo: 'Admin', assignedUserId: 1 },
        { id: 5, name: 'Targaryen', status: 'Daenerys', changeDate: null, assignedTo: 'Admin', assignedUserId: 1 },
        { id: 6, name: 'Melisandre', status: null, changeDate: 150, assignedTo: 'Admin', assignedUserId: 1 },
        { id: 7, name: 'Clifford', status: 'Ferrara', changeDate: 44, assignedTo: 'Admin', assignedUserId: 1 },
        { id: 8, name: 'Frances', status: 'Rossini', changeDate: 36, assignedTo: 'Admin', assignedUserId: 1 },
        { id: 9, name: 'Roxie', status: 'Harvey', changeDate: 65, assignedTo: 'Admin', assignedUserId: 1 }
    ]
}

export const tasksSlice = createSlice({
    name: 'tasks',
    initialState,
    reducers: {
        updateData: (state, action) => {

            const { data } = action.payload;

            state.data = [...data];
        }
    }
})

export const { updateData } = tasksSlice.actions;

export default tasksSlice.reducer;