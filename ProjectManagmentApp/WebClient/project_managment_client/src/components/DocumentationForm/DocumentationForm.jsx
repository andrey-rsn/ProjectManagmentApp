import "./DocumentationForm.css";
import FileUploadIcon from '@mui/icons-material/FileUpload';
import Button from '@mui/material/Button';
import Divider from '@mui/material/Divider';
import { DataGrid } from '@mui/x-data-grid';
import Box from '@mui/material/Box';

const DocumentationForm = () => {

    const rows = [
        { id: 1, documentName: 'Snow', documentDescription: 'Jon', age: 35 },
        { id: 2, documentName: 'Lannister', documentDescription: 'Cersei', age: 42 },
        { id: 3, documentName: 'Lannister', documentDescription: 'Jaime', age: 45 },
        { id: 4, documentName: 'Stark', documentDescription: 'Arya', age: 16 },
        { id: 5, documentName: 'Targaryen', documentDescription: 'Daenerys', age: null },
        { id: 6, documentName: 'Melisandre', documentDescription: null, age: 150 },
        { id: 7, documentName: 'Clifford', documentDescription: 'Ferrara', age: 44 },
        { id: 8, documentName: 'Frances', documentDescription: 'Rossini', age: 36 },
        { id: 9, documentName: 'Roxie', documentDescription: 'Harvey', age: 65 },
    ];

    const columns = [
        { field: 'id', headerName: 'ID', width: 90, hide: true },
        {
            field: 'documentName',
            headerName: 'Название документа',
            width: 150,
            editable: false,
        },
        {
            field: 'documentDescription',
            headerName: 'Описание документа',
            width: 400,
            editable: true,
        }
    ];

    return (
        <div className="documentation-form">
            <div className="documentation-form__header">
                <p>Документация проекта</p>
            </div>
            <div className="documentation-form__form-actions form-actions">
                <div className="form-actions__buttons">
                    <Button size="small" sx={{ color: 'black' }}><FileUploadIcon />Загрузить документ</Button>
                </div>
            </div>
            <Divider />
            <div className="documentation-form__documents-list documents-list">
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
    )
}

export default DocumentationForm;

