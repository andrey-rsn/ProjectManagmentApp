export const formatTime = (time) =>{
    
    if(time === null){
        return '';
    }
    const date = new Date(time).toLocaleString().trim().split(',');

    return `${date[1]}, ${date[0]}`;
}