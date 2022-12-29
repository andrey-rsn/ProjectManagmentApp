import './App.css';
import LoginPage from './pages/LoginPage/LoginPage';
import {BrowserRouter as Router, Route, Routes} from 'react-router-dom';
import MainPage from './pages/MainPage/MainPage';
import { DefaultPage } from './pages/DefaultPage/DefaultPage';

function App() {
  return (
    <div className="App">
      <Router>
        <Routes>
          <Route path="/" element={<DefaultPage/>}/>
          <Route path="/login" element={<LoginPage/>}/>
          <Route path="/main" element={<MainPage/>}/>
        </Routes>
      </Router>
    </div>
  );
}

export default App;
