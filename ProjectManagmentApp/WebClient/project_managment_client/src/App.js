import './App.css';
import LoginPage from './pages/LoginPage/LoginPage';
import {BrowserRouter as Router, Route, Routes} from 'react-router-dom';
import MainPage from './pages/MainPage/MainPage';
import { useEffect } from "react"
import { useNavigate } from "react-router-dom"
import {DefaultPage} from './pages/DefaultPage/DefaultPage';

const App = () => {

  return (
    <div className="App">
        <Routes>
          <Route path="/" element={<DefaultPage/>}/>
          <Route path="/login" element={<LoginPage/>}/>
          <Route path="/main/*" element={<MainPage/>}/>
        </Routes>
    </div>
  );
}

export default App;
