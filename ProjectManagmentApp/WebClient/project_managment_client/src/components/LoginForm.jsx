import "./LoginForm.css"
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import OutlinedInput from '@mui/material/OutlinedInput';
import InputLabel from '@mui/material/InputLabel';
import InputAdornment from '@mui/material/InputAdornment';
import FormControl from '@mui/material/FormControl';
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';
import TextField from '@mui/material/TextField';
import * as React from 'react';

const LoginForm = ()=>{

    const [showPassword, setShowPassword] = React.useState(false);

    const handleClickShowPassword = () => setShowPassword((show) => !show);
  
    const handleMouseDownPassword = (event) => {
      event.preventDefault();
    };

    return(
        <div className="login-form-container">
            <div className="login-box">
                <div className="login-box__header">
                    <p>Авторизация</p>
                </div>
                <div className="login-box__middle box-middle">
                        <TextField
                        error
                        helperText="Incorrect entry."
                          id="outlined-password-input"
                          label="Логин"
                          type="login"
                          autoComplete="current-password"
                          sx={{width:'250px'}}
                        />
                        <FormControl sx={{ m: 1, width: '250px' }} variant="outlined" error helperText="Incorrect entry.">
                            <InputLabel htmlFor="outlined-adornment-password" error >Пароль</InputLabel>
                            <OutlinedInput
                              id="outlined-adornment-password"
                              type={showPassword ? 'text' : 'password'}
                              endAdornment={
                                <InputAdornment position="end">
                                  <IconButton
                                    aria-label="toggle password visibility"
                                    onClick={handleClickShowPassword}
                                    onMouseDown={handleMouseDownPassword}
                                    edge="end"
                                  >
                                    {showPassword ? <VisibilityOff /> : <Visibility />}
                                  </IconButton>
                                </InputAdornment>
                              }
                              label="Password"
                            />
                        </FormControl>
                </div>
                <div className="login-box__footer">
                    <Button variant="contained" sx={{minWidth:'250px',height:'40px'}}>Войти</Button>
                </div>
            </div>
        </div>
    )
}

export default LoginForm;