import Header from '../../components/Header/Header';
import MainInfoForm from '../../components/MainInfoForm/MainInfoForm';
import './StartPage.css';

const StartPage = () => {


    return (
        <div className="start-page">
            <div className="start-page__header">
                <Header />
            </div>
            <div className="start-page__content">
                <MainInfoForm/>
            </div>
        </div >
    )
}

export default StartPage;