import * as React from 'react';
import CircularProgress from '@mui/material/CircularProgress';
import Button from '@mui/material/Button';
import { useState } from 'react';
import './WorkTimePage.css';
import { useStartWorkMutation } from '../../features/workTimeApi/workTimeApiSlice';
import { useDispatch, useSelector } from 'react-redux';
import {startWork, endWork} from '../../features/workTimeApi/workTimeSlice';
import { selectCurrentUserId } from '../../features/auth/authSlice';



const WorkTimePage = () => {

    const [startWorkFetch,{isLoading}] = useStartWorkMutation();

    const {startTime, endTime, isStarted} = useSelector(state => state.workTime);
    const user_id = useSelector(selectCurrentUserId);

    const dispatch = useDispatch();

	const onBtnToggle = async () => {
        const data = new Date().toLocaleString();
		if(isStarted){
            dispatch(endWork({endTime: data}));
            console.log("endWork");
            console.log(isStarted);
        } else {
            const result = await startWorkFetch(user_id).unwrap().then(value => dispatch(startWork({startTime:data})));
        }
	}

    
	return (
		<div className='work-time-page'>
			<div className='work-time-form'>
				<div className='work-time-form__header'>
					<CircularProgress variant={isStarted ? "indeterminate" : "determinate"} value={100} />
				</div>
				<div className='work-time-form__middle middle'>
					<div className='middle__time-start'>
						<div className='left-wrapper'>
							<p>Время начала работы</p>
						</div>
						<div className='right-wrapper'>
							<p>{startTime}</p>
						</div>
					</div>
					<div className='middle__time-end'>
						<div className='left-wrapper'>
							<p>Время завершения работы</p>
						</div>
						<div className='right-wrapper'>
							<p>{endTime}</p>
						</div>
					</div>
				</div>
				<div className='work-time-form__bottom'>
					<Button variant="contained" color={isStarted ? 'error' : 'success'} onClick={onBtnToggle} disabled = {isLoading}>{isStarted ? 'Завершить работу' : 'Начать работу'}</Button>
				</div>
			</div>
		</div>
	)
}

export default WorkTimePage;