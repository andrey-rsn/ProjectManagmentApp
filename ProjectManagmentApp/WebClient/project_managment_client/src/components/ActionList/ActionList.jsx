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

const ActionList = () => {
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
    <Box sx={{ width: '100%', maxWidth: 300, backgroundColor: 'rgb(190, 196, 181)', display: 'flex', flexDirection: 'column', flexGrow: 1 ,height: '100%', padding: '0' }}>
      <List className='action-list'>
        <ActionItem style={({ isActive }) => LinkStyle(isActive)} text="Название проекта" linkTo="." image={<ArticleIcon />}
          listItemStyle={{ backgroundColor: 'rgb(148, 148, 148)' }}
        />
        <Divider sx={{ backgroundColor: 'black', height: '0.2px' }} />
        <ActionItem style={({ isActive }) => LinkStyle(isActive)} text="Учёт времени работы" linkTo="/main/workTime" image={<AccessTimeIcon />} />
        <ActionItem style={({ isActive }) => LinkStyle(isActive)} text="Задачи" linkTo="/main/tasks" image={<TaskAltIcon />} />
        <ActionItem style={({ isActive }) => LinkStyle(isActive)} text="Документация" linkTo="/main/documents" image={<DescriptionIcon />} />
        <ActionItem style={({ isActive }) => LinkStyle(isActive)} text="Отчёты" linkTo="/main/reports" image={<SummarizeIcon />} />
        <ActionItem style={({ isActive }) => LinkStyle(isActive)} text="Аналитика работы" linkTo="/main/analytics" image={<AssessmentIcon />} />
        {isUserPM ? <ActionItem style={({ isActive }) => LinkStyle(isActive)} text="Параметры проекта" linkTo="/main/projectSettings" image={<SettingsIcon />} isHeaderDivider={true} /> : <div></div>}
      </List>
    </Box>
  );
}

export default ActionList;