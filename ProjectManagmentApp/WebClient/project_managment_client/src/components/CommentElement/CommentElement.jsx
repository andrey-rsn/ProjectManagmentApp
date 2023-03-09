import Avatar from '@mui/material/Avatar';
import './CommentElement.css';

const CommentElement = (props) => {
    const {img, name, creationDate, text} = props;
    /* console.log(img); */

    return (
        <div className='comment-element'>
            <div className='comment-element__img'>
                <Avatar alt="A" src="" />
            </div>
            <div className='comment-element__comment-info comment-info'>
                <div className='comment-info__comment-header comment-header'>
                    <p className='comment-header__user-name'>{name}</p>
                    <p className='comment-header__change-date'>{creationDate}</p>
                </div>
                <div className='comment-info__comment-text comment-text'>
                    <p className='comment-text__text'>{text}</p>
                </div>
            </div>
        </div>
    )
}

export default CommentElement;