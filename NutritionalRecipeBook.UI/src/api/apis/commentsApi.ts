import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const BASE_URL = import.meta.env.VITE_API_URL;

const commentsApi = createApi({
  reducerPath: 'comments',
  baseQuery: fetchBaseQuery({ baseUrl: BASE_URL }),
  tagTypes: ['Comment'],
  endpoints: (builder) => ({
    createComment: builder.mutation<
      void,
      { recipeId: string; rating: number; content: string }
    >({
      invalidatesTags: ['Comment'],
      query: ({ recipeId, rating, content }) => ({
        url: `/api/comments`,
        method: 'POST',
        headers: localStorage.getItem('token')
          ? { Authorization: `Bearer ${localStorage.getItem('token')}` }
          : undefined,
        body: { recipeId, rating, content },
      }),
    }),
  }),
});

export const { useCreateCommentMutation } = commentsApi;
export default commentsApi;
