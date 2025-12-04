import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import type { CommentModel, PagedResult } from '@models';

const BASE_URL = import.meta.env.VITE_API_URL;

const getHeader = () =>
  localStorage.getItem('token')
    ? { Authorization: `Bearer ${localStorage.getItem('token')}` }
    : undefined;

const commentsApi = createApi({
  reducerPath: 'comments',
  baseQuery: fetchBaseQuery({ baseUrl: BASE_URL }),
  tagTypes: ['Comment'],
  endpoints: (builder) => ({
    getComments: builder.query<CommentModel[] | PagedResult<CommentModel>, { recipeId?: string } | void>({
      providesTags: ['Comment'],
      query: (params) => {
        const headers = getHeader();

        return {
          url: `/api/comments`,
          method: 'GET',
          ...(headers ? { headers } : {}),
          ...(params ? { params } : {}),
        };
      },
    }),
    getMyComments: builder.query<CommentModel[] | PagedResult<CommentModel>, { recipeId?: string } | void>({
      providesTags: ['Comment'],
      query: (params) => {
        const headers = getHeader();

        return {
          url: `/api/comments/mine`,
          method: 'GET',
          ...(headers ? { headers } : {}),
          ...(params ? { params } : {}),
        };
      },
    }),
    createComment: builder.mutation<
      void,
      { recipeId: string; rating: number; content: string }
    >({
      invalidatesTags: ['Comment'],
      query: ({ recipeId, rating, content }) => {
        const headers = getHeader();
        return {
          url: `/api/comments`,
          method: 'POST',
          ...(headers ? { headers } : {}),
          body: { recipeId, rating, content },
        };
      },
    }),
    deleteComment: builder.mutation<void, { commentId: string }>({
      invalidatesTags: ['Comment'],
      query: ({ commentId }) => {
        const headers = getHeader();
        return {
          url: `/api/comments`,
          method: 'DELETE',
          ...(headers ? { headers } : {}),
          params: { commentId },
        };
      },
    }),
  }),
});

export default commentsApi;
