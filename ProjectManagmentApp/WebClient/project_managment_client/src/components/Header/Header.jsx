import './Header.css'
import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import Menu from '@mui/material/Menu';
import Avatar from '@mui/material/Avatar';
import Tooltip from '@mui/material/Tooltip';
import MenuItem from '@mui/material/MenuItem';
import { logOut } from '../../features/auth/authSlice';
import { NavLink } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { useSelector, useDispatch } from 'react-redux';
import { useMemo } from 'react';




const Header = () => {
    const settings = ['Настройки', 'Выйти'];
    const [anchorElUser, setAnchorElUser] = React.useState(null);
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const userInfo = useSelector(state => state.auth.userInfo);


    const handleOpenUserMenu = (event) => {
        setAnchorElUser(event.currentTarget);
    };

    const handleCloseUserMenu = () => {
        setAnchorElUser(null);
    };

    const onUserMenuParameterClick = (e) => {
        switch (e.target.innerHTML) {
            case 'Выйти':
                dispatch(logOut());
                navigate("/login");
                break;

        }
    }

    const userShortNameText = useMemo(() => {
        console.log(userInfo);
        if (userInfo.firstName && userInfo.secondName && userInfo.patronymic) {
            return `${userInfo.secondName} ${userInfo.firstName[0]}.${userInfo.patronymic[0]}`;
        }
        return "";
    }, [userInfo])

    return (
        <Box >
            <AppBar position="static">
                <Toolbar sx={{ flexDirection: 'row', justifyContent: 'start' }}>
                    <Typography
                        variant="h6"
                        noWrap
                        component="div"
                        sx={{ flexGrow: 1, display: { xs: 'none', sm: 'block' }, textAlign: 'left', marginRight: 'auto', maxWidth: '200px' }}
                    >
                        <NavLink to='../' style={{ textDecoration: 'none', color: 'white' }} relative='main'>
                            Project Managment
                        </NavLink>
                    </Typography>

                    <Box sx={{ flexGrow: 0, display: 'flex', flexDirection: 'row' }}>
                        <p style={{ marginRight: '15px' }}>{userShortNameText}</p>
                        <Tooltip title="Профиль">
                            <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
                                <Avatar alt={userInfo.firstName} src="/static/images/avatar/2.jpg" />
                            </IconButton>
                        </Tooltip>
                        <Menu
                            sx={{ mt: '45px' }}
                            id="menu-appbar"
                            anchorEl={anchorElUser}
                            anchorOrigin={{
                                vertical: 'top',
                                horizontal: 'right',
                            }}
                            keepMounted
                            transformOrigin={{
                                vertical: 'top',
                                horizontal: 'right',
                            }}
                            open={Boolean(anchorElUser)}
                            onClose={handleCloseUserMenu}
                        >
                            {settings.map((setting) => (
                                <MenuItem key={setting} onClick={handleCloseUserMenu}>
                                    <Typography textAlign="center" onClick={(e) => onUserMenuParameterClick(e)}>{setting}</Typography>
                                </MenuItem>
                            ))}
                        </Menu>
                    </Box>
                </Toolbar>
            </AppBar>
        </Box>
    );
}

export default Header;