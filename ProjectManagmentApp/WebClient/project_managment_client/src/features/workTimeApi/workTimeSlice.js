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

            state.startTime = startTime;
            state.endTime = null;
            state.isStarted = true;
        },
        endWork: (state,action) => {
            const {endTime} = action.payload;

            state.endTime = endTime;
            state.isStarted = false;
        },
        updateInfo: (state,action) => {
            const {startTime, endTime, isStarted} = action.payload;

            state.startTime = startTime;
            state.endTime = endTime;
            state.isStarted = isStarted;
        }
    }      
})


export const {startWork, endWork, updateInfo} = workTimeSlice.actions;

export default workTimeSlice.reducer;