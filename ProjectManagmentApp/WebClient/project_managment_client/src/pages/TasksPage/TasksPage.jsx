import { Route, Routes } from "react-router-dom";
import TasksForm from "../../components/TasksForm/TasksForm";
import TaskCardPage from "../TaskCardPage/TaskCardPage";
import "./TasksPage.css";

const TasksPage = (props) => {
    const { projectId } = props;

    return (
        <div className="tasks-page">
            <TasksForm projectId={projectId}/>
        </div>
    )
}

export default TasksPage;