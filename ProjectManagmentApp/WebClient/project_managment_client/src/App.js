import './App.css';
import LoginPage from './pages/LoginPage/LoginPage';
import {BrowserRouter as Router, Route, Routes} from 'react-router-dom';
import MainPage from './pages/MainPage/MainPage';

function App() {
  return (
    <div className="App">
      <Router>
        <Routes>
          <Route path="/" element={<LoginPage/>}/>
          <Route path="/main" element={<MainPage/>}/>
        </Routes>
      </Router>
    </div>
  );
}

export default App;
