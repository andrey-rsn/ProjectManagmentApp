import './TaskCardForm.css';
import AccountBoxIcon from '@mui/icons-material/AccountBox';
import Divider from '@mui/material/Divider';
import Button from '@mui/material/Button';
import SaveIcon from '@mui/icons-material/Save';
import CircleIcon from '@mui/icons-material/Circle';
import * as React from 'react';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import TextField from '@mui/material/TextField';
import { useSelector } from 'react-redux';
import CommentElement from '../CommentElement/CommentElement';
import RefreshIcon from '@mui/icons-material/Refresh';
import { selectCurrentUserId, selectCurrentUserName } from '../../features/auth/authSlice';
import { useState } from 'react';
import { useEffect } from 'react';
import { useLazyGetTaskByIdQuery, useUpdateTaskMutation } from '../../features/tasksApi/tasksApiSlice';
import { formatTime } from '../../helpers/timeHelper/timeHelper';
import CircularProgress from '@mui/material/CircularProgress';
import OutlinedInput from '@mui/material/OutlinedInput';


const TaskCardForm = (props) => {
    const { taskId } = props;

    const [taskInfo, setTaskInfo] = useState({});
    const [isChanged, setIsChanged] = useState(false);
    const [taskName, setTaskName] = useState("");
    const [taskByIdFetch, { isLoading, error }] = useLazyGetTaskByIdQuery();
    const [UpdateTaskFetch, { updateTaskIsLoading }] = useUpdateTaskMutation();


    const userName = useSelector(selectCurrentUserName);
    const userId = useSelector(selectCurrentUserId);

    useEffect(() => {
        loadData();
    }, []);

    async function loadData() {

        await taskByIdFetch(taskId).unwrap().then(value => {
            setTaskInfo(value);
            setTaskName(value.name);
        }).catch(err => console.log(err));

    }

    async function saveData(data) {
        await UpdateTaskFetch(data).unwrap().catch(err => console.log(err));
    }

    const [commentText, setCommentText] = useState("");

    const { id, img, name, assignedTo, priority, statusId, description, comments, changedBy, changeDate } = taskInfo;

    const CommentsElements = React.useMemo(() => {
        if (comments?.length > 0) {
            return comments.map((value, index) => <CommentElement key={index} name={value.author} img={value.img} creationDate={formatTime(value.creationDate)} text={value.commentText} />)
        }
    }, [comments]);

    const handleStatusChange = (e) => {
        setIsChanged(true);

        setTaskInfo({
            ...taskInfo,
            statusId: e.target.value
        })
    }

    const onTaskInfoSave = async () => {

        if (!isChanged) {
            return;
        }

        const taskInfoToSave = { ...taskInfo };
        taskInfoToSave.comments = [];
        taskInfoToSave.name = taskName;

        if (commentText) {
            const commentInfo = {
                authorId: userId, creationDate: new Date().toISOString(), commentText, associatedTaskId: taskId
            }

            taskInfoToSave.comments.push(commentInfo);

        }
        await saveData(taskInfoToSave);
        await loadData();
        clearCommentText();
    };

    const onTaskInfoUpdate = async () => {
        console.log(taskInfo.statusId);
        await loadData();
    }

    const onTaskNameChange = (e) => {
        setTaskName(e.target.value);
    }

    const commentTextChangeHandle = (e) => {
        setIsChanged(true);
        setCommentText(e.target.value);
    }

    const clearCommentText = () => {
        setCommentText("");
    }

    const descriptionChangeHandle = (e) => {
        setIsChanged(true);

        setTaskInfo({
            ...taskInfo,
            description: e.target.value
        });
    }

    const statusColor = React.useMemo(() => {
        switch (statusId) {
            case 1: return 'disabled';
            case 2: return 'info';
            case 3: return 'warning';
            case 4: return 'success';
            default: return 'disabled'
        }
    }, [taskInfo.statusId]);

    const content = isLoading
        ? <div><CircularProgress /></div>
        : <div className="task-card-form">
            <div className="task-card-form__header header">
                <div className="header__main-info main-info">
                    <p className='main-info__id'>{id}</p>
                    <OutlinedInput placeholder="Наименование задачи" sx={{width:'80%', height:'35px'}} value={taskName} onChange={(e) => onTaskNameChange(e)}/>
                </div>
                <div className="header__additional-info additional-info">
                    <div className='additional-info__name'>
                        <AccountBoxIcon sx={{ height: '100%', color: 'gray', marginRight: '5px' }} />
                        <p>{assignedTo}</p>
                    </div>
                    <div className='additional-info__tools'>
                        <Button size="small" sx={{ color: 'black' }} onClick={() => onTaskInfoSave()} ><SaveIcon sx={{ height: '100%' }} />Сохранить</Button>
                        <Button size="small" sx={{ color: 'black' }} onClick={() => onTaskInfoUpdate()} ><RefreshIcon sx={{ height: '100%' }} />Обновить</Button>
                    </div>
                </div>
            </div>
            <Divider sx={{ backgroundColor: 'grey', marginRight: '-1%', marginLeft: '-1%' }} />
            <div className="task-card-form__secondary-info secondary-info">
                <div className='left-side'>
                    <div className='secondary-info__state'>
                        <p>Состояние:</p>
                        <CircleIcon color={statusColor} sx={{ fontSize: '1.2em', height: '100%', marginRight: '0px !important' }} />
                        <FormControl sx={{ m: 1, minWidth: 160, lineHeight: '50%' }} size="small">
                            <InputLabel id="demo-select-small"></InputLabel>
                            <Select
                                labelId="demo-select-small"
                                id="demo-select-small"
                                value={`${statusId}`}
                                onChange={handleStatusChange}
                            >
                                <MenuItem value={1}>Новая</MenuItem>
                                <MenuItem value={2}>В работе</MenuItem>
                                <MenuItem value={3}>Выполнена</MenuItem>
                                <MenuItem value={4}>Завершена</MenuItem>
                            </Select>
                        </FormControl>
                    </div>
                    <div className='secondary-info__priority'>
                        <p>Приоритет:</p>
                        <p>{priority}</p>
                    </div>
                </div>
                <div className='right-side'>
                    <div className='secondary-info__updated-by'>
                        <p>Кем обновлено:</p>
                        <p>{changedBy} {formatTime(changeDate)}</p>
                    </div>
                </div>
            </div>
            <Divider sx={{ backgroundColor: 'grey', marginRight: '-1%', marginLeft: '-1%' }} variant='fullWidth' />
            <div className="task-card-form__main-info main-info">
                <div className='main-info__description'>
                    <p>Описание</p>
                    <TextField
                        id="filled-multiline-static"
                        multiline
                        rows={8}
                        placeholder="Введите описание задачи"
                        variant="filled"
                        sx={{ width: '70%' }}
                        defaultValue={description}
                        onChange={descriptionChangeHandle}
                    />
                </div>
                <div className='main-info__comments comments'>
                    <div className='comments__input'>
                        <p>Комментарии</p>
                        <TextField
                            id="filled-multiline-static"
                            multiline
                            rows={1}
                            placeholder="Введите текст комментария"
                            variant="filled"
                            sx={{ width: '70%' }}
                            defaultValue=''
                            onChange={commentTextChangeHandle}
                        />
                    </div>
                    <div className='comments__list'>
                        {CommentsElements}
                    </div>
                </div>
            </div>
        </div>

    return (
        <>
            {content}
        </>
    )
}




export default TaskCardForm;