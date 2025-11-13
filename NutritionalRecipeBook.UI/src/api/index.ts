import { configureStore } from '@reduxjs/toolkit';
import recipesApi from './apis/recipesApi.ts';
import authApi from './apis/authApi.ts';

const store = configureStore({
  reducer: {
    [recipesApi.reducerPath]: recipesApi.reducer,
    [authApi.reducerPath]: authApi.reducer,
  },
  middleware: (getDefaultMiddleware) => {
    return getDefaultMiddleware().concat(recipesApi.middleware, authApi.middleware);
  }
});

export const {
  useCreateRecipeMutation,
  useUpdateRecipeMutation,
  useDeleteRecipeMutation,
  useLazyGetRecipeByIdQuery,
  useLazyGetRecipesQuery
} = recipesApi;

export const { useRegisterMutation } = authApi;

export default store;