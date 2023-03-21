import "./AttachEmployeeForm.css";
import { DataGrid } from '@mui/x-data-grid';
import Box from '@mui/material/Box';
import Divider from '@mui/material/Divider';
import Button from '@mui/material/Button';
import { useEffect, useState } from "react";
import { useLazyGetEmployeesAttachedToProjectQuery, useLazyGetEmployeesNotAttachedToProjectQuery } from '../../features/projectsApi/projectsApiSlice';
import { useParams } from 'react-router-dom';
import Skeleton from '@mui/material/Skeleton';
import { useSelector } from 'react-redux';
import { useAttachEmployeesMutation } from "../../features/projectsApi/projectsApiSlice";
import { useSnackbar } from 'notistack';

const AttachEmployeeForm = () => {

    const { projectId } = useParams();

    const [selectedRows, setSelectedRows] = useState([]);

    const { enqueueSnackbar } = useSnackbar();

    const [attachEmployeesToProjectFetch, {isLoading: isAttaching, isSuccess: isSuccessAttaching}] = useAttachEmployeesMutation();
    const [attachedEmployeesFetch, { isLoading: isAttachedEmployeesLoading, isSuccess: isAttachedEmployeesSuccess }] = useLazyGetEmployeesAttachedToProjectQuery();
    const [notAttachedEmployeesFetch, { isLoading: isNotAttachedEmployeesLoading, isSuccess: isNotAttachedEmployeesSuccess }] = useLazyGetEmployeesNotAttachedToProjectQuery();

    const [attachedEmployees, setAttachedEmployees] = useState([]);

    const [notAttachedEmployees, setNotAttachedEmployees] = useState([]);
    const projectInfo = useSelector(state => state.projects);

    useEffect(() => {
        loadData();
    }, [])

    const loadData = async () => {
        await attachedEmployeesFetch({ projectId }).unwrap().then(data => setAttachedEmployees(data)).catch(err => console.log(err));
        await notAttachedEmployeesFetch({ projectId }).unwrap().then(data => setNotAttachedEmployees(data)).catch(err => console.log(err));
    }

    const columns = [
        { field: 'id', headerName: 'ID', width: 90, hide: true },
        {
            field: 'firstName',
            headerName: 'Имя',
            width: 150,
            editable: false,
        },
        {
            field: 'secondName',
            headerName: 'Фамилия',
            width: 150,
            editable: true,
        },
        {
            field: 'position',
            headerName: 'Должность',
            width: 190,
            editable: true,
        }
    ];

    const onAttachClick = async () => {
        let requestData = {
            projectId: Number(projectId),
            employeesIds: selectedRows
        }

        await attachEmployeesToProjectFetch(requestData).unwrap().then(()=>handleSuccessAttach()).catch(err => handleErrorAttach(err));

    }

    const handleSuccessAttach = async () => {
        enqueueSnackbar('Пользователи успешно прикрепелены к проекту', {variant:'success'});
        setSelectedRows([]);
        await loadData();
    }

    const handleErrorAttach = (error) => {
        enqueueSnackbar(`Ошибка при прикреплении пользователя`, {variant:'error'});
        console.log(error);
    }

    return (
        <div className="attach-employee-form">
            <div className="attach-employee-form__header header">
                <p className="header__text">Прикрепление сотрудников к проекту {projectInfo.name}</p>
            </div>
            <Divider sx={{ backgroundColor: 'grey', marginBottom: '20px' }} />
            <div className="attach-employee-form__attached-employees attached-employees">
                <div className="attached-employees__text">
                    <p>Прикрепленные к проекту пользователи</p>
                </div>
                <div className="attached-employees__list">
                    {isAttachedEmployeesLoading || !isAttachedEmployeesSuccess ?
                        <Skeleton
                            sx={{ height: 371, width: '100%' }}
                            variant='rounded'
                        /> :
                        <Box sx={{ height: 371, width: '100%' }}>
                            <DataGrid
                                rows={attachedEmployees}
                                columns={columns}
                                rowsPerPageOptions={[5]}
                                pageSize={5}
                                disableRowSelectionOnClick
                                getRowId={(row) => row.user_Id}
                            />
                        </Box>}
                </div>
            </div>
            <Divider sx={{ backgroundColor: 'grey', marginBottom: '20px' }} />
            <div className="attach-employee-form__all-employees all-employees">
                <div className="all-employees__text">
                    <p>Не прикрепленные к проекту пользователи</p>
                </div>
                <div className="all-employees__list">
                    {isNotAttachedEmployeesLoading || !isNotAttachedEmployeesSuccess ?
                        <Skeleton
                            sx={{ height: 371, width: '100%' }}
                            variant='rounded'
                        /> :
                        <Box sx={{ height: 371, width: '100%' }}>
                            <DataGrid
                                rows={notAttachedEmployees}
                                columns={columns}
                                rowsPerPageOptions={[5]}
                                pageSize={5}
                                disableRowSelectionOnClick
                                checkboxSelection
                                getRowId={(row) => row.user_Id}
                                onSelectionModelChange={e => setSelectedRows(e)}
                            />
                        </Box>}
                </div>
            </div>
            <div className="attach-employee-form__bottom bottom">
                <Button variant="contained" color="success" disabled={selectedRows.length === 0} onClick={() => onAttachClick()}>
                    Прикрепить выбранных сотрудников
                </Button>
            </div>
        </div>
    )
}

export default AttachEmployeeForm;