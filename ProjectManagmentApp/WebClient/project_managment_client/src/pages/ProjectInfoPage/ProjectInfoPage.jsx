import ProjectInfo from "../../components/ProjectInfo/ProjectInfo";
import './ProjectInfoPage.css';

const ProjectInfoPage = (props)=>{
    const {projectInfo} = props;

    return(
        <div className="project-info-page">
            <ProjectInfo projectInfo = {projectInfo}/>
        </div>
    )
}

export default ProjectInfoPage;