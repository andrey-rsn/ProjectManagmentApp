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


const TaskCardForm = (props) => {
    const { taskId } = props;

    /* const taskInfoSelector = createSelector(); */

    const taskInfo = useSelector(state => state.tasks.data.find(value => value.id == taskId));

    const {id, name} = taskInfo;

    const dispatch = useDispatch();

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
                        <p>Коровай Андрей Александрович</p>
                    </div>
                    <div className='additional-info__comments'>
                        <ForumIcon sx={{ marginRight: '5px', height: '100%', color: 'blue' }} />
                        <p>Комменатриев: 0</p>
                    </div>
                    <div className='additional-info__tools'>
                        <Button size="small" sx={{ color: 'black' }}><SaveIcon sx={{ height: '100%' }} />Сохранить</Button>
                    </div>
                </div>
            </div>
            <Divider sx={{ backgroundColor: 'grey' }} />
            <div className="task-card-form__secondary-info secondary-info">
                <div className='left-side'>
                    <div className='secondary-info__state'>
                        <p>Состояние:</p>
                        <CircleIcon color='disabled' sx={{ fontSize: '1.2em', height: '100%', marginRight: '0px !important' }} />
                        <SelectSmall />
                    </div>
                    <div className='secondary-info__priority'>
                        <p>Приоритет:</p>
                        <p>2</p>
                    </div>
                </div>
                <div className='right-side'>
                    <div className='secondary-info__updated-by'>
                        <p>Кем обновлено:</p>
                        <p>Admin admin admin 20.10.2023</p>
                    </div>
                </div>
            </div>
            <Divider sx={{ backgroundColor: 'grey' }} />
            <div className="task-card-form__main-info main-info">
                <div className='main-info__description'>
                    <p>Описание</p>
                    <TextField
                        id="filled-multiline-static"
                        multiline
                        rows={8}
                        defaultValue="Описание задачи"
                        variant="filled"
                        sx={{width: '70%'}}
                    />
                </div>
                <div className='main-info__comments'>
                    <p>Комментарии</p>
                    <TextField
                        id="filled-multiline-static"
                        multiline
                        rows={1}
                        defaultValue="Введите текст комментария..."
                        variant="filled"
                        sx={{width: '70%'}}
                    />
                </div>
            </div>
        </div>
    )
}

function SelectSmall() {
    const [age, setAge] = React.useState('');

    const handleChange = (event) => {
        setAge(event.target.value);
    };

    return (
        <FormControl sx={{ m: 1, minWidth: 160, lineHeight: '50%' }} size="small">
            <InputLabel id="demo-select-small"></InputLabel>
            <Select
                labelId="demo-select-small"
                id="demo-select-small"
                value={10}
                onChange={handleChange}
            >
                <MenuItem value={10}>Новая</MenuItem>
                <MenuItem value={20}>В работе</MenuItem>
                <MenuItem value={40}>Выполнена</MenuItem>
                <MenuItem value={50}>Завершена</MenuItem>
            </Select>
        </FormControl>
    );
}

export default TaskCardForm;