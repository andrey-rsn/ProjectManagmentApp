import { apiSlice } from "../../app/api/apiSlice";

export const documentsApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        uploadDocument: builder.mutation({
            query: (payload) => ({
                url: `/api/v1/documents`,
                method: 'POST',
                formData: payload
            })
        })
    })
})

export const {
    useUploadDocumentMutation
} = documentsApiSlice;