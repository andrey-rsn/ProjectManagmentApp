import Header from '../../components/Header/Header';
import MainInfoForm from '../../components/MainInfoForm/MainInfoForm';
import './StartPage.css';
import { BrowserRouter as Router, Route, Routes, useParams } from 'react-router-dom';
import WorkTimePage from '../WorkTimePage/WorkTimePage';
import EmployeeRegistrationPage from '../EmployeeRegistrationPage/EmployeeRegistrationPage';
import { SnackbarProvider } from 'notistack';
import CreateProjectPage from '../CreateProjectPage/CreateProjectPage';
import { useLazyGetUserInfoQuery } from '../../features/auth/authApiSlice';
import { useSelector, useDispatch } from 'react-redux';
import { selectCurrentUserId } from '../../features/auth/authSlice';
import { setUserInfo } from '../../features/auth/authSlice';
import { useEffect, useState } from 'react';
import CircularProgress from '@mui/material/CircularProgress';

const StartPage = () => {
    const dispatch = useDispatch();
    const userInfo = useSelector(state => state.auth.userInfo);
    const userId = useSelector(selectCurrentUserId);
    const [isPageLoading, setIsPageLoading] = useState(true);

    const [getUserInfoFetch] = useLazyGetUserInfoQuery();

    useEffect(() => {
        const loadUserInfoFunc = async () => {
            await loadUserInfo();
        }

        loadUserInfoFunc();

    }, [userInfo])

    const loadUserInfo = async () => {
        await getUserInfoFetch(userId).unwrap().then(data => getUserSuccessHandle(data)).catch(err => console.log(err));
    }

    const getUserSuccessHandle = (data) => {
        const info = {
            firstName: data.firstName,
            secondName: data.secondName,
            patronymic: data.patronymic
        }
        console.log(info);
        dispatch(setUserInfo(info));
        setIsPageLoading(false);
    }

    return (
        <div className="start-page">
            {isPageLoading ? <CircularProgress /> :
                <>
                    <div className="start-page__header">
                        <Header />
                    </div>
                    <div className="start-page__content">
                        <SnackbarProvider maxSnack={3} anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }} autoHideDuration={5000}>
                            <Routes>
                                <Route path="/" element={<MainInfoForm />} />
                                <Route path="/workTime" element={<WorkTimePage />} />
                                <Route path="/addEmployee" element={<EmployeeRegistrationPage />} />
                                <Route path="/createProject" element={<CreateProjectPage />} />
                            </Routes>
                        </SnackbarProvider>
                    </div>
                </>
            }
        </div >
    )
}

export default StartPage;