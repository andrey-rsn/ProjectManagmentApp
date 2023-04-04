import { apiSlice } from "../../app/api/apiSlice";

export const documentsApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        uploadDocument: builder.mutation({
            query: (payload) => ({
                url: `/api/v1/documents`,
                method: 'POST',
                body: payload
            })
        }),
        getDocumentsByProject: builder.query({
            query: (projectId) => ({
                url: `/api/v1/documents/byProject/${projectId}`,
                method: 'GET'
            })
        }),
        deleteDocumentById: builder.mutation({
            query: (documentId) => ({
                url: `/api/v1/documents/${documentId}`,
                method: 'DELETE'
            })
        }),
    })
})

export const {
    useUploadDocumentMutation,
    useLazyGetDocumentsByProjectQuery,
    useDeleteDocumentByIdMutation
} = documentsApiSlice;