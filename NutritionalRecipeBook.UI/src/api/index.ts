import { configureStore } from '@reduxjs/toolkit';
import recipesApi from './apis/recipesApi.ts';
import authApi from './apis/authApi.ts';
import commentsApi from './apis/commentsApi.ts';
import authReducer from './slices/authSlice.ts';
import userRecipesReducer from './slices/userRecipeSlice.ts';

const store = configureStore({
  reducer: {
    auth: authReducer,
    userRecipes: userRecipesReducer,
    [recipesApi.reducerPath]: recipesApi.reducer,
    [authApi.reducerPath]: authApi.reducer,
    [commentsApi.reducerPath]: commentsApi.reducer,
  },
  middleware: (getDefaultMiddleware) => {
    return getDefaultMiddleware().concat(
      recipesApi.middleware,
      authApi.middleware,
      commentsApi.middleware
    );
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
export const { 
  useCreateCommentMutation, 
  useLazyGetCommentsQuery, 
  useDeleteCommentMutation,
  useGetMyCommentsQuery,
  useLazyGetMyCommentsQuery,
} = commentsApi;

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export default store;