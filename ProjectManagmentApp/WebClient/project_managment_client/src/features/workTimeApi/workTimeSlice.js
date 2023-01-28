import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    startTime: null,
    endTime: null,
    isStarted: false
}

export const workTimeSlice = createSlice({
    name: 'workTime',
    initialState,
    reducers:{
        startWork: (state,action) => {
            const {startTime} = action.payload;
            console.log(action.payload);
            state.startTime = startTime;
            state.isStarted = true;
        },
        endWork: (state,action) => {
            const {endTime} = action.payload;
            console.log(action.payload);
            state.endTime = endTime;
            state.isStarted = false;
        }
    }      
})


export const {startWork, endWork} = workTimeSlice.actions;

export default workTimeSlice.reducer;