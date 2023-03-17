import ProjectSettingsForm from "../../components/ProjectSettingsForm/ProjectSettingsForm";
import "./ProjectSettingsPage.css";

const ProjectSettingsPage = (props) => {

    const {projectInfo} = props;

    return(
        <div className="project-settings-page">
            <ProjectSettingsForm projectInfo={projectInfo}/>
        </div>
    )
}

export default ProjectSettingsPage;