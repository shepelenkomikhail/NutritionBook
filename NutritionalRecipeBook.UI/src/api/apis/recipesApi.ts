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
    getRecipes: builder.query({
      providesTags: ['Recipe'],
      query: (params?: {
        search?: string;
        pageNumber?: number;
        pageSize?: number;
        minCookingTimeInMin?: number;
        maxCookingTimeInMin?: number;
        minServings?: number;
        maxServings?: number;
      }) => ({
        url: '/api/recipes',
        method: 'GET',
        params,
      }),
    }),
    getRecipeById: builder.query({
      providesTags: ['Recipe'],
      query: (id: string) => ({
        url: `/api/recipes/${id}`,
        method: 'GET',
      })
    }),
    getRecipesByUser: builder.query({
      providesTags: ['Recipe'],
      query: (params?: {
        search?: string;
        pageNumber?: number;
        pageSize?: number;
        minCookingTimeInMin?: number;
        maxCookingTimeInMin?: number;
        minServings?: number;
        maxServings?: number;
      })=>  ({
        url: `/api/users/recipes`,
        method: 'GET',
        headers: localStorage.getItem('token')
          ? { Authorization: `Bearer ${localStorage.getItem('token')}` }
          : undefined,
        params,
      })
    })
  })
});

export default recipesApi;
