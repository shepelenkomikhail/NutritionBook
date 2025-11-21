import { configureStore } from '@reduxjs/toolkit';
import recipesApi from './apis/recipesApi.ts';
import authApi from './apis/authApi.ts';
import authReducer from './slices/authSlice.ts';

const store = configureStore({
  reducer: {
    auth: authReducer,
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
  useLazyGetRecipesQuery,
  useLazyGetRecipesByUserQuery
} = recipesApi;

export const { useRegisterMutation, useLoginMutation } = authApi;

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export default store;