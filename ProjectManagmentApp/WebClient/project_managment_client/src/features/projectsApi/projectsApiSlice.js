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
        })
    })
})

export const {
    useLazyGetProjectsByUserIdQuery,
    useLazyGetProjectsByUserAndProjectIdQuery
} = projectsApiSlice