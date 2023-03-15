import './MainInfoForm.css';
import { DataGrid } from '@mui/x-data-grid';
import Box from '@mui/material/Box';
import Divider from '@mui/material/Divider';
import Button from '@mui/material/Button';
import { useEffect, useState } from "react";
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
import { selectCurrentUserRole, selectCurrentUserId } from '../../features/auth/authSlice';
import { useMemo } from 'react';
import { useLazyGetProjectsByUserIdQuery } from '../../features/projectsApi/projectsApiSlice';

const MainInfoForm = () => {
    const userRole = useSelector(selectCurrentUserRole);
    const userId = useSelector(selectCurrentUserId);
    const[projects, setProjects] = useState([]);

    let navigate = useNavigate();

    const[getPorjectsByIdFetch,{isLoading: isProjectsByUserIdLoading}] = useLazyGetProjectsByUserIdQuery();

    useEffect(() => {
        loadData();
    },[])

    const loadData = async () => {
        await getPorjectsByIdFetch(userId).unwrap().then(data=> setProjects(data)).catch(err => console.log(err));
    }

    const projectsColumns = [
        { field: 'projectId', headerName: 'projectId', width: 90, hide: true },
        {
            field: 'name',
            headerName: 'Название проекта',
            width: 400,
            editable: false,
        }
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
                            <Paper sx={{ width: '90%', backgroundColor:'aliceblue'}}>
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
                                    rows={projects}
                                    columns={projectsColumns}
                                    rowsPerPageOptions={[5]}
                                    pageSize={5}
                                    disableRowSelectionOnClick
                                    onRowClick={e => rowClickHandle(e)}
                                    getRowId={(row) => row.projectId}
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