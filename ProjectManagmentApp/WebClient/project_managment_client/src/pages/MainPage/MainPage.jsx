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
import { useSelector, useDispatch } from 'react-redux';
import { selectCurrentUserId } from '../../features/auth/authSlice';
import { useLazyGetProjectsByUserAndProjectIdQuery } from '../../features/projectsApi/projectsApiSlice';
import Skeleton from '@mui/material/Skeleton';
import { useState } from 'react';
import { setProjectInfo } from '../../features/projectsApi/projectsSlice';
import { SnackbarProvider } from 'notistack';
import DocumentationPage from '../DocumentationPage/DocumentationPage';
import AnalyticsPage from '../AnalyticsPage/AnalyticsPage';


const MainPage = () => {
    const { projectId } = useParams();
    const navigate = useNavigate();
    const userId = useSelector(selectCurrentUserId);
    const [projectFetch, { isLoading: isProjectLoading }] = useLazyGetProjectsByUserAndProjectIdQuery();
    const [dataIsLoading, setDataIsLoading] = useState(true);
    const projectInfo = useSelector(state => state.projects);
    const dispatch = useDispatch();

    useEffect(() => {
        if (checkProjectId()) {
            const loadDataProcess = async () => {
                await loadData();
            }

            loadDataProcess();
        } else {
            navigate('/main');
        }
    }, [])


    const loadData = async () => {
        await projectFetch({ userId, projectId }).unwrap().then(data => dispatch(setProjectInfo(data))).catch(err => handleError(err));

        setDataIsLoading(false);
    }



    const handleError = (error) => {
        switch (error.status) {
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
                <SnackbarProvider maxSnack={3} anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }} autoHideDuration={5000}>
                    <ActionList projectId={projectId} projectName={projectInfo.name} isLoading={isProjectLoading || dataIsLoading} />
                    <Routes>
                        <Route exact path="/" element={<ProjectInfoPage />} />
                        <Route path="/tasks" element={<TasksPage projectId={projectId} />} />
                        <Route path="/tasks/:taskId" element={<TaskCardPage />} />
                        <Route path="/tasks/createTask" element={<TaskCardPage isNew={true}/>} />
                        <Route path="/analytics" element={<AnalyticsPage />} />
                        <Route path="/projectSettings" element={<ProjectSettingsPage />} />
                        <Route path="/projectSettings/attachEmployee" element={<AttachEmployeePage projectId={projectId} />} />
                        <Route exact path="/documents" element={<DocumentationPage />} />
                    </Routes>
                </SnackbarProvider>
            </div>
        </div >
    )
}

export default MainPage;