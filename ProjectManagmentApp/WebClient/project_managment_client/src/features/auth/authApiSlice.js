import { apiSlice } from "../../app/api/apiSlice";

export const authApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        login: builder.mutation({
            query: credentials => ({
                url: '/api/v1/identity/login',
                method: 'POST',
                body: { ...credentials }
            })
        }),
        register: builder.mutation({
            query: credentials => ({
                url: '/api/v1/identity/registration',
                method: 'POST',
                body: { ...credentials }
            })
        }),
        getPositions: builder.query({
            query: () => ({
                url: '/api/v1/positions',
                method: 'GET'
            })
        }),
        getUserInfo: builder.query({
            query: (userId) => ({
                url: `/api/v1/userInfo/${userId}`,
                method: 'GET'
            })
        })

    })
})

export const {
    useLoginMutation,
    useLazyGetPositionsQuery,
    useRegisterMutation,
    useLazyGetUserInfoQuery
} = authApiSlice