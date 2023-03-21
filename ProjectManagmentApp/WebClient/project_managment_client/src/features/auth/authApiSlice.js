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
        getPositions: builder.query({
            query: () => ({
                url: '/api/v1/positions',
                method: 'GET'
            })
        })

    })
})

export const {
    useLoginMutation,
    useLazyGetPositionsQuery
} = authApiSlice