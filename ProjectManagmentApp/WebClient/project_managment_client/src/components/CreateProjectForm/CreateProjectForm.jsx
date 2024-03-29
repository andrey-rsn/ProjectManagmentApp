import "./CreateProjectForm.css";
import Divider from '@mui/material/Divider';
import TextField from '@mui/material/TextField';
import { useFormik } from 'formik';
import * as yup from 'yup';
import { useState } from "react";
import Button from '@mui/material/Button';
import { useSnackbar } from 'notistack';
import { useCreateProjectMutation } from "../../features/projectsApi/projectsApiSlice";
import { useSelector } from "react-redux";
import { selectCurrentUserId } from "../../features/auth/authSlice";
import { selectCurrentUserRole } from "../../features/auth/authSlice";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";

const CreateProjectForm = () => {

    const [createProjectError, setCreateProjectError] = useState();

    const [createProjectFetch] = useCreateProjectMutation();

    const { enqueueSnackbar } = useSnackbar();

    const userId = useSelector(selectCurrentUserId);
    const userRole = useSelector(selectCurrentUserRole);
    const navigate = useNavigate();

    useEffect(() => {
        if (userRole !== "PM") {
            navigate("/forbid");
        }
    },[])

    const validationSchema = yup.object({
        name: yup
            .string('Введите название проекта')
            .required('Поле обязательно для заполенения'),
        description: yup
            .string('Введите описание проекта')
            .required('Поле обязательно для заполенения')
    });

    const formik = useFormik({
        initialValues: {
            name: '',
            description: '',
        },
        validationSchema: validationSchema,
        onSubmit: (values) => {
            onFormSubmit(values);
        },
    });

    const onFormSubmit = async (values) => {
        const requestData = {
            name: values.name,
            description: values.description,
            authorId: userId
        }

        await createProjectFetch(requestData).unwrap().then(() => handleSuccessProjectCreation()).catch(err => handleErrorProjectCreation(err));
    }

    const handleSuccessProjectCreation = () => {
        enqueueSnackbar('Проект успешно создан', {variant:'success'});
        formik.resetForm();
    }

    const handleErrorProjectCreation = (error) => {
        if(error.status === 409) {
            enqueueSnackbar('Проект с таким названием уже существует', {variant:'error'});
        } else {
           enqueueSnackbar('Ошибка при создании проекта', {variant:'error'}); 
        }
    }

    return (
        <form className="create-project-form" onSubmit={formik.handleSubmit}>
            <div className="create-project-form__header-text">
                <p>Создание проекта</p>
            </div>
            <Divider sx={{ backgroundColor: 'grey' }} />
            <div className="create-project-form__form-body create-project-form-body">
                <div className="create-project-form-body__input-element input-element">
                    <div className="input-element__text">
                        <p>Название проекта</p>
                    </div>
                    <div className="input-element__input-box">
                        <TextField
                            fullWidth
                            id="name"
                            name="name"
                            value={formik.values.name}
                            onChange={formik.handleChange}
                            error={formik.touched.name && Boolean(formik.errors.name) || Boolean(createProjectError)}
                            helperText={formik.touched.name && formik.errors.name}
                            sx={{ height: '100% !important' }}
                        />
                    </div>
                </div>
                <div className="create-project-form-body__input-element input-element">
                    <div className="input-element__text">
                        <p>Описание проекта</p>
                    </div>
                    <div className="input-element__input-box">
                        <TextField
                            fullWidth
                            id="description"
                            name="description"
                            multiline
                            maxRows={6}
                            value={formik.values.description}
                            onChange={formik.handleChange}
                            error={formik.touched.description && Boolean(formik.errors.description) || Boolean(createProjectError)}
                            helperText={formik.touched.description && formik.errors.description}
                            sx={{ height: '100% !important' }}
                        />
                    </div>
                </div>
            </div>
            <Divider sx={{ backgroundColor: 'grey' }} />
            <div className="create-project-form__form-bottom form-bottom">
                <div className="form-bottom__actions">
                    <Button variant="contained" type="submit">
                        Создать проект
                    </Button>
                </div>
            </div>
        </form>
    )
}

export default CreateProjectForm;