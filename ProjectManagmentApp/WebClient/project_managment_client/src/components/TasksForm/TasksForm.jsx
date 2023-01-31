import * as React from 'react';
import Box from '@mui/material/Box';
import { DataGrid } from '@mui/x-data-grid';
import { styled, alpha } from '@mui/material/styles';
import Button from '@mui/material/Button';
import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';
import AddIcon from '@mui/icons-material/Add';
import DeleteIcon from '@mui/icons-material/Delete';
import Divider from '@mui/material/Divider';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
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
        field: 'fullName',
        headerName: 'Назначено на',
        description: 'This column has a value getter and is not sortable.',
        sortable: false,
        width: 160,
        valueGetter: (params) =>
            `${params.row.firstName || ''} ${params.row.lastName || ''}`,
    },
];

const rows = [
    { id: 1, name: 'Snow', status: 'Jon', changeDate: 35 },
    { id: 2, name: 'Lannister', status: 'Cersei', changeDate: 42 },
    { id: 3, name: 'Lannister', status: 'Jaime', changeDate: 45 },
    { id: 4, name: 'Stark', status: 'Arya', changeDate: 16 },
    { id: 5, name: 'Targaryen', status: 'Daenerys', changeDate: null },
    { id: 6, name: 'Melisandre', status: null, changeDate: 150 },
    { id: 7, name: 'Clifford', status: 'Ferrara', changeDate: 44 },
    { id: 8, name: 'Frances', status: 'Rossini', changeDate: 36 },
    { id: 9, name: 'Roxie', status: 'Harvey', changeDate: 65 },
];

const TasksForm = () => {

    return (
        <div className='tasks-form'>
            <div className='tasks-form__header header'>
                <p className='header__text'>Задачи</p>
            </div>
            <div className='tasks-form__filters'>
                <CustomizedMenus />
                <Divider orientation="vertical" flexItem />
                <div>
                    <Button size="small" sx={{color: 'black'}}><AddIcon />Создать задачу</Button>
                </div>
                <div>
                    <Button size="small" sx={{color: 'black'}}><DeleteIcon />Удалить выбранные</Button>
                </div>
            </div>
            <div className='tasks-form__data-table'>
                <Box sx={{ height: 'auto', width: '100%' }}>
                    <DataGrid
                        rows={rows}
                        columns={columns}
                        pageSize={10}
                        rowsPerPageOptions={[10]}
                        /* checkboxSelection */
                        disableSelectionOnClick
                        experimentalFeatures={{ newEditingApi: true }}
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