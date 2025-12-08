import { RecipePayload, PagedResult, RecipeModel } from '@models';
import { createApi } from '@reduxjs/toolkit/query/react';
import { fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const BASE_URL = import.meta.env.VITE_API_URL;

const getHeader = () =>
  localStorage.getItem('token')
    ? { Authorization: `Bearer ${localStorage.getItem('token')}` }
    : undefined;

const recipesApi = createApi({
  reducerPath: 'recipes',
  baseQuery: fetchBaseQuery({ baseUrl: BASE_URL }),
  tagTypes: ['Recipe'],
  endpoints: (builder) => ({
    createRecipe: builder.mutation({
      invalidatesTags: ['Recipe'],
      query: (payload: RecipePayload) => {
        const headers = getHeader();
        return {
          url: '/api/recipes',
          method: 'POST',
          ...(headers ? { headers } : {}),
          body: payload,
        };
      },
    }),
    updateRecipe: builder.mutation<void, { id: string | undefined; data: RecipePayload }>({
      invalidatesTags: ['Recipe'],
      query: ({ id, data }) => {
        const headers = getHeader();
        return {
          url: `/api/recipes/${id}`,
          method: 'PUT',
          ...(headers ? { headers } : {}),
          body: data,
        };
      },
    }),
    deleteRecipe: builder.mutation({
      invalidatesTags: ['Recipe'],
      query: (id: string) => {
        const headers = getHeader();
        return {
          url: `/api/recipes/${id}`,
          method: 'DELETE',
          ...(headers ? { headers } : {}),
        };
      },
    }),
    getRecipes: builder.query<PagedResult<RecipeModel>, {
      search?: string;
      pageNumber?: number;
      pageSize?: number;
      minCookingTimeInMin?: number;
      maxCookingTimeInMin?: number;
      minServings?: number;
      maxServings?: number;
      minCaloriesPerServing?: number;
      maxCaloriesPerServing?: number;
    } | void>({
      providesTags: ['Recipe'],
      query: (params) => {
        const headers = getHeader();
        return {
          url: '/api/recipes',
          method: 'GET',
          ...(headers ? { headers } : {}),
          ...(params ? { params } : {}),
        };
      },
    }),
    getRecipeById: builder.query<RecipePayload, string>({
      providesTags: ['Recipe'],
      query: (id: string) => {
        const headers = getHeader();
        return {
          url: `/api/recipes/${id}`,
          method: 'GET',
          ...(headers ? { headers } : {}),
        };
      }
    }),
    getRecipesByUser: builder.query<PagedResult<RecipeModel>, {
      search?: string;
      pageNumber?: number;
      pageSize?: number;
      minCookingTimeInMin?: number;
      maxCookingTimeInMin?: number;
      minServings?: number;
      maxServings?: number;
      minCaloriesPerServing?: number;
      maxCaloriesPerServing?: number;
    } | void>({
      providesTags: ['Recipe'],
      query: (params)=>  {
        const headers = getHeader();
        return {
          url: `/api/recipes/mine`,
          method: 'GET',
          ...(headers ? { headers } : {}),
          ...(params ? { params } : {}),
        };
      }
    }),
    getFavoriteRecipes: builder.query<PagedResult<RecipeModel>, {
      search?: string;
      pageNumber?: number;
      pageSize?: number;
      minCookingTimeInMin?: number;
      maxCookingTimeInMin?: number;
      minServings?: number;
      maxServings?: number;
      minCaloriesPerServing?: number;
      maxCaloriesPerServing?: number;
    } | void>({
      providesTags: ['Recipe'],
      query: (params) => {
        const headers = getHeader();
        return {
          url: '/api/recipes/favorite',
          method: 'GET',
          ...(headers ? { headers } : {}),
          ...(params ? { params } : {}),
        };
      },
    }),
    markFavoriteRecipe: builder.mutation({
      invalidatesTags: ['Recipe'],
      query: (params) => {
        const headers = getHeader();
        return {
          url: `/api/recipes/favorite/${params.id}`,
          method: 'POST',
          ...(headers ? { headers } : {}),
          ...(params ? { params } : {})
        };
      },
    }),
    unmarkFavoriteRecipe: builder.mutation({
      invalidatesTags: ['Recipe'],
      query: (params) => {
        const headers = getHeader();
        return {
          url: `/api/recipes/favorite/${params.id}`,
          method: 'DELETE',
          ...(headers ? { headers } : {}),
        };
      },
    }),
  })
});

export default recipesApi;
