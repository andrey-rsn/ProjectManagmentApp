import Header from '../../components/Header/Header';
import MainInfoForm from '../../components/MainInfoForm/MainInfoForm';
import './StartPage.css';
import { BrowserRouter as Router, Route, Routes, useParams } from 'react-router-dom';
import WorkTimePage from '../WorkTimePage/WorkTimePage';

const StartPage = () => {


    return (
        <div className="start-page">
            <div className="start-page__header">
                <Header />
            </div>
            <div className="start-page__content">
                <Routes>
                    <Route path="/" element={<MainInfoForm/>} />
                    <Route path="/workTime" element={<WorkTimePage/>} /> 
                </Routes>
                
            </div>
        </div >
    )
}

export default StartPage;