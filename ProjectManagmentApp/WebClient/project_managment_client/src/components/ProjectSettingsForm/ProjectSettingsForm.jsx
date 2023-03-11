import "./ProjectSettingsForm.css";
import OutlinedInput from '@mui/material/OutlinedInput';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Divider from '@mui/material/Divider';
import { NavLink } from "react-router-dom";

const ProjectSettingsForm = () => {

    return (
        <div className="project-settings-form">
            <div className="project-settings-form__header">
                <p>Настройки проекта</p>
            </div>
            <Divider sx={{ backgroundColor: 'grey' }} />
            <div className="project-settings-form__project-params project-params">
                <div className="project-params__params-input params-input">
                    <p className="params-input__param-name">Название проекта :</p>
                    <OutlinedInput placeholder="Название проекта" sx={{ width: '100%', height: '35px' }} />
                </div>
                <div className="project-params__params-input params-input">
                    <p className="params-input__param-name">Описание проекта :</p>
                    <TextField
                        sx={{ width: '100%' }}
                        id="outlined-multiline-flexible"
                        multiline
                        maxRows={6}
                        placeholder="Описание проекта"
                    />
                </div>
            </div>
            <Divider sx={{ backgroundColor: 'grey', marginBottom: '20px' }} />
            <div className="project-settings-form__bottom">
                <Button variant="contained" color="success" sx={{ marginRight: "auto" }}>
                    Сохранить
                </Button>
                <NavLink to='attachEmployee' relative='main/projectSettings' style={{textDecoration:'none'}}>
                    <Button variant="contained" color="success">
                        Прикрепить сотрудников
                    </Button>
                </NavLink>
            </div>
        </div>
    )
}

export default ProjectSettingsForm;