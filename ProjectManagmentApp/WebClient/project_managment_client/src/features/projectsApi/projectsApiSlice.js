import { apiSlice } from "../../app/api/apiSlice";

export const projectsApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        getProjectsByUserId: builder.query({
            query: (user_id) => ({
                url: `api/v1/projects/byUser/${user_id}`,
                method: 'GET'
            })
        }),
        getProjectsByUserAndProjectId: builder.query({
            query: (payload) => ({
                url: `api/v1/projects/byUserAndProject?userId=${payload.userId}&projectId=${payload.projectId}`,
                method: 'GET'
            })
        }),
        getEmployeesAttachedToProject: builder.query({
            query: (payload) => ({
                url: `/api/v1/projects/${payload.projectId}/attachedEmployees`,
                method: 'GET'
            })
        }),
        getEmployeesNotAttachedToProject: builder.query({
            query: (payload) => ({
                url: `/api/v1/projects/${payload.projectId}/notAttachedEmployees`,
                method: 'GET'
            })
        }),
        updateProject: builder.mutation({
            query: (project) => ({
                url: `/api/v1/projects`,
                method: 'PUT',
                body: project
            })
        })

    })
})

export const {
    useLazyGetProjectsByUserIdQuery,
    useLazyGetProjectsByUserAndProjectIdQuery,
    useLazyGetEmployeesAttachedToProjectQuery,
    useLazyGetEmployeesNotAttachedToProjectQuery,
    useUpdateProjectMutation
} = projectsApiSlice