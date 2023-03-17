import './ProjectInfo.css';
import Divider from '@mui/material/Divider';
import { DataGrid } from '@mui/x-data-grid';
import Box from '@mui/material/Box';


const ProjectInfo = (props) => {
    const {projectInfo} = props;

    const columns = [
        { field: 'id', headerName: 'ID', width: 90, hide:true},
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
        <div className="project-info">
            <div className="project-info__project-main-info project-main-info">
                <div className="project-main-info__project-name project-name">
                    <p className="project-name__text">{projectInfo.name}</p>
                </div>
                <Divider sx={{ backgroundColor: 'grey' }} />
                <div className="project-main-info__project-description project-description">
                    <p className="project-description__text">{projectInfo.description}</p>
                </div>
            </div>
            <Divider sx={{ backgroundColor: 'grey' }} />
            <div className="project-info__project-additional-info project-additional-info">
                <div className="project-additional-info__project-employees project-employees">
                    <div className="project-employees__text">
                        <p>Список сотрудников проекта :</p>
                    </div>
                    <div className="project-employees__empoyees-list">
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
            </div>
        </div>
    )
}

export default ProjectInfo;