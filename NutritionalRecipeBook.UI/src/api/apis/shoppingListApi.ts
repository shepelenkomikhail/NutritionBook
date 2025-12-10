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
      },
    }),
    updateShoppingList: builder.mutation<UserIngredientUnitOfMeasuresModel, UserIngredientUnitOfMeasuresModel>({
      invalidatesTags: ['ShoppingList'],
      query: (payload) => {
        const headers = getHeader();

        return {
          url: `/api/shoppinglist`,
          method: 'PUT',
          ...(headers ? { headers } : {}),
          body: payload,
        };
      },
    }),
    deleteShoppingListItem: builder.mutation<void, string>({
      invalidatesTags: ['ShoppingList'],
      query: (ingredientId) => {
        const headers = getHeader();

        return {
          url: `/api/shoppinglist/${ingredientId}`,
          method: 'DELETE',
          ...(headers ? { headers } : {}),
        };
      },
    }),
    clearShoppingList: builder.mutation<void, void>({
      invalidatesTags: ['ShoppingList'],
      query: () => {
        const headers = getHeader();

        return {
          url: `/api/shoppinglist/clear`,
          method: 'DELETE',
          ...(headers ? { headers } : {}),
        };
      },
    }),
    updateShoppingListItemIsBoughtStatus: builder.mutation<
      void,
      { itemId: string|undefined; isBought: boolean }
    >({
      invalidatesTags: ['ShoppingList'],
      query: ({ itemId, isBought }) => {
        const headers = {
          ...getHeader(),
          'Content-Type': 'application/json',
        };

        return {
          url: `/api/shoppinglist/item/${itemId}/bought`,
          method: 'PUT',
          headers,
          body: JSON.stringify(isBought),
        };
      },
    }),
    updateAllShoppingListItemsIsBoughtStatus: builder.mutation<void, {isBought:boolean}>({
      invalidatesTags: ['ShoppingList'],
      query: ({isBought}) => {
        const headers = {
          ...getHeader(),
          'Content-Type': 'application/json',
        };

        return {
          url: `/api/shoppinglist/bought`,
          method: 'PUT',
          ...(headers ? { headers } : {}),
          body: JSON.stringify(isBought),
        };
      },
    }),
    getPrintedShoppingList: builder.query<Blob, void>({
      query: () => {
        const headers = getHeader();

        return {
          url: `/api/shoppinglist/print`,
          method: 'GET',
          ...(headers ? { headers } : {}),
          responseType: 'blob',

          responseHandler: (response) => response.blob(),
        };
      }
    }),
  }),
});

export default shoppingListApi;