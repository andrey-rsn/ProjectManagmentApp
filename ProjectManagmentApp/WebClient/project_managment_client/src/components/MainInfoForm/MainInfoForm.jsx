import './MainInfoForm.css';
import { DataGrid } from '@mui/x-data-grid';
import Box from '@mui/material/Box';
import Divider from '@mui/material/Divider';
import Button from '@mui/material/Button';
import { useState } from "react";
import MenuList from '@mui/material/MenuList';
import MenuItem from '@mui/material/MenuItem';
import Paper from '@mui/material/Paper';
import ListItemIcon from '@mui/material/ListItemIcon';
import Typography from '@mui/material/Typography';
import PriorityHighIcon from '@mui/icons-material/PriorityHigh';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import AddIcon from '@mui/icons-material/Add';
import AccountBoxIcon from '@mui/icons-material/AccountBox';
import { useNavigate, NavLink } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { selectCurrentUserRole } from '../../features/auth/authSlice';
import { useMemo } from 'react';

const MainInfoForm = () => {
    const userRole = useSelector(selectCurrentUserRole);
    let navigate = useNavigate();

    const projectsColumns = [
        { field: 'id', headerName: 'ID', width: 90, hide: true },
        {
            field: 'firstName',
            headerName: 'Название',
            width: 400,
            editable: false,
        }
    ];

    const employeesColumns = [
        { field: 'id', headerName: 'ID', width: 90, hide: true },
        {
            field: 'firstName',
            headerName: 'Имя',
            width: 150,
            editable: false,
        },
        {
            field: 'lastName',
            headerName: 'Фамилия',
            width: 150,
            editable: true,
        }
    ];

    const rows = [
        { id: 1, lastName: 'Snow', firstName: 'Jon', age: 35 },
        { id: 2, lastName: 'Lannister', firstName: 'Cersei', age: 42 },
        { id: 3, lastName: 'Lannister', firstName: 'Jaime', age: 45 },
        { id: 4, lastName: 'Stark', firstName: 'Arya', age: 16 },
        { id: 5, lastName: 'Targaryen', firstName: 'Daenerys', age: null },
        { id: 6, lastName: 'Melisandre', firstName: null, age: 150 },
        { id: 7, lastName: 'Clifford', firstName: 'Ferrara', age: 44 },
        { id: 8, lastName: 'Frances', firstName: 'Rossini', age: 36 },
        { id: 9, lastName: 'Roxie', firstName: 'Harvey', age: 65 },
    ];

    const rowClickHandle = (e) => {
        if (e.id) {
            navigate(`/project/${e.id}`);
        }
    }

    const additionalMenuItems = useMemo(() => {
        if (userRole === 'PM') {
            return (
                <>
                    <NavLink to='addEmployee' style={{ textDecoration: 'none', color: 'black' }} relative='main'>
                        <MenuItem>
                            <ListItemIcon>
                                <AccountBoxIcon fontSize="small" />
                            </ListItemIcon>
                            <Typography variant="inherit" noWrap>Зарегистрировать сотрудника</Typography>
                        </MenuItem>
                    </NavLink>
                    <NavLink to='createProject' style={{ textDecoration: 'none', color: 'black' }} relative='main'>
                        <MenuItem>
                            <ListItemIcon>
                                <AddIcon fontSize="small" />
                            </ListItemIcon>
                            <Typography variant="inherit" noWrap>
                                Создать новый проект
                            </Typography>
                        </MenuItem>
                    </NavLink>
                </>
            )
        }

    },[userRole])

    return (
        <div className="main-info-form">
            <div className="main-info-form__header">
                <p>Главная страница</p>
            </div>
            <div className="main-info-form__content content">
                <div className="content__left-wrapper left-wrapper">
                    <div className="left-wrapper__menu menu">
                        <div className='menu__text'>
                            <p>Меню</p>
                        </div>
                        <div className='menu__menu-list'>
                            <Paper sx={{ width: '90%' }}>
                                <MenuList>
                                    <NavLink to='workTime' style={{ textDecoration: 'none', color: 'black' }} relative='main'>
                                        <MenuItem>
                                            <ListItemIcon>
                                                <AccessTimeIcon fontSize="small" />
                                            </ListItemIcon>
                                            <Typography variant="inherit" noWrap>
                                                Учёт врменени работы
                                            </Typography>
                                        </MenuItem>
                                    </NavLink>
                                    {additionalMenuItems}
                                </MenuList>
                            </Paper>
                        </div>
                    </div>
                </div>
                <div className="content__right-wrapper right-wrapper">
                    <div className="right-wrapper__projects-list projects-list">
                        <div className="projects-list__text">
                            <p>Список доступных проектов</p>
                        </div>
                        <div className="projects-list__list">
                            <Box sx={{ height: 371, width: '100%' }}>
                                <DataGrid
                                    rows={rows}
                                    columns={projectsColumns}
                                    rowsPerPageOptions={[5]}
                                    pageSize={5}
                                    disableRowSelectionOnClick
                                    onRowClick={e => rowClickHandle(e)}
                                />
                            </Box>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default MainInfoForm;