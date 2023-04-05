import "./AnalyticsForm.css";
import React from 'react';
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';
import { Pie } from 'react-chartjs-2';
import { useState } from "react";
import { useLazyGetTasksAnalyticsByProjectQuery } from "../../features/analytics/analyticsApiSlice";
import { useParams } from "react-router-dom";
import { useEffect } from "react";


ChartJS.register(ArcElement, Tooltip, Legend);




const AnalyticsForm = () => {
    const [tasksAnalytics, setTasksAnalytics] = useState([1, 0, 0, 0]);
    const { projectId } = useParams();
    const [fetchTasksAnalytics, { isLoading: isTasksAnalyticsLoading, isSuccess: isTasksAnalyticsLoaded }] = useLazyGetTasksAnalyticsByProjectQuery();

    useEffect(() => {
        const load = async () => {
            await loadData();
        }
        load();
    }, [])

    const loadData = async () => {
        await fetchTasksAnalytics(projectId).unwrap().then(data => setTasksAnalytics(data)).catch(err => console.log(err));
    }

    const data = {
        labels: ['Новые', 'В работе', 'Выполненные', 'Завершённые'],
        datasets: [
            {
                label: 'кол-во задач',
                data: tasksAnalytics,
                backgroundColor: [
                    'rgb(125, 125, 125, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)'
                ],
                borderColor: [
                    'rgb(125, 125, 125, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)'
                ],
                borderWidth: 1,
            },
        ],
    };

    const emptyData = {
        labels: ['Данные отсутствуют'],
        datasets: [
            {
                label: '',
                data: [1],
                backgroundColor: [
                    'rgb(125, 125, 125, 0.2)'
                ],
                borderColor: [
                    'rgb(125, 125, 125, 1)'
                ],
                borderWidth: 1,
            },
        ],
    };

    return (
        <div className="analytics-form">
            <div className="analytics-form__header">
                <p>Аналитика проекта</p>
            </div>
            <div className="analytics-form__form-body form-body">
                <div className="form-body__diagram">
                    <Pie data={isTasksAnalyticsLoading || !isTasksAnalyticsLoaded? emptyData:data} style={{ marginBottom: '20px' }} />
                    Статусы задач проекта
                </div>
            </div>
        </div>
    )
}

export default AnalyticsForm;