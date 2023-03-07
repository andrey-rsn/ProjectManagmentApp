export const formatTime = (time) =>{
    
    if(time === null){
        return '';
    }
    const date = new Date(time).toLocaleString().trim().split(',');

    return `${date[0]}, ${date[1]}`;
}