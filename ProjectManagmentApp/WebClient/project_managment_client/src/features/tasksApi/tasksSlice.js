import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    data: [
        { id: 1, name: 'Snow', status: 'Jon', changeDate: 35, assignedTo: 'Admin', assignedUserId: 1, priority: 3, statusId: 1, description: 'sdfwtegwteywet', comments: [{ img: null, name: 'Admin admin', creationDate: '27.05.2023', text: 'lsjdfhjhskgdfkhsgdfjkhgaskjhgfkjshadgfkjhsagdjkfhgsajdfgjksahgdfjgsakjdgfjkshagdfkjgsakjdfgsakjdhgfjhsagdkfjhgaskdhjfgaksjdhgf' }, { img: null, name: 'Admin admin', creationDate: '27.05.2023', text: 'lsjdfhjhskgdfkhsgdfjkhgaskjhgfkjshadgfkjhsagdjkfhgsajdfgjksahgdfjgsakjdgfjkshagdfkjgsakjdfgsakjdhgfjhsagdkfjhgaskdhjfgaksjdhgf' }], changedBy: 'Andrey' },
        { id: 2, name: 'Lannister', status: 'Cersei', changeDate: 42, assignedTo: 'Admin', assignedUserId: 1, priority: 3, statusId: 2, description: 'sdfwtegwteywet' },
        { id: 3, name: 'Lannister', status: 'Jaime', changeDate: 45, assignedTo: 'Admin', assignedUserId: 1, priority: 3, statusId: 3, description: 'sdfwtegwteywet' },
        { id: 4, name: 'Stark', status: 'Arya', changeDate: 16, assignedTo: 'Admin', assignedUserId: 1, priority: 3, statusId: 4, description: 'sdfwtegwteywet' },
        { id: 5, name: 'Targaryen', status: 'Daenerys', changeDate: null, assignedTo: 'Admin', assignedUserId: 1, priority: 3, statusId: 1, description: 'sdfwtegwteywet' },
        { id: 6, name: 'Melisandre', status: null, changeDate: 150, assignedTo: 'Admin', assignedUserId: 1, priority: 3, statusId: 1, description: 'sdfwtegwteywet' },
        { id: 7, name: 'Clifford', status: 'Ferrara', changeDate: 44, assignedTo: 'Admin', assignedUserId: 1, priority: 3, statusId: 1, description: 'sdfwtegwteywet' },
        { id: 8, name: 'Frances', status: 'Rossini', changeDate: 36, assignedTo: 'Admin', assignedUserId: 1, priority: 3, statusId: 1, description: 'sdfwtegwteywet' },
        { id: 9, name: 'Roxie', status: 'Harvey', changeDate: 65, assignedTo: 'Admin', assignedUserId: 1, priority: 3, statusId: 1, description: 'sdfwtegwteywet' }
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