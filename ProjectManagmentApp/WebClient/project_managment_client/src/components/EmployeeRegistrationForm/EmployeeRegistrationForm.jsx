import "./EmployeeRegistrationForm.css";
import TextField from '@mui/material/TextField';
import * as yup from 'yup';
import { useFormik } from 'formik';
import { useEffect, useMemo, useState } from "react";
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import InputLabel from '@mui/material/InputLabel';
import Divider from '@mui/material/Divider';
import Button from '@mui/material/Button';
import { useLazyGetPositionsQuery, useRegisterMutation } from "../../features/auth/authApiSlice";
import { useSnackbar } from 'notistack';

const EmployeeRegistrationForm = () => {

    const [registrationError, setRegistrationError] = useState();
    const [positions, setPositions] = useState([]);
    const { enqueueSnackbar } = useSnackbar();

    const [getAllPositionsFetch, {isLoading: isPositionsLoading, isSuccess: isPositionsLoadingSuccess}] = useLazyGetPositionsQuery();
    const [registrationFetch] = useRegisterMutation();

    useEffect(() => {
        const loadDataAsync = async () => {
            await loadData();
        }

        loadDataAsync();
    }, [])

    const loadData = async () => {
        await getAllPositionsFetch().unwrap().then(data => setPositions(data)).catch(err => console.log(err));
    }

    const positionsElements = useMemo(() => {
        return (
            positions.map(value => {
                return <MenuItem key={value.position_Id} value={value.position_Id}>{value.positionName}</MenuItem>
            })
        )
    }, [positions])

    const validationSchema = yup.object({
        email: yup
            .string('Введите Email')
            .email('Некорректное значение Email')
            .required('Поле обязательно для заполенения'),
        login: yup
            .string('Введите логин')
            .required('Поле обязательно для заполенения'),
        password: yup
            .string('Введите пароль')
            .required('Поле обязательно для заполенения'),
        firstName: yup
            .string('Введите имя')
            .required('Поле обязательно для заполенения'),
        secondName: yup
            .string('Введите фамилию')
            .required('Поле обязательно для заполенения'),
        patronymic: yup
            .string('Введите отчество'),
        role: yup
            .string()
            .required('Поле обязательно для заполенения'),
        PositionId: yup
            .number()
            .min(1)
            .required('Поле обязательно для заполенения')
    });

    const formik = useFormik({
        initialValues: {
            email:'',
            login:'',
            password:'',
            firstName:'',
            secondName:'',
            patronymic:'',
            role:'',
            PositionId: 0
        },
        validationSchema: validationSchema,
        onSubmit: (values) => {
            onFormSubmit(values);
        },
    });


    const onFormSubmit = async (values) => {
        console.log(values);
        await registrationFetch(values).unwrap().then(() => handleSuccessRegistartion()).catch(err => handleErrorRegistration(err));
    }

    const handleSuccessRegistartion = () => {
        enqueueSnackbar('Пользователь был успешно зарегистрирован', {variant:'success'});
        formik.resetForm();
    }

    const handleErrorRegistration = (err) => {
        if(err.status === 409) {
            enqueueSnackbar(`Пользователь с таким Email или Логином уже существует`, {variant:'error'});
        } else {
            enqueueSnackbar(`Ошибка при регистрации пользователя`, {variant:'error'});
        }
    }

    return (
        <form className="employee-registration-form" onSubmit={formik.handleSubmit}>
            <div className="employee-registration-form__header-text">
                <p>Регистрация сотрудника</p>
            </div>
            <Divider sx={{ backgroundColor: 'grey' }} />
            <div className="employee-registration-form__registration-form-body registration-form-body">
                <div className="registration-form-body__input-element input-element">
                    <div className="input-element__element-text">
                        <p>Email</p>
                    </div>
                    <div className="input-element__input-box">
                        <TextField
                            fullWidth
                            id="email"
                            name="email"
                            label="Email"
                            value={formik.values.email}
                            onChange={formik.handleChange}
                            error={formik.touched.email && Boolean(formik.errors.email) || Boolean(registrationError)}
                            helperText={formik.touched.email && formik.errors.email}
                            sx={{ height: '100% !important' }}
                        />
                    </div>
                </div>
                <div className="registration-form-body__input-element input-element">
                    <div className="input-element__element-text">
                        <p>Логин</p>
                    </div>
                    <div className="input-element__input-box">
                        <TextField
                            fullWidth
                            id="login"
                            name="login"
                            label="Логин"
                            value={formik.values.login}
                            onChange={formik.handleChange}
                            error={formik.touched.login && Boolean(formik.errors.login) || Boolean(registrationError)}
                            helperText={formik.touched.login && formik.errors.login}
                            sx={{ height: '100% !important' }}
                        />
                    </div>
                </div>
                <div className="registration-form-body__input-element input-element">
                    <div className="input-element__element-text">
                        <p>Пароль</p>
                    </div>
                    <div className="input-element__input-box">
                        <TextField
                            fullWidth
                            id="password"
                            name="password"
                            label="Пароль"
                            value={formik.values.password}
                            onChange={formik.handleChange}
                            error={formik.touched.password && Boolean(formik.errors.password) || Boolean(registrationError)}
                            helperText={formik.touched.password && formik.errors.password}
                            sx={{ height: '100% !important' }}
                        />
                    </div>
                </div>
                <div className="registration-form-body__input-element input-element">
                    <div className="input-element__element-text">
                        <p>Имя</p>
                    </div>
                    <div className="input-element__input-box">
                        <TextField
                            fullWidth
                            id="firstName"
                            name="firstName"
                            label="Имя"
                            value={formik.values.firstName}
                            onChange={formik.handleChange}
                            error={formik.touched.firstName && Boolean(formik.errors.firstName) || Boolean(registrationError)}
                            helperText={formik.touched.firstName && formik.errors.firstName}
                            sx={{ height: '100% !important' }}
                        />
                    </div>
                </div>
                <div className="registration-form-body__input-element input-element">
                    <div className="input-element__element-text">
                        <p>Фамилия</p>
                    </div>
                    <div className="input-element__input-box">
                        <TextField
                            fullWidth
                            id="secondName"
                            name="secondName"
                            label="Фамилия"
                            value={formik.values.secondName}
                            onChange={formik.handleChange}
                            error={formik.touched.secondName && Boolean(formik.errors.secondName) || Boolean(registrationError)}
                            helperText={formik.touched.secondName && formik.errors.secondName}
                            sx={{ height: '100% !important' }}
                        />
                    </div>
                </div>
                <div className="registration-form-body__input-element input-element">
                    <div className="input-element__element-text">
                        <p>Отчество</p>
                    </div>
                    <div className="input-element__input-box">
                        <TextField
                            fullWidth
                            id="patronymic"
                            name="patronymic"
                            label="Отчество"
                            value={formik.values.patronymic}
                            onChange={formik.handleChange}
                            error={formik.touched.patronymic && Boolean(formik.errors.patronymic) || Boolean(registrationError)}
                            helperText={formik.touched.patronymic && formik.errors.patronymic}
                            sx={{ height: '100% !important' }}
                        />
                    </div>
                </div>
                <div className="registration-form-body__input-element input-element">
                    <div className="input-element__element-text">
                        <p>Роль</p>
                    </div>
                    <div className="input-element__input-box">
                        <FormControl sx={{ m: 1, width:'100%', textAlign:'start', margin:'0' }} size="medium">
                            <InputLabel id="demo-select-small"></InputLabel>
                            <Select
                                id="role"
                                name="role"
                                value={formik.values.role}
                                onChange={formik.handleChange}
                                error={formik.touched.role && Boolean(formik.errors.role) || Boolean(registrationError)}
                            >
                                <MenuItem value={'PM'}>Проектный менеджер</MenuItem>
                                <MenuItem value={'Employee'}>Сотрудник</MenuItem>
                            </Select>
                        </FormControl>
                    </div>
                </div>
                <div className="registration-form-body__input-element input-element">
                    <div className="input-element__element-text">
                        <p>Должность</p>
                    </div>
                    <div className="input-element__input-box">
                        <FormControl sx={{ m: 1, width:'100%', textAlign:'start', margin:'0' }} size="medium">
                            <InputLabel id="demo-select-small"></InputLabel>
                            <Select
                                id="PositionId"
                                name="PositionId"
                                value={formik.values.PositionId}
                                onChange={formik.handleChange}
                                error={formik.touched.PositionId && Boolean(formik.errors.PositionId) || Boolean(registrationError)}
                                disabled={isPositionsLoading || !isPositionsLoadingSuccess}
                            >
                                {positionsElements}
                            </Select>
                        </FormControl>
                    </div>
                </div>
            </div>
            <Divider sx={{ backgroundColor: 'grey' }} />
            <div className="employee-registration-form__form-bottom form-bottom">
                <div className="form-bottom__actions">
                    <Button variant="contained" type="submit">
                        Зарегистрировать сотрудника
                    </Button>
                </div>
            </div>
        </form>
    )
}

export default EmployeeRegistrationForm;