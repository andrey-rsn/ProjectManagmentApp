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
import { useState, useMemo } from 'react';
import { useEffect } from 'react';
import { useLazyGetTaskByIdQuery, useUpdateTaskMutation, useAddTaskMutation } from '../../features/tasksApi/tasksApiSlice';
import { formatTime } from '../../helpers/timeHelper/timeHelper';
import CircularProgress from '@mui/material/CircularProgress';
import OutlinedInput from '@mui/material/OutlinedInput';
import { useLazyGetEmployeesAttachedToProjectQuery } from '../../features/projectsApi/projectsApiSlice';
import { useParams } from 'react-router-dom';
import { useSnackbar } from 'notistack';
import { useNavigate } from 'react-router-dom';


const TaskCardForm = (props) => {
    const { taskId, isNew } = props;
    const { projectId } = useParams();

    const navigate = useNavigate();

    const { enqueueSnackbar } = useSnackbar();
    const [taskInfo, setTaskInfo] = useState({
        id: 0,
        name:'',
        status:'',
        statusId:0,
        changeDate: '',
        assignedTo:'',
        assignedUserId:0,
        priority:0,
        description:'',
        changedByUserId: 0,
        changedBy:'',
        comments: []
    });

    const [isChanged, setIsChanged] = useState(false);
    const [taskName, setTaskName] = useState("");
    const [priority, setPriority] = useState(0);
    const [attachedEmployees, setAttachedEmployees] = useState([]);
    const [taskByIdFetch, { isLoading: isTasksLoading, isSuccess: isTasksLoadingSuccess, error }] = useLazyGetTaskByIdQuery();
    const [UpdateTaskFetch, { updateTaskIsLoading }] = useUpdateTaskMutation();
    const [attachedEmployeesFetch, { isLoading: isAttachedEmployeesLoading, isSuccess: isAttachedEmployeesSuccess }] = useLazyGetEmployeesAttachedToProjectQuery();
    const [addTaskFetch] = useAddTaskMutation();

    const ITEM_HEIGHT = 48;
    const ITEM_PADDING_TOP = 8;
    const MenuProps = {
        PaperProps: {
            style: {
                maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
                width: 250,
            },
        },
    };

    const userId = useSelector(selectCurrentUserId);

    useEffect(() => {
        loadData();
    }, []);

    async function loadData() {

        await attachedEmployeesFetch({ projectId }).unwrap().then(data => setAttachedEmployees(data)).catch(err => console.log(err));

        if(!isNew){
            await taskByIdFetch(taskId).unwrap().then(value => {
                setTaskInfo(value);
                setTaskName(value.name);
                setPriority(value.priority);
            }).catch(err => console.log(err));
        }

    }

    async function saveData(data) {
        await UpdateTaskFetch(data).unwrap().then(() => handleSuccesTaskUpdate()).catch(err => handleErrorTaskUpdate(err));
    }

    async function addTask(data) {
        await addTaskFetch(data).unwrap().then((data) => handleSuccesTaskCreate(data)).catch(err => handleErrorTaskCreate(err));
    }

    const [commentText, setCommentText] = useState("");

    const { id, img, name, assignedTo, statusId, description, comments, changedBy, changeDate } = taskInfo;

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

    const handleAssignedEmployeeChange = (e) => {
        setIsChanged(true);

        setTaskInfo({
            ...taskInfo,
            assignedUserId: e.target.value
        })
    }

    const onTaskInfoSave = async () => {

        if (!isChanged) {
            return;
        }

        const taskInfoToSave = { ...taskInfo };
        taskInfoToSave.comments = [];
        taskInfoToSave.name = taskName;
        taskInfoToSave.priority = priority;
        taskInfoToSave.changedByUserId = userId;

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

    const onTaskInfoCreate = async () => {

        const taskInfoToSave = { 
            projectId: Number(projectId),
            name: taskName,
            description: taskInfo.description,
            assignedUserId: taskInfo.assignedUserId,
            priority : Number(priority),
            changedByUserId: Number(userId),
            comments: [],
            statusId: taskInfo.statusId
        };

        await addTask(taskInfoToSave);
    };

    const onTaskInfoUpdate = async () => {
        await loadData();
    }

    const onTaskNameChange = (e) => {
        setIsChanged(true);
        setTaskName(e.target.value);
    }

    const onPriorityChange = (e) => {
        setIsChanged(true);
        setPriority(e.target.value);
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

    const handleSuccesTaskUpdate = () => {
        enqueueSnackbar('Параметры задачи сохранены', { variant: 'success' });
    }

    const handleErrorTaskUpdate = (error) => {
        enqueueSnackbar('Ошибка при сохранении параметров задачи', { variant: 'error' });
        console.log(error);
    }

    const handleSuccesTaskCreate = (id) => {
        enqueueSnackbar('Задача создана', { variant: 'success' });
        navigate(`../tasks`);
    }

    const handleErrorTaskCreate = (error) => {
        enqueueSnackbar('Ошибка при создании задачи', { variant: 'error' });
        console.log(error);
    }

    const onSaveClick = async () => {
        if(isNew){
            await onTaskInfoCreate();
        } else {
            await onTaskInfoSave();
        }
    }

    const canSaveOrAdd = useMemo(() => {
        return taskName.length !== 0 && taskInfo.description.length !== 0 && priority > 0 && taskInfo.assignedUserId > 0;
    },[taskName, taskInfo, priority])

    const statusColor = React.useMemo(() => {
        switch (statusId) {
            case 1: return 'disabled';
            case 2: return 'info';
            case 3: return 'warning';
            case 4: return 'success';
            default: return 'disabled'
        }
    }, [taskInfo.statusId]);

    const content = isTasksLoading || (!isTasksLoadingSuccess && !isNew )
        ? <div><CircularProgress /></div>
        : <div className="task-card-form">
            <div className="task-card-form__header header">
                <div className="header__main-info main-info">
                    <p className='main-info__id'>{!isNew ? id : 'id'}</p>
                    <OutlinedInput placeholder="Наименование задачи" sx={{ width: '80%', height: '35px' }} value={taskName} onChange={(e) => onTaskNameChange(e)} />
                </div>
                <div className="header__additional-info additional-info">
                    <div className='additional-info__name'>
                        <AccountBoxIcon sx={{ height: '100%', color: 'gray' }} />
                        <FormControl sx={{ m: 1, minWidth: 400, lineHeight: '50%', textAlign: 'start' }} size="small" disabled={isAttachedEmployeesLoading || !isAttachedEmployeesSuccess}>
                            <InputLabel id="demo-select-small"></InputLabel>
                            <Select
                                labelId="demo-select-small"
                                id="demo-select-small"
                                value={`${taskInfo.assignedUserId}`}
                                MenuProps={MenuProps}
                                onChange={handleAssignedEmployeeChange}
                            >
                                {attachedEmployees.map(value => <MenuItem key={value.user_Id} value={value.user_Id}>{value.fullName}</MenuItem>)}
                            </Select>
                        </FormControl>
                    </div>
                    <div className='additional-info__tools'>
                        <Button size="small" sx={{ color: 'black' }} onClick={() => onSaveClick()} disabled={!canSaveOrAdd}><SaveIcon sx={{ height: '100%' }} />Сохранить</Button>
                        {!isNew ? <Button size="small" sx={{ color: 'black' }} onClick={() => onTaskInfoUpdate()} ><RefreshIcon sx={{ height: '100%' }} />Обновить</Button> : null}
                    </div>
                </div>
            </div>
            <Divider sx={{ backgroundColor: 'grey', marginRight: '-1%', marginLeft: '-1%' }} />
            <div className="task-card-form__secondary-info secondary-info">
                <div className='left-side'>
                    <div className='secondary-info__state'>
                        <p>Состояние:</p>
                        <CircleIcon color={statusColor} sx={{ fontSize: '1.2em', height: '100%', marginRight: '0px !important' }} />
                        <FormControl sx={{ m: 1, minWidth: 132, lineHeight: '50%' }} size="small">
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
                        <OutlinedInput sx={{ width: '15%', height: '30px' }} value={priority} onChange={(e) => onPriorityChange(e)} />
                    </div>
                </div>
                <div className='right-side'>
                    {!isNew ?
                        <div className='secondary-info__updated-by'>
                            <p>Кем обновлено:</p>
                            <p>{changedBy} {formatTime(changeDate)}</p>
                        </div> :
                        null}
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
                {!isNew ?
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
                    </div> :
                    null}
            </div>
        </div>

    return (
        <>
            {content}
        </>
    )
}




export default TaskCardForm;