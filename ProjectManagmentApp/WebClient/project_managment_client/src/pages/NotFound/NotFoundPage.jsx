import "./NotFoundPage.css"
import { NavLink } from "react-router-dom";

const NotFoundPage = () => {

    return (
        <div className="not-found-page">
            <div className="not-found-page__not-found-page-content not-found-page-content">
                <div className="not-found-page-content__text">
                    СТРАНИЦА НЕ НАЙДЕНА
                </div>
                <div className="not-found-page-content__reference">
                    <NavLink to='/main' >
                        На главную
                    </NavLink>
                </div>
            </div>
        </div>
    )
}

export default NotFoundPage;