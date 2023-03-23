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
import FormControl from '@mui/material/FormControl';
import InputLabel from '@mui/material/InputLabel';
import Select from '@mui/material/Select';
import { useNavigate } from 'react-router-dom';
import { useLazyGetTasksByProjectIdQuery, useDeleteTaskMutation } from '../../features/tasksApi/tasksApiSlice';
import './TasksForm.css';
import { formatTime } from '../../helpers/timeHelper/timeHelper';
import Skeleton from '@mui/material/Skeleton';
import { useMemo } from 'react';
import { useSnackbar } from 'notistack';
import { useSelector } from 'react-redux';
import { selectCurrentUserId } from '../../features/auth/authSlice';


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
        field: 'priority',
        headerName: 'Приоритет',
        width: 160,
        editable: false,
        hide: false
    },
    {
        field: 'assignedTo',
        headerName: 'Назначено на',
        width: 200,
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

const TasksForm = (props) => {
    const { projectId } = props;
    const userId = useSelector(selectCurrentUserId);
    const [selectedRows, setSelectedRows] = useState([]);
    const [tasks, setTasks] = useState([]);
    const [allTasksFetch, { isLoading, error, isSuccess: isDataLoaded }] = useLazyGetTasksByProjectIdQuery();
    const [deleteTaskByIdFetch, { isLoading: isDeleting, isSuccess: isDeletingSuccess }] = useDeleteTaskMutation();
    const { enqueueSnackbar } = useSnackbar();
    const [filterValue, setFilterValue] = useState(1);
    const [isRefreshing, setIsRefreshing] = useState(false);


    let navigate = useNavigate();

    async function updateData() {
        setIsRefreshing(true);
        await allTasksFetch({ projectId: projectId, limit: 100 }).unwrap().then(value => {
            setData(value);
        }).catch(err => handleErrorTasksLoading(err));
        setIsRefreshing(false);
    }

    useEffect(() => {
        updateData();
    }, []);

    useEffect(() => {
        const update = async () => {
            await updateData();
        }

        update().then(() => {
            if (filterValue === 2) {
                setTasks(tasks.filter(value => value.assignedUserId == userId));
            }
        });

    }, [filterValue])

    const setData = (data) => {
        setTasks(formatData(data));
    }

    const formatData = (data) => {
        let items = [...data];

        items = items.map(d => {
            d = { ...d, changeDate: formatTime(d.changeDate) };
            return d;
        });


        return items;
    }

    const onRowsDelete = async () => {
        console.log(selectedRows);
        if (selectedRows?.length > 0) {
            selectedRows.forEach(id => deleteTaskByIdFetch(id).unwrap().then(() => handleSuccessRowDeleting(id)).catch(err => handleErrorRowDeleting(id, err)));
            setTasks([]);
            setSelectedRows([]);
            await updateData();
        }
    }

    const handleErrorTasksLoading = (error) => {
        console.log(error);
    }

    const handleSuccessRowDeleting = (id) => {
        enqueueSnackbar(`Задача ${id} успешно удалена`, { variant: 'success' });
    }

    const handleErrorRowDeleting = (id, error) => {
        enqueueSnackbar(`При удалении задачи ${id} произошла ошибка`, { variant: 'error' });
        console.log(error);
    }

    const rowClickHandle = (e) => {
        if (e.id) {
            navigate(`${e.id}`);
        }
    }

    const onFilterChange = async (e) => {
        const value = e.target.value;

        if (value) {
            setFilterValue(value);
        }
    }

    const onCreateTask = () => {
        navigate("createTask");
    }

    return (
        <div className='tasks-form'>
            <div className='tasks-form__header header'>
                <p className='header__text'>Задачи</p>
            </div>
            <div className='tasks-form__filters filters'>
                <FormControl sx={{ m: 1, minWidth: 162, lineHeight: '50%', textAlign: 'start' }} size="small">
                    <InputLabel id="demo-select-small"></InputLabel>
                    <Select
                        labelId="demo-select-small"
                        id="demo-select-small"
                        value={`${filterValue}`}
                        onChange={onFilterChange}
                    >
                        <MenuItem value={1}>Все</MenuItem>
                        <MenuItem value={2}>Назначено мне</MenuItem>
                    </Select>
                </FormControl>
                <Divider orientation="vertical" flexItem />
                <div className='filters__button'>
                    <Button size="small" sx={{ color: 'black' }} onClick={(e) => onCreateTask()}><AddIcon />Создать задачу</Button>
                </div>
                <div className='filters__button'>
                    <Button size="small" sx={{ color: 'black' }} onClick={(e) => onRowsDelete()} disabled={selectedRows.length === 0}><DeleteIcon />Удалить выбранные</Button>
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
                        loading={isRefreshing}
                        localeText={{noRowsLabel:"Данные отсутсвуют"}}
                    />
                </Box>
            </div>
        </div>
    );
}




export default TasksForm;