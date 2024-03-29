import React, { useEffect, useState } from 'react';
import { useFormik } from 'formik';
import * as yup from 'yup';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import { useNavigate } from "react-router-dom";
import { useDispatch } from 'react-redux';
import { setCredentials } from '../../features/auth/authSlice';
import { useLoginMutation } from '../../features/auth/authApiSlice';
import './LoginForm.css';

const validationSchema = yup.object({
    login: yup
        .string('Введите логин')
        .required('Поле обязательно для заполенения'),
    password: yup
        .string('Введите пароль')
        .required('Поле обязательно для заполенения'),
});

const LoginForm = () => {

    const [user, setUser] = useState('');
    const [authError,setAuthError] = useState();
    const [loginFunc, { isLoading }] = useLoginMutation();
    const dispatch = useDispatch();

    const formik = useFormik({
        initialValues: {
            login: '',
            password: '',
        },
        validationSchema: validationSchema,
        onSubmit: (values) => {
            onFormSubmit(values);
        },
    });

    let navigate = useNavigate();

    const onFormSubmit = async (values) => {
        const { login, password } = values;
        try {
                await loginFunc({ login, password }).unwrap().then(value=> {
                dispatch(setCredentials(value));
                navigate('/main');
            });

        } catch (error) {
            if(error.status == 404){
                setAuthError('Неверный логин или пароль');
            }
            console.log(error);
        }
    }

    return (
        <div className='login-form-container'>
            <div className='login-box'>
                <p>Авторизация</p>
                <form onSubmit={formik.handleSubmit} className='login-box__form login-box-container'>
                    <div className='form__login-field input-field'>
                        <TextField
                            fullWidth
                            id="login"
                            name="login"
                            label="Логин"
                            value={formik.values.login}
                            onChange={formik.handleChange}
                            error={formik.touched.login && Boolean(formik.errors.login) || Boolean(authError)}
                            helperText={formik.touched.login && formik.errors.login}
                            sx={{ height: '100% !important' }}
                        />
                    </div>
                    <div className='form__password-field input-field'>
                        <TextField
                            fullWidth
                            id="password"
                            name="password"
                            label="Пароль"
                            type="password"
                            value={formik.values.password}
                            onChange={formik.handleChange}
                            error={formik.touched.password && Boolean(formik.errors.password) || Boolean(authError)}
                            helperText={formik.touched.password && formik.errors.password || authError}
                            sx={{ height: '100% !important' }}
                        />
                    </div>
                    <Button color="primary" variant="contained" fullWidth type="submit" sx={{ height: '40px' }}>
                        Войти
                    </Button>
                </form>
            </div>
        </div>
    );
}

export default LoginForm;