import { createApi } from '@reduxjs/toolkit/query/react';
import { fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import type { RecipeModel } from '@models'

const BASE_URL = import.meta.env.VITE_API_URL;

const recipesApi = createApi({
  reducerPath: 'recipes',
  baseQuery: fetchBaseQuery({ baseUrl: BASE_URL }),
  tagTypes: ['Recipe'],
  endpoints: (builder) => ({
    createRecipe: builder.mutation({
      invalidatesTags: ['Recipe'],
      query: (newRecipe: RecipeModel) => ({
        url: '/api/recipes',
        method: 'POST',
        body: {
          name: newRecipe.name,
          description: newRecipe.description,
          ingredients: newRecipe.ingredients,
          instructions: newRecipe.instructions,
          cookingTimeInMin: newRecipe.cookingTimeInMin,
          servings: newRecipe.servings
        }
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