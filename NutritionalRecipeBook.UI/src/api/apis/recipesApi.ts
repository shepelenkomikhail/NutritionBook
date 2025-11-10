import { RecipePayload } from '@models';
import { createApi } from '@reduxjs/toolkit/query/react';
import { fetchBaseQuery } from '@reduxjs/toolkit/query/react';


const BASE_URL = import.meta.env.VITE_API_URL;

const recipesApi = createApi({
  reducerPath: 'recipes',
  baseQuery: fetchBaseQuery({ baseUrl: BASE_URL }),
  tagTypes: ['Recipe'],
  endpoints: (builder) => ({
    createRecipe: builder.mutation({
      invalidatesTags: ['Recipe'],
      query: (payload: RecipePayload) => ({
        url: '/api/recipes',
        method: 'POST',
        body: payload,
      }),
    }),
    updateRecipe: builder.mutation<void, { id: string; data: RecipePayload }>({
      invalidatesTags: ['Recipe'],
      query: ({ id, data }) => ({
        url: `/api/recipes/${id}`,
        method: 'PUT',
        body: data,
      }),
    }),
    deleteRecipe: builder.mutation({
      invalidatesTags: ['Recipe'],
      query: (id: string) => ({
        url: `/api/recipes/${id}`,
        method: 'DELETE',
      }),
    }),
  }),
});

export default recipesApi;