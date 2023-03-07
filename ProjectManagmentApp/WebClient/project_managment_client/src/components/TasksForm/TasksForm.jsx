import * as React from 'react';
import Box from '@mui/material/Box';
import { useState, useEffect } from 'react';
import { DataGrid } from '@mui/x-data-grid';
import { styled, alpha } from '@mui/material/styles';
import Button from '@mui/material/Button';
import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';
import AddIcon from '@mui/icons-material/Add';
import DeleteIcon from '@mui/icons-material/Delete';
import Divider from '@mui/material/Divider';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import { useDispatch, useSelector } from 'react-redux';
import { updateData } from '../../features/tasksApi/tasksSlice';
import { useNavigate } from 'react-router-dom';
import { useLazyGetAllTasksQuery } from '../../features/tasksApi/tasksApiSlice'; 
import './TasksForm.css';


const StyledMenu = styled((props) => (
    <Menu
        elevation={0}
        anchorOrigin={{
            vertical: 'bottom',
            horizontal: 'right',
        }}
        transformOrigin={{
            vertical: 'top',
            horizontal: 'right',
        }}
        {...props}
    />
))(({ theme }) => ({
    '& .MuiPaper-root': {
        borderRadius: 6,
        marginTop: theme.spacing(1),
        minWidth: 180,
        color:
            theme.palette.mode === 'light' ? 'rgb(55, 65, 81)' : theme.palette.grey[300],
        boxShadow:
            'rgb(255, 255, 255) 0px 0px 0px 0px, rgba(0, 0, 0, 0.05) 0px 0px 0px 1px, rgba(0, 0, 0, 0.1) 0px 10px 15px -3px, rgba(0, 0, 0, 0.05) 0px 4px 6px -2px',
        '& .MuiMenu-list': {
            padding: '4px 0',
        },
        '& .MuiMenuItem-root': {
            '& .MuiSvgIcon-root': {
                fontSize: 18,
                color: theme.palette.text.secondary,
                marginRight: theme.spacing(1.5),
            },
            '&:active': {
                backgroundColor: alpha(
                    theme.palette.primary.main,
                    theme.palette.action.selectedOpacity,
                ),
            },
        },
    },
}));


const columns = [
    { field: 'id', headerName: 'Идентфикатор задачи', width: 90 },
    {
        field: 'name',
        headerName: 'Название',
        width: 150,
        editable: false,
    },
    {
        field: 'status',
        headerName: 'Статус',
        width: 150,
        editable: false,
    },
    {
        field: 'changeDate',
        headerName: 'Дата изменения',
        width: 130,
        editable: false,
    },
    {
        field: 'assignedTo',
        headerName: 'Назначено на',
        width: 160,
        editable: false
    },
    {
        field: 'assignedUserId',
        headerName: 'Идентификатор назначенного пользователя',
        width: 160,
        editable: false,
        hide: true
    }
];

const TasksForm = () => {

    const [selectedRows, setSelectedRows] = useState([]);
    const [tasks, setTasks] = useState([]);
    const [allTasksFetch, { isLoading, error }] = useLazyGetAllTasksQuery();
    const dispatch = useDispatch();

    let navigate = useNavigate();

    useEffect(() => {

        async function updateData() {
            await allTasksFetch(50).unwrap().then(value => {
                setTasks(value);
            }).catch(err => console.log(err));
        }

        updateData();
    }, []);

    const onRowsDelete = () => {
        console.dir(selectedRows);
        if(selectedRows?.length > 0){
            let newTasks = tasks;

            selectedRows.forEach((id) => {
                newTasks = newTasks.filter(value => value.id != id);
            })
            console.log(newTasks);
            dispatch(updateData({data: newTasks}));
        }
    }

    const rowClickHandle = (e) => {
        if(e.id){
            navigate(`/main/tasks/${e.id}`);
        }
    }

    return (
        <div className='tasks-form'>
            <div className='tasks-form__header header'>
                <p className='header__text'>Задачи</p>
            </div>
            <div className='tasks-form__filters'>
                <CustomizedMenus />
                <Divider orientation="vertical" flexItem />
                <div>
                    <Button size="small" sx={{ color: 'black' }}><AddIcon />Создать задачу</Button>
                </div>
                <div>
                    <Button size="small" sx={{ color: 'black' }} onClick={onRowsDelete}><DeleteIcon />Удалить выбранные</Button>
                </div>
            </div>
            <div className='tasks-form__data-table'>
                <Box sx={{ height: 'auto', width: '100%' }}>
                    <DataGrid
                        rows={tasks}
                        columns={columns}
                        pageSize={10}
                        rowsPerPageOptions={[10]}
                        checkboxSelection
                        disableSelectionOnClick
                        experimentalFeatures={{ newEditingApi: true }}
                        onSelectionModelChange={e => setSelectedRows(e)}
                        onRowClick={e => rowClickHandle(e)}
                    />
                </Box>
            </div>
        </div>
    );
}


function CustomizedMenus() {
    const [anchorEl, setAnchorEl] = React.useState(null);
    const [btnText, setBtnText] = React.useState("Назначено мне");
    const open = Boolean(anchorEl);
    const handleClick = (event) => {
        setAnchorEl(event.currentTarget);

    };
    const handleClose = (event) => {
        setAnchorEl(null);
        const text = event.target.innerText;
        if (text) {
            setBtnText(text);
        }

    };

    return (
        <div>
            <Button
                id="demo-customized-button"
                aria-controls={open ? 'demo-customized-menu' : undefined}
                aria-haspopup="true"
                aria-expanded={open ? 'true' : undefined}
                variant="contained"
                disableElevation
                onClick={handleClick}
                endIcon={<KeyboardArrowDownIcon />}
                sx={{ minWidth: '190px', display: 'flex', justifyContent: 'space-between' }}
            >
                {btnText}
            </Button>
            <StyledMenu
                id="demo-customized-menu"
                MenuListProps={{
                    'aria-labelledby': 'demo-customized-button',
                }}
                anchorEl={anchorEl}
                open={open}
                onClose={handleClose}
            >
                <MenuItem onClick={handleClose} disableRipple>
                    Все
                </MenuItem>
                <MenuItem onClick={handleClose} disableRipple>
                    Назначено мне
                </MenuItem>
            </StyledMenu>
        </div>
    );
}


export default TasksForm;