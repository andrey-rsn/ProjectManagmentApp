import Avatar from '@mui/material/Avatar';
import './CommentElement.css';

const CommentElement = (props) => {


    return (
        <div className='comment-element'>
            <div className='comment-element__img'>
                <Avatar alt="Andrey" src="/static/images/avatar/2.jpg" />
            </div>
            <div className='comment-element__comment-info comment-info'>
                <div className='comment-info__comment-header comment-header'>
                    <p className='comment-header__user-name'>Admin admin admin</p>
                    <p className='comment-header__change-date'>26.03.2023</p>
                </div>
                <div className='comment-info__comment-text comment-text'>
                    <p className='comment-text__text'>Lorem, ipsum dolor sit amet consectetur adipisicing elit. Mollitia commodi dicta animi veritatis, distinctio consectetur inventore, blanditiis est atque molestias accusantium porro, nostrum laboriosam maxime quas vel harum eveniet et?</p>
                </div>
            </div>
        </div>
    )
}

export default CommentElement;