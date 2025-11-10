import { configureStore } from '@reduxjs/toolkit';
import recipesApi from './apis/recipesApi.ts';

const store = configureStore({
  reducer: {
    [recipesApi.reducerPath]: recipesApi.reducer,
  },
  middleware: (getDefaultMiddleware) => {
    return getDefaultMiddleware().concat(recipesApi.middleware);
  }
});

export const { useCreateRecipeMutation, useUpdateRecipeMutation } = recipesApi;
export default store;