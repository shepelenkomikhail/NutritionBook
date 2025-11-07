import type { RecipeModel } from '@models';
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
      query: (payload) => ({
        url: '/api/recipes',
        method: 'POST',
        body: payload
      })
    }),
    updateRecipe: builder.mutation({
      invalidatesTags: ['Recipe'],
      query: (updatedRecipe: RecipeModel) => ({
        url: `/api/recipes/${updatedRecipe.id}`,
        method: 'PUT',
        body: {
          name: updatedRecipe.name,
          description: updatedRecipe.description,
          ingredients: updatedRecipe.ingredients,
          instructions: updatedRecipe.instructions,
          cookingTimeInMin: updatedRecipe.cookingTimeInMin,
          servings: updatedRecipe.servings
        }
      })
    }),
  }),
});

export default recipesApi;