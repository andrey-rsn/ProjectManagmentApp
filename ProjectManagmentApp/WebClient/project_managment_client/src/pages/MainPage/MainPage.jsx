import { BrowserRouter as Router, Route, Routes, useParams } from 'react-router-dom';
import ActionList from '../../components/ActionList/ActionList';
import Header from '../../components/Header/Header';
import './MainPage.css';
import ProjectInfoPage from "../ProjectInfoPage/ProjectInfoPage";
import TasksPage from "../TasksPage/TasksPage";
import TaskCardPage from "../TaskCardPage/TaskCardPage";
import ProjectSettingsPage from "../ProjectSettingsPage/ProjectSettingsPage";
import AttachEmployeePage from "../AttachEmployeePage/AttachEmployeePage";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useSelector, useDispatch} from 'react-redux';
import { selectCurrentUserId } from '../../features/auth/authSlice';
import { useLazyGetProjectsByUserAndProjectIdQuery } from '../../features/projectsApi/projectsApiSlice';
import { setProjectInfo } from '../../features/projectsApi/projectsSlice';


const MainPage = () => {
    const {projectId} = useParams();
    const navigate = useNavigate();
    const userId = useSelector(selectCurrentUserId);
    const dispatch = useDispatch();
    const[projectFetch, {isLoading: isProjectLoading}] = useLazyGetProjectsByUserAndProjectIdQuery();

    useEffect(() => {
        if(checkProjectId()){
            loadData();
        } else {
            navigate('/main');
        }
    },[])

    const loadData = async () =>{
        await projectFetch({userId, projectId}).unwrap().then(data => dispatch(setProjectInfo(data))).catch(err => handleError(err));
    }

    const handleError = (error) => {
        switch(error.status){
            case 404:
                navigate('/notFound');
                break;
            case 403:
                navigate('/forbid');
                break;
            default:
                console.log(error);
                navigate('/main');
                break;
        }
    }

    const checkProjectId = () => {
        return projectId !== undefined && !isNaN(projectId);
    }

    return (
        <div className="main-page">
            <div className="main-page__header">
                <Header />
            </div>
            <div className="main-page__content">
                <ActionList projectId={projectId}/>
                <Routes>
                    <Route path="/" element={<ProjectInfoPage projectId={projectId}/>} />
                    <Route path="/tasks" element={<TasksPage projectId={projectId}/>} />
                    <Route path="/tasks/:taskId" element={<TaskCardPage />} />
                    <Route path="/projectSettings" element={<ProjectSettingsPage projectId={projectId}/>} />
                    <Route path="/projectSettings/attachEmployee" element={<AttachEmployeePage projectId={projectId}/>} />
                </Routes>
            </div>
        </div >
    )
}

export default MainPage;