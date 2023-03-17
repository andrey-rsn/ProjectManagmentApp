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
import Skeleton from '@mui/material/Skeleton';
import { useState } from 'react';


const MainPage = () => {
    const {projectId} = useParams();
    const navigate = useNavigate();
    const userId = useSelector(selectCurrentUserId);
    const[projectFetch, {isLoading: isProjectLoading}] = useLazyGetProjectsByUserAndProjectIdQuery();
    const[dataIsLoading,setDataIsLoading] = useState(true);
    const [data, setData] = useState({});

    useEffect(() => {
        if(checkProjectId()){
            loadData();
        } else {
            navigate('/main');
        }
    },[])

    const loadData = async () =>{
        var data = await projectFetch({userId, projectId}).unwrap().then(data => data).catch(err => handleError(err));
        setData(data);
        setDataIsLoading(false);
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
                {isProjectLoading || dataIsLoading? <Skeleton sx={{width:'300px', height:'100%'}}/> : <ActionList projectId={projectId} projectName={data.name}/>}
                <Routes>
                    <Route path="/" element={<ProjectInfoPage projectInfo={data} />} />
                    <Route path="/tasks" element={<TasksPage projectId={projectId}/>} />
                    <Route path="/tasks/:taskId" element={<TaskCardPage />} />
                    <Route path="/projectSettings" element={<ProjectSettingsPage projectInfo={data}/>} />
                    <Route path="/projectSettings/attachEmployee" element={<AttachEmployeePage projectId={projectId}/>} />
                </Routes>
            </div>
        </div >
    )
}

export default MainPage;