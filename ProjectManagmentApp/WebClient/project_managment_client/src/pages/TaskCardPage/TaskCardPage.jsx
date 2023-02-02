import { useEffect } from "react";
import { useParams } from "react-router-dom";
import TaskCardForm from "../../components/TaskCardForm/TaskCardFrom";


const TaskCardPage = (props) => {
    const params = useParams();

    
    return (
        <div className="task-card-page">
            <TaskCardForm taskId={params?.taskId}/>
        </div>
    )
}

export default TaskCardPage;