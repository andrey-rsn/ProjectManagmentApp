import { apiSlice } from "../../app/api/apiSlice";

export const tasksApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        addTask: builder.mutation({
            query: (task) => ({
                url: `/api/v1/userTask`,
                method: 'POST',
                body: task
            })
        }),

        updateTask: builder.mutation({
            query: (task) => ({
                url: `/api/v1/userTask`,
                method: 'PUT',
                body: task
            })
        }),

        deleteTask: builder.mutation({
            query: (id) => ({
                url: `/api/v1/userTask/${id}`,
                method: 'DELETE'
            })
        }),

        getAllTasks: builder.query({
            query: (limit) => ({
                url: `/api/v1/userTask/all?limit=${limit}`,
                method: 'GET'
            })
        }),

        getTaskById: builder.query({
            query: (id) => ({
                url: `/api/v1/userTask/${id}`,
                method: 'GET'
            })
        }),

        getTasksByProjectId: builder.query({
            query: (payload) => ({
                url: `/api/v1/userTask/byProject?projectId=${payload.projectId}&limit=${payload.limit}`,
                method: 'GET'
            })
        })

    })
})

export const {
    useAddTaskMutation,
    useDeleteTaskMutation,
    useUpdateTaskMutation,
    useLazyGetAllTasksQuery,
    useLazyGetTaskByIdQuery,
    useLazyGetTasksByProjectIdQuery
} = tasksApiSlice