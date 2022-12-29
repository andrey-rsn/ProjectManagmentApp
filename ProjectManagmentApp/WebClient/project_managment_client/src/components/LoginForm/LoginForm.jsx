import React from 'react';
import { useFormik } from 'formik';
import * as yup from 'yup';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import { useNavigate} from "react-router-dom";
import './LoginForm.css';

const validationSchema = yup.object({
  email: yup
    .string('Введите логин')
    .email('Логин некорректен')
    .required('Поле обязательно для заполенения'),
  password: yup
    .string('Введите пароль')
    .required('Поле обязательно для заполенения'),
});

const LoginForm = () => {

  const formik = useFormik({
    initialValues: {
      email: '',
      password: '',
    },
    validationSchema: validationSchema,
    onSubmit: (values) => {
      onFormSubmit(values);
    },
  });

  let navigate = useNavigate();

  const onFormSubmit = (values) =>{
    console.log(values);
    navigate('/main');
  }

  return (
    <div className='login-form-container'>
      <div className='login-box'>
        <p>Авторизация</p>
        <form onSubmit={formik.handleSubmit} className='login-box__form login-box-container'>
          <div className='form__login-field input-field'>
            <TextField
              fullWidth
              id="email"
              name="email"
              label="Логин"
              value={formik.values.email}
              onChange={formik.handleChange}
              error={formik.touched.email && Boolean(formik.errors.email)}
              helperText={formik.touched.email && formik.errors.email}
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
              error={formik.touched.password && Boolean(formik.errors.password)}
              helperText={formik.touched.password && formik.errors.password}
              sx={{ height: '100% !important' }}
            />
          </div>
          <Button color="primary" variant="contained" fullWidth type="submit" sx={{height:'40px'}}>
            Войти
          </Button>
        </form>
      </div>
    </div>
  );
}

export default LoginForm;