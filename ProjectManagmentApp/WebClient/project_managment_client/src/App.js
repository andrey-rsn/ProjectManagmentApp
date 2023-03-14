import './App.css';
import LoginPage from './pages/LoginPage/LoginPage';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import MainPage from './pages/MainPage/MainPage';
import { useEffect } from "react"
import { useNavigate } from "react-router-dom"
import { DefaultPage } from './pages/DefaultPage/DefaultPage';
import RequireAuth from './features/auth/requireAuth';
import StartPage from './pages/StartPage/StartPage';

const App = () => {



    return (
        <div className="App">
            <Routes>
                <Route path="/" element={<DefaultPage />} />

                <Route path="/login" element={<LoginPage />} />

                <Route element={< RequireAuth />}>
                    <Route path="/main" element={<StartPage />} />
                    <Route path="/main/:projectId/*" element={<MainPage />} />
                </Route>

            </Routes>
        </div>
    );
}

export default App;
