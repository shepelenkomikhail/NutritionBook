import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { getHeader } from '@utils/getHeader';
import { UserIngredientUnitOfMeasuresModel } from '@models';

const BASE_URL = import.meta.env.VITE_API_URL;

const shoppingListApi = createApi({
  reducerPath: 'shoppingList',
  baseQuery: fetchBaseQuery({ baseUrl: BASE_URL }),
  tagTypes: ['ShoppingList'],
  endpoints: (builder) => ({
    getShoppingList: builder.query<UserIngredientUnitOfMeasuresModel, void>({
      providesTags: ['ShoppingList'],
      query: () => {
        const headers = getHeader();

        return {
          url: `/api/shoppinglist`,
          method: 'GET',
          ...(headers ? { headers } : {}),
        };
      },
    }),
    createShoppingList: builder.mutation<void, UserIngredientUnitOfMeasuresModel>({
      invalidatesTags: ['ShoppingList'],
      query: (payload) => {
        const headers = getHeader();

        return {
          url: `/api/shoppinglist`,
          method: 'POST',
          ...(headers ? { headers } : {}),
          body: payload,
        };
      }
    })
  }),
});

export default shoppingListApi;