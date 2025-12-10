import { configureStore } from '@reduxjs/toolkit';
import recipesApi from './apis/recipesApi.ts';
import authApi from './apis/authApi.ts';
import commentsApi from './apis/commentsApi.ts';
import authReducer from './slices/authSlice.ts';
import userRecipesReducer from './slices/userRecipeSlice.ts';
import ingredientsApi from './apis/ingredientApi.ts';
import shoppingListApi from './apis/shoppingListApi.ts';

const store = configureStore({
  reducer: {
    auth: authReducer,
    userRecipes: userRecipesReducer,
    [recipesApi.reducerPath]: recipesApi.reducer,
    [authApi.reducerPath]: authApi.reducer,
    [commentsApi.reducerPath]: commentsApi.reducer,
    [ingredientsApi.reducerPath]: ingredientsApi.reducer,
    [shoppingListApi.reducerPath]: shoppingListApi.reducer,
  },
  middleware: (getDefaultMiddleware) => {
    return getDefaultMiddleware().concat(
      recipesApi.middleware,
      authApi.middleware,
      commentsApi.middleware,
      ingredientsApi.middleware,
      shoppingListApi.middleware
    );
  }
});

export const { useRegisterMutation, useLoginMutation } = authApi;

export const { useGetIngredientsQuery, useGetMeasurementUnitsQuery } = ingredientsApi;

export const {
  useGetShoppingListQuery,
  useCreateShoppingListMutation,
  useUpdateShoppingListMutation,
  useDeleteShoppingListItemMutation,
  useClearShoppingListMutation,
  useUpdateAllShoppingListItemsIsBoughtStatusMutation,
  useUpdateShoppingListItemIsBoughtStatusMutation
} = shoppingListApi;

export const {
  useCreateRecipeMutation,
  useUpdateRecipeMutation,
  useDeleteRecipeMutation,
  useLazyGetRecipeByIdQuery,
  useLazyGetRecipesQuery,
  useLazyGetRecipesByUserQuery,
  useLazyGetFavoriteRecipesQuery,
  useMarkFavoriteRecipeMutation,
  useUnmarkFavoriteRecipeMutation,
} = recipesApi;

export const {
  useCreateCommentMutation,
  useLazyGetCommentsQuery,
  useDeleteCommentMutation,
  useLazyGetMyCommentsQuery,
} = commentsApi;

export type RootState = ReturnType<typeof store.getState>;

export default store;