import "./AttachEmployeeForm.css";
import { DataGrid } from '@mui/x-data-grid';
import Box from '@mui/material/Box';
import Divider from '@mui/material/Divider';
import Button from '@mui/material/Button';
import { useState } from "react";

const AttachEmployeeForm = () => {

    const [selectedRows, setSelectedRows] = useState([]);

    const columns = [
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


    return (
        <div className="attach-employee-form">
            <div className="attach-employee-form__header header">
                <p className="header__text">Прикрепление сотрудников к проекту Название проекта</p>
            </div>
            <Divider sx={{ backgroundColor: 'grey', marginBottom: '20px' }} />
            <div className="attach-employee-form__attached-employees attached-employees">
                <div className="attached-employees__text">
                    <p>Прикрепленные к проекту пользователи</p>
                </div>
                <div className="attached-employees__list">
                    <Box sx={{ height: 371, width: '100%' }}>
                        <DataGrid
                            rows={rows}
                            columns={columns}
                            rowsPerPageOptions={[5]}
                            pageSize={5}
                            disableRowSelectionOnClick
                        />
                    </Box>
                </div>
            </div>
            <Divider sx={{ backgroundColor: 'grey', marginBottom: '20px' }} />
            <div className="attach-employee-form__all-employees all-employees">
                <div className="all-employees__text">
                    <p>Не прикрепленные к проекту пользователи</p>
                </div>
                <div className="all-employees__list">
                    <Box sx={{ height: 371, width: '100%' }}>
                        <DataGrid
                            rows={rows}
                            columns={columns}
                            rowsPerPageOptions={[5]}
                            pageSize={5}
                            disableRowSelectionOnClick
                            checkboxSelection
                            onSelectionModelChange={e => setSelectedRows(e)}
                        />
                    </Box>
                </div>
            </div>
            <div className="attach-employee-form__bottom bottom">
                <Button variant="contained" color="success" disabled={selectedRows.length === 0}>
                    Прикрепить выбранных сотрудников
                </Button>
            </div>
        </div>
    )
}

export default AttachEmployeeForm;