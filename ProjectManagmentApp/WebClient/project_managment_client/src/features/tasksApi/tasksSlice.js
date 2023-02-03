import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    data: [
        { id: 1,img:null, name: 'Snow', status: 'Jon', changeDate: 35, assignedTo: 'Admin', assignedUserId: 1, priority: 3, statusId: 1, description: 'sdfwtegwteywet', comments: [{ img: null, author: 'Admin admin', creationDate: '27.05.2023', text: 'lsjdfhjhskgdfkhsgdfjkhgaskjhgfkjshadgfkjhsagdjkfhgsajdfgjksahgdfjgsakjdgfjkshagdfkjgsakjdfgsakjdhgfjhsagdkfjhgaskdhjfgaksjdhgf' }, { img: null, author: 'Admin admin', creationDate: '27.05.2023', text: 'lsjdfhjhskgdfkhsgdfjkhgaskjhgfkjshadgfkjhsagdjkfhgsajdfgjksahgdfjgsakjdgfjkshagdfkjgsakjdfgsakjdhgfjhsagdkfjhgaskdhjfgaksjdhgf' }], changedBy: 'Andrey' },
        { id: 2,img:null, name: 'Lannister', status: 'Cersei', changeDate: 42, assignedTo: 'Admin', assignedUserId: 1, priority: 3, statusId: 2, description: 'sdfwtegwteywet' },
        { id: 3,img:null, name: 'Lannister', status: 'Jaime', changeDate: 45, assignedTo: 'Admin', assignedUserId: 1, priority: 3, statusId: 3, description: 'sdfwtegwteywet' },
        { id: 4,img:null, name: 'Stark', status: 'Arya', changeDate: 16, assignedTo: 'Admin', assignedUserId: 1, priority: 3, statusId: 4, description: 'sdfwtegwteywet' },
        { id: 5,img:null, name: 'Targaryen', status: 'Daenerys', changeDate: null, assignedTo: 'Admin', assignedUserId: 1, priority: 3, statusId: 1, description: 'sdfwtegwteywet' },
        { id: 6,img:null, name: 'Melisandre', status: null, changeDate: 150, assignedTo: 'Admin', assignedUserId: 1, priority: 3, statusId: 1, description: 'sdfwtegwteywet' },
        { id: 7,img:null, name: 'Clifford', status: 'Ferrara', changeDate: 44, assignedTo: 'Admin', assignedUserId: 1, priority: 3, statusId: 1, description: 'sdfwtegwteywet' },
        { id: 8,img:null, name: 'Frances', status: 'Rossini', changeDate: 36, assignedTo: 'Admin', assignedUserId: 1, priority: 3, statusId: 1, description: 'sdfwtegwteywet' },
        { id: 9,img:null, name: 'Roxie', status: 'Harvey', changeDate: 65, assignedTo: 'Admin', assignedUserId: 1, priority: 3, statusId: 1, description: 'sdfwtegwteywet' }
    ]
}

export const tasksSlice = createSlice({
    name: 'tasks',
    initialState,
    reducers: {
        updateData: (state, action) => {

            const { data } = action.payload;

            state.data = [...data];
        },
        updateElement: (state, action) => {

            const element = action.payload;

            const index = state.data.indexOf(state.data.find(value => value.id == element.id));

            console.log(state.data[index].name);
            console.log(element);

            state.data[index] = element;
        }
    }
})

export const { updateData, updateElement } = tasksSlice.actions;

export default tasksSlice.reducer;