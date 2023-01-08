import WorkTimePage from "../WorkTimePage/WorkTimePage";
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import ActionList from '../../components/ActionList/ActionList';
import Header from '../../components/Header/Header';
import './MainPage.css';
import ProjectInfoPage from "../ProjectInfoPage/ProjectInfoPage";


const MainPage = () => {


    return (
        <div className="main-page">
            <div className="main-page__header">
                <Header />
            </div>
            <div className="main-page__content">
                <ActionList />
                <Routes>
                    <Route path="/" element={<ProjectInfoPage />} />
                    <Route path="/workTime" element={<WorkTimePage />} />
                </Routes>
            </div>
        </div >
    )
}

export default MainPage;