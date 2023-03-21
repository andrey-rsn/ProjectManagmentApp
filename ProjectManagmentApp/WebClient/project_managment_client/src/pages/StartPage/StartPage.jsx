import Header from '../../components/Header/Header';
import MainInfoForm from '../../components/MainInfoForm/MainInfoForm';
import './StartPage.css';
import { BrowserRouter as Router, Route, Routes, useParams } from 'react-router-dom';
import WorkTimePage from '../WorkTimePage/WorkTimePage';
import EmployeeRegistrationPage from '../EmployeeRegistrationPage/EmployeeRegistrationPage';
import { SnackbarProvider } from 'notistack';

const StartPage = () => {


    return (
        <div className="start-page">
            <div className="start-page__header">
                <Header />
            </div>
            <div className="start-page__content">
                <SnackbarProvider maxSnack={3} anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }} autoHideDuration={5000}>
                    <Routes>
                        <Route path="/" element={<MainInfoForm />} />
                        <Route path="/workTime" element={<WorkTimePage />} />
                        <Route path="/addEmployee" element={<EmployeeRegistrationPage />} />
                    </Routes>
                </SnackbarProvider>
            </div>
        </div >
    )
}

export default StartPage;