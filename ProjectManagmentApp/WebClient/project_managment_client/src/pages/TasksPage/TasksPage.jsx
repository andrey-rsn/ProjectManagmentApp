import { Route, Routes } from "react-router-dom";
import TasksForm from "../../components/TasksForm/TasksForm";
import TaskCardPage from "../TaskCardPage/TaskCardPage";
import "./TasksPage.css";

const TasksPage = () => {
    

    return (
        <div className="tasks-page">
            <TasksForm />
        </div>
    )
}

export default TasksPage;