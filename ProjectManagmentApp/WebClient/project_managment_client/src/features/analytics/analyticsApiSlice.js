import { apiSlice } from "../../app/api/apiSlice";

export const analyticsApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        getTasksAnalyticsByProject: builder.query({
            query: (projectId) => ({
                url: `/api/v1/analytics/tasks/${projectId}`,
                method: 'GET'
            })
        })
    })
})

export const {
    useLazyGetTasksAnalyticsByProjectQuery
} = analyticsApiSlice;