import "./AnalyticsForm.css";
import React from 'react';
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';
import { Pie } from 'react-chartjs-2';

ChartJS.register(ArcElement, Tooltip, Legend);

const data = {
    labels: ['Новые', 'В работе', 'Выполненные', 'Завершённые'],
    datasets: [
        {
            label: 'кол-во задач',
            data: [12, 19, 3, 5],
            backgroundColor: [
                'rgb(235, 235, 235)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)'
            ],
            borderColor: [
                'rgb(235, 235, 235)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)'
            ],
            borderWidth: 1,
        },
    ],
};


const AnalyticsForm = () => {


    return (
        <div className="analytics-form">
            <div className="analytics-form__header">
                <p>Аналитика проекта</p>
            </div>
            <div className="analytics-form__form-body form-body">
                <div className="form-body__diagram">
                    <Pie data={data} style={{marginBottom:'20px'}}/>
                    Статусы задач проекта
                </div>
            </div>
        </div>
    )
}

export default AnalyticsForm;