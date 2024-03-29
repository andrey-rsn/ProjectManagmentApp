import "./ProjectSettingsForm.css";
import OutlinedInput from '@mui/material/OutlinedInput';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Divider from '@mui/material/Divider';
import { NavLink } from "react-router-dom";
import { useUpdateProjectMutation } from "../../features/projectsApi/projectsApiSlice";
import { useEffect, useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import { setProjectInfo } from "../../features/projectsApi/projectsSlice";
import { selectCurrentUserRole } from "../../features/auth/authSlice";
import { useNavigate } from "react-router-dom";
import { useSnackbar } from 'notistack';

const ProjectSettingsForm = () => {

    const [projectInfoChanges, setProjectInfoChanges] = useState({name:'', description:''}); 

    const [updateProjectFetch, {isLoading: isProjectUpdating, isSuccess: isProjectUpdateSuccess}] = useUpdateProjectMutation();

    const { enqueueSnackbar } = useSnackbar();

    const projectInfo = useSelector(state => state.projects);
    const userRole = useSelector(selectCurrentUserRole);
    const navigate = useNavigate();

    const dispatch = useDispatch();

    useEffect(() => {
        if (userRole !== "PM") {
            navigate("/forbid");
        }
    }, [])

    const onDescriptionChange = (e) => {
        setProjectInfoChanges({...projectInfoChanges, description: e.target.value})
    }

    const onNameChange = (e) => {
        setProjectInfoChanges({...projectInfoChanges, name: e.target.value})
    }

    const onSaveClick = async () => {
        let dataToSave = projectInfoChanges;

        if(dataToSave.name.length === 0) {
            dataToSave.name = projectInfo.name;
        }

        if(dataToSave.description.length === 0) {
            dataToSave.description = projectInfo.description;
        }

        dataToSave.projectId = projectInfo.projectId;

        await updateProjectFetch(dataToSave).unwrap().then(data => handleSuccessUpdateProject(data)).catch(err => handleErrorUpdateProject(err));
    }

    const handleSuccessUpdateProject = (data) => {
        dispatch(setProjectInfo(data));
        setProjectInfoChanges({name:'', description:''});
        enqueueSnackbar('Настройки проекта успешно обновлены', { variant: 'success' });
    }

    const handleErrorUpdateProject = (error) => {
        enqueueSnackbar(`Ошибка при сохранении настроек проекта`, { variant: 'error' });
        console.log(error);
    }

    return (
        <div className="project-settings-form">
            <div className="project-settings-form__header">
                <p>Настройки проекта</p>
            </div>
            <Divider sx={{ backgroundColor: 'grey' }} />
            <div className="project-settings-form__project-params project-params">
                <div className="project-params__params-input params-input">
                    <p className="params-input__param-name">Название проекта :</p>
                    <OutlinedInput placeholder="Название проекта" sx={{ width: '100%', height: '35px' }} defaultValue={`${projectInfo.name}`} onChange={e => onNameChange(e)}/>
                </div>
                <div className="project-params__params-input params-input">
                    <p className="params-input__param-name">Описание проекта :</p>
                    <TextField
                        sx={{ width: '100%' }}
                        id="outlined-multiline-flexible"
                        multiline
                        maxRows={6}
                        placeholder="Описание проекта"
                        defaultValue={`${projectInfo.description}`}
                        onChange={e => onDescriptionChange(e)}
                    />
                </div>
            </div>
            <Divider sx={{ backgroundColor: 'grey', marginBottom: '20px' }} />
            <div className="project-settings-form__bottom">
                <Button variant="contained" color="success" sx={{ marginRight: "auto" }} onClick={() => onSaveClick()} disabled ={!(projectInfoChanges.name.length !== 0 || projectInfoChanges.description.length !== 0)}>
                    Сохранить
                </Button>
                <NavLink to='attachEmployee' relative='main/projectSettings' style={{textDecoration:'none'}}>
                    <Button variant="contained" >
                        Прикрепить сотрудников
                    </Button>
                </NavLink>
            </div>
        </div>
    )
}

export default ProjectSettingsForm;