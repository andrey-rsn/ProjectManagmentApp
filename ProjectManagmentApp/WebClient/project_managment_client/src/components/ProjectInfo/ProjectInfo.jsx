import './ProjectInfo.css';
import Divider from '@mui/material/Divider';
import { DataGrid } from '@mui/x-data-grid';
import Box from '@mui/material/Box';

const ProjectInfo = () => {

    const columns = [
        { field: 'id', headerName: 'ID', width: 90 },
        {
            field: 'firstName',
            headerName: 'First name',
            width: 150,
            editable: true,
        },
        {
            field: 'lastName',
            headerName: 'Last name',
            width: 150,
            editable: true,
        },
        {
            field: 'age',
            headerName: 'Age',
            type: 'number',
            width: 110,
            editable: true,
        },
        {
            field: 'fullName',
            headerName: 'Full name',
            description: 'This column has a value getter and is not sortable.',
            sortable: false,
            width: 160,
            valueGetter: (params) =>
                `${params.row.firstName || ''} ${params.row.lastName || ''}`,
        },
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
        <div className="project-info">
            <div className="project-info__project-main-info project-main-info">
                <div className="project-main-info__name">
                    <p>Название проекта</p>
                </div>
                <Divider sx={{ backgroundColor: 'grey' }} />
                <div className="project-main-info__description">
                    <p>Lorem ipsum dolor, sit amet consectetur adipisicing elit. Quisquam necessitatibus nam, laboriosam id ex eum doloribus fugiat quibusdam molestiae corrupti rem culpa fugit totam repellat, aliquid accusantium itaque numquam in?</p>
                </div>
            </div>
            <Divider sx={{ backgroundColor: 'grey' }} />
            <div className="project-info__project-additional-info project-additional-info">
                <div className="project-additional-info__project-employees project-employees">
                    <div className="project-employees__text">
                        <p>Список сотрудников проекта :</p>
                    </div>
                    <div className="project-employees__empoyees-list">
                        <Box sx={{ height: 400, width: '100%' }}>
                            <DataGrid
                                rows={rows}
                                columns={columns}
                                initialState={{
                                    pagination: {
                                        paginationModel: {
                                            pageSize: 5,
                                        },
                                    },
                                }}
                                pageSizeOptions={[5]}
                                disableRowSelectionOnClick
                            />
                        </Box>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default ProjectInfo;