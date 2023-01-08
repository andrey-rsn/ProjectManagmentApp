import * as React from 'react';
import PropTypes from 'prop-types';
import LinearProgress from '@mui/material/LinearProgress';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import './WorkTimePage.css';

function LinearProgressWithLabel(props) {
  return (
    <Box sx={{ display: 'flex', alignItems: 'center' }}>
      <Box sx={{ width: '40%', mr: 1 }}>
        <LinearProgress sx={{height:'10px',borderRadius:'4px'}} variant="determinate" {...props} />
      </Box>
      <Box sx={{ minWidth: 35 }}>
        <Typography variant="body2" color="text.secondary">{`${Math.round(
          props.value,
        )}%`}</Typography>
      </Box>
    </Box>
  );
}


const WorkTimePage = () => {


    return (
        <div className='work-time-page'>
            <Box sx={{ width: '100%' }}>
                <LinearProgressWithLabel value={40} />
            </Box>
            <p>Время начала работы</p>
            <p>Время завершения работы</p>
        </div>
    )
}

export default WorkTimePage;