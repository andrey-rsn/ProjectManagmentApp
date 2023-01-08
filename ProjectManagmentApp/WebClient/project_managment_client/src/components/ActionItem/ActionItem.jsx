import * as React from 'react';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import { NavLink } from "react-router-dom";
import Divider from '@mui/material/Divider';
import '../ActionList/ActionList.css'

const ActionItem = (props)=>{
    const {style,linkTo,text,image,isHeaderDivider,listItemStyle} = props;

    return(
        <div className='action-list__element'>
          <NavLink to={linkTo} style={style} relative='main'>
            {isHeaderDivider ? <Divider />: null}
            <ListItem disablePadding sx={listItemStyle === undefined ? {backgroundColor:'inherit'} : listItemStyle}>
              <ListItemButton>
                <ListItemIcon>
                  {image}
                </ListItemIcon>
                <ListItemText primary={text} />
              </ListItemButton>
            </ListItem>
          </NavLink>
        </div>
    )
} 

export default ActionItem;