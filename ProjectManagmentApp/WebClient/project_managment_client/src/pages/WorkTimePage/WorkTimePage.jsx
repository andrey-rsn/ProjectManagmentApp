import * as React from 'react';
import CircularProgress from '@mui/material/CircularProgress';
import Button from '@mui/material/Button';
import { useEffect } from 'react';
import './WorkTimePage.css';
import { useStartWorkMutation, useEndWorkMutation, useLazyLastWorkTimeInfoQuery } from '../../features/workTimeApi/workTimeApiSlice';
import { useDispatch, useSelector } from 'react-redux';
import { startWork, endWork, updateInfo } from '../../features/workTimeApi/workTimeSlice';
import { selectCurrentUserId } from '../../features/auth/authSlice';
import Skeleton from '@mui/material/Skeleton';
import { formatTime } from '../../helpers/timeHelper/timeHelper';



const WorkTimePage = () => {

    const user_id = useSelector(selectCurrentUserId);
    const [lastWorkTimeInfoFetch, { isLoading: isLoadingWorkTimeInfo, error }] = useLazyLastWorkTimeInfoQuery();
    const dispatch = useDispatch();

    async function updateData() {
        await lastWorkTimeInfoFetch(user_id).unwrap().then(value => {
            const workTimeInfo = formatWorkTimeInfo(value);
            dispatch(updateInfo(workTimeInfo));
        }).catch(err => {
            if (err.originalStatus === 400) {
                const emptyData = {
                    startTime: '',
                    endTime: '',
                    isStarted: false
                }
                dispatch(updateInfo(emptyData));
            } else {
                console.log(err);
            }
        });
    }
    useEffect(() => {

        updateData();
    }, []);

    const formatWorkTimeInfo = (worktimeInfo) => {
        const { startTime, endTime } = worktimeInfo;

        return {
            ...worktimeInfo,
            startTime: formatTime(startTime),
            endTime: formatTime(endTime)
        }
    }

    const content = (isLoadingWorkTimeInfo
        ? <Skeleton sx={{ bgcolor: 'gray', borderRadius: '10px' }} variant="rectangular" width={400} height={210} />
        : <WorkTimeForm />)

    return (
        <div className='work-time-page'>
            {content}
        </div>
    )

}


const WorkTimeForm = () => {
    const [startWorkFetch, { isStartingProcess }] = useStartWorkMutation();
    const [endWorkFetch, { isEndingProcess }] = useEndWorkMutation();

    const { startTime, endTime, isStarted } = useSelector(state => state.workTime);
    const user_id = useSelector(selectCurrentUserId);

    const dispatch = useDispatch();

    const onBtnToggle = async () => {

        if (isStarted) {
            await endWorkFetch(user_id).unwrap().then(value => {
                const endTime = formatTime(value.endTime);
                dispatch(endWork({ endTime: endTime }))
            });
        } else {
            await startWorkFetch(user_id).unwrap().then(value => {
                const startTime = formatTime(value.startTime);
                dispatch(startWork({ startTime: startTime }))
            });
        }

    }

    return (
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
                <Button variant="contained"
                    color={isStarted ? 'error' : 'success'}
                    onClick={onBtnToggle}
                    disabled={isStartingProcess || isEndingProcess}>
                    {isStarted ? 'Завершить работу' : 'Начать работу'}
                </Button>
            </div>
        </div>
    )
}

export default WorkTimePage;