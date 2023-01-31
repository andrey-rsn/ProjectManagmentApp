import { apiSlice } from "../../app/api/apiSlice";

export const workTimeApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        startWork: builder.mutation({
            query: (user_id) => ({
                url: `/api/v1/workTime/start?UserId=${user_id}`,
                method: 'POST'
            })
        }),

        endWork: builder.mutation({
            query: (user_id) => ({
                url: `/api/v1/workTime/end?UserId=${user_id}`,
                method: 'POST'
            })
        }),

        lastWorkTimeInfo: builder.query({
            query: (user_id) => ({
                url: `/api/v1/workTime/last/${user_id}`,
                method: 'GET'
            })
        })

    })
})

export const {
    useStartWorkMutation,
    useEndWorkMutation,
    useLazyLastWorkTimeInfoQuery
} = workTimeApiSlice