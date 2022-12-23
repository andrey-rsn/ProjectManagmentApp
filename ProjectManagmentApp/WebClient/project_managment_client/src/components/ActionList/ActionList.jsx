import * as React from 'react';
import Box from '@mui/material/Box';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import Divider from '@mui/material/Divider';
import ArticleIcon from '@mui/icons-material/Article';
import DescriptionIcon from '@mui/icons-material/Description';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import TaskAltIcon from '@mui/icons-material/TaskAlt';
import SummarizeIcon from '@mui/icons-material/Summarize';
import AssessmentIcon from '@mui/icons-material/Assessment';
import DuoIcon from '@mui/icons-material/Duo';
import SettingsIcon from '@mui/icons-material/Settings';
import './ActionList.css'

const ActionList = () =>{
    return (
        <Box sx={{ width: '100%', maxWidth: 300, backgroundColor:'rgb(190, 196, 181)',height:'100vh', padding:'0' }}>
            <List className='action-list'>
              <div className='action-list__element'>
                <ListItem disablePadding>
                  <ListItemButton>
                    <ListItemIcon>
                      <ArticleIcon />
                    </ListItemIcon>
                    <ListItemText primary="Название проекта" />
                  </ListItemButton>
                </ListItem>
                <Divider />
              </div>
              <div className='action-list__element'>
                <ListItem disablePadding>
                  <ListItemButton>
                    <ListItemIcon>
                      <AccessTimeIcon />
                    </ListItemIcon>
                    <ListItemText primary="Учёт времени работы" />
                  </ListItemButton>
                </ListItem>
              </div>
              <div className='action-list__element'>
                <ListItem disablePadding>
                  <ListItemButton>
                    <ListItemIcon>
                      <TaskAltIcon />
                    </ListItemIcon>
                    <ListItemText primary="Задачи" />
                  </ListItemButton>
                </ListItem>
              </div>
              <div className='action-list__element'>
                <ListItem disablePadding>
                  <ListItemButton>
                    <ListItemIcon>
                      <DescriptionIcon />
                    </ListItemIcon>
                    <ListItemText primary="Документация" />
                  </ListItemButton>
                </ListItem>
              </div>
              <div className='action-list__element'>
                <ListItem disablePadding>
                  <ListItemButton>
                    <ListItemIcon>
                      <SummarizeIcon />
                    </ListItemIcon>
                    <ListItemText primary="Отчёты" />
                  </ListItemButton>
                </ListItem>
              </div>
              <div className='action-list__element'>
                <ListItem disablePadding>
                  <ListItemButton>
                    <ListItemIcon>
                      <AssessmentIcon/>
                    </ListItemIcon>
                    <ListItemText primary="Аналитика работы" />
                  </ListItemButton>
                </ListItem>
              </div>
              <div className='action-list__element'>
                <ListItem disablePadding>
                  <ListItemButton>
                    <ListItemIcon>
                      <DuoIcon/>
                    </ListItemIcon>
                    <ListItemText primary="Видеоконференции" />
                  </ListItemButton>
                </ListItem>
              </div>
              <div className='action-list__element'>
                <Divider />
                <ListItem disablePadding >
                  <ListItemButton>
                    <ListItemIcon>
                      <SettingsIcon/>
                    </ListItemIcon>
                    <ListItemText primary="Параметры проекта" />
                  </ListItemButton>
                </ListItem>
              </div>
            </List>
        </Box>
      );
}

export default ActionList;