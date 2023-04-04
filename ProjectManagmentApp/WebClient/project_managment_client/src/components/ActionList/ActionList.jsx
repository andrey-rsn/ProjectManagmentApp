import * as React from 'react';
import Box from '@mui/material/Box';
import List from '@mui/material/List';
import Divider from '@mui/material/Divider';
import ArticleIcon from '@mui/icons-material/Article';
import DescriptionIcon from '@mui/icons-material/Description';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import TaskAltIcon from '@mui/icons-material/TaskAlt';
import SummarizeIcon from '@mui/icons-material/Summarize';
import AssessmentIcon from '@mui/icons-material/Assessment';
import SettingsIcon from '@mui/icons-material/Settings';
import './ActionList.css'
import ActionItem from '../ActionItem/ActionItem';
import { useSelector } from 'react-redux';
import { selectCurrentUserRole } from '../../features/auth/authSlice';
import { Skeleton } from '@mui/material';



const ActionList = (props) => {
    const { projectId, projectName, isLoading } = props;
    const userRole = useSelector(selectCurrentUserRole);




    const LinkStyle = (isActive) => {
        return {
            color: 'inherit',
            textDecoration: 'none',
            backgroundColor: (isActive ? 'rgb(107, 153, 123)' : undefined)
        }
    }

    const isUserPM = userRole === 'PM';

    return (
        <Box sx={{ width: '100%', maxWidth: 300, backgroundColor: 'rgb(190, 196, 181)', display: 'flex', flexDirection: 'column', flexGrow: 1, height: '100%', padding: '0' }}>
            {isLoading ?
                <Skeleton sx={{height:'100%', width: '100%', display:"flex"}}/> :
                <List className='action-list'>
                    <ActionItem style={({ isActive }) => LinkStyle(isActive)} text={`${projectName}`} linkTo={`/project/${projectId}`} image={<ArticleIcon />}
                        listItemStyle={{ backgroundColor: 'rgb(148, 148, 148)' }}
                    />
                    <Divider sx={{ backgroundColor: 'black', height: '0.2px' }} />
                    <ActionItem style={({ isActive }) => LinkStyle(isActive)} text="Задачи" linkTo={`/project/${projectId}/tasks`} image={<TaskAltIcon />} />
                    <ActionItem style={({ isActive }) => LinkStyle(isActive)} text="Документация" linkTo={`/project/${projectId}/documents`} image={<DescriptionIcon />} />
                    <ActionItem style={({ isActive }) => LinkStyle(isActive)} text="Аналитика работы" linkTo={`/project/${projectId}/analytics`} image={<AssessmentIcon />} />
                    {isUserPM ? <ActionItem style={({ isActive }) => LinkStyle(isActive)} text="Параметры проекта" linkTo={`/project/${projectId}/projectSettings`} image={<SettingsIcon />} isHeaderDivider={true} /> : <div></div>}
                </List>}
        </Box>
    );
}

export default ActionList;