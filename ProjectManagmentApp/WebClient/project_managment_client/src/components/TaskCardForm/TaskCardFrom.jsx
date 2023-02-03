import './TaskCardForm.css';
import AccountBoxIcon from '@mui/icons-material/AccountBox';
import Divider from '@mui/material/Divider';
import ForumIcon from '@mui/icons-material/Forum';
import Button from '@mui/material/Button';
import SaveIcon from '@mui/icons-material/Save';
import CircleIcon from '@mui/icons-material/Circle';
import * as React from 'react';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import TextField from '@mui/material/TextField';
import { useDispatch, useSelector } from 'react-redux';
import { createSelector } from '@reduxjs/toolkit';
import CommentElement from '../CommentElement/CommentElement';
import RefreshIcon from '@mui/icons-material/Refresh';


const TaskCardForm = (props) => {
    const { taskId } = props;

    /* const taskInfoSelector = createSelector(); */

    const taskInfo = useSelector(state => state.tasks.data.find(value => value.id == taskId));

    const [taskInfoLocal, setTaskInfoLocal] = React.useState(taskInfo);

    const { id, name, assignedTo, priority, statusId, description, comments, changedBy, changeDate } = taskInfoLocal;

    const dispatch = useDispatch();

    const CommentsElements = React.useMemo(() => {
        return comments.map((value, index) => <CommentElement key={index} name={value.name} img={value.img} creationDate={value.creationDate} text={value.text} />)
    });

    const handleStatusChange = (e) => {
        setTaskInfoLocal({
            ...taskInfoLocal,
            statusId: e.target.value
        })
    }

    const statusColor = React.useMemo(() => {
        switch(taskInfoLocal.statusId){
            case 1: return 'disabled';
            case 2: return 'info';
            case 3: return 'warning';
            case 4: return 'success';
            default: return 'disabled'
        }
    },[taskInfoLocal.statusId]);

    return (
        <div className="task-card-form">
            <div className="task-card-form__header header">
                <div className="header__main-info main-info">
                    <p className='main-info__id'>{id}</p>
                    <p className='main-info__name'>{name}</p>
                </div>
                <div className="header__additional-info additional-info">
                    <div className='additional-info__name'>
                        <AccountBoxIcon sx={{ height: '100%', color: 'gray', marginRight: '5px' }} />
                        <p>{assignedTo}</p>
                    </div>
                    <div className='additional-info__tools'>
                        <Button size="small" sx={{ color: 'black' }}><SaveIcon sx={{ height: '100%' }} />Сохранить</Button>
                        <Button size="small" sx={{ color: 'black' }}><RefreshIcon sx={{ height: '100%' }} />Обновить</Button>
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
                                value={statusId}
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
                        <p>{changedBy} {changeDate}</p>
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
                        defaultValue=""
                        placeholder="Введите описание задачи"
                        variant="filled"
                        sx={{ width: '70%' }}
                        value={description}
                    />
                </div>
                <div className='main-info__comments comments'>
                    <div className='comments__input'>
                        <p>Комментарии</p>
                        <TextField
                            id="filled-multiline-static"
                            multiline
                            rows={1}
                            defaultValue=""
                            placeholder="Введите текст комментария"
                            variant="filled"
                            sx={{ width: '70%' }}
                        />
                    </div>
                    <div className='comments__list'>
                        {CommentsElements}
                    </div>
                </div>
            </div>
        </div>
    )
}




export default TaskCardForm;