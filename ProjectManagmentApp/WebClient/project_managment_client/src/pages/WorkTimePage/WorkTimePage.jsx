import * as React from 'react';
import CircularProgress from '@mui/material/CircularProgress';
import Button from '@mui/material/Button';
import { useState } from 'react';
import './WorkTimePage.css';



const WorkTimePage = () => {

	const [isActive, setIsActive] = useState(false);

	const onBtnToggle = () => {
		setIsActive(!isActive);
	}

	return (
		<div className='work-time-page'>
			<div className='work-time-form'>
				<div className='work-time-form__header'>
					<CircularProgress variant={isActive ? "indeterminate" : "determinate"} value={100} />
				</div>
				<div className='work-time-form__middle middle'>
					<div className='middle__time-start'>
						<div className='left-wrapper'>
							<p>Время начала работы</p>
						</div>
						<div className='right-wrapper'>
							<p>20:20</p>
						</div>
					</div>
					<div className='middle__time-end'>
						<div className='left-wrapper'>
							<p>Время завершения работы</p>
						</div>
						<div className='right-wrapper'>
							<p>22:20</p>
						</div>
					</div>
				</div>
				<div className='work-time-form__bottom'>
					<Button variant="contained" color={isActive ? 'error' : 'success'} onClick={onBtnToggle}>{isActive ? 'Завершить работу' : 'Начать работу'}</Button>
				</div>
			</div>
		</div>
	)
}

export default WorkTimePage;