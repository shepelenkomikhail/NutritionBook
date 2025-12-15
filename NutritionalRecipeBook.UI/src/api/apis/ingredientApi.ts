import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import type { IngredientNutritionInfoModel, UnitOfMeasureModel } from '@models';
import { getHeader } from '@utils/getHeader';

const BASE_URL = import.meta.env.VITE_API_URL;

const ingredientsApi = createApi({
  reducerPath: 'ingredients',
  baseQuery: fetchBaseQuery({ baseUrl: BASE_URL }),
  tagTypes: ['Ingredient'],
  endpoints: (builder) => ({
    getIngredients: builder.query<IngredientNutritionInfoModel[], void>({
      providesTags: ['Ingredient'],
      query: () => {
        const headers = getHeader();

        return {
          url: `/api/ingredients`,
          method: 'GET',
          ...(headers ? { headers } : {}),
        };
      },
    }),
    getMeasurementUnits: builder.query<UnitOfMeasureModel[], { isLiquid?: boolean } | void>({
      query: (params) => {
        const headers = getHeader();
        return {
          url: `/api/ingredients/measures`,
          method: 'GET',
          ...(headers ? { headers } : {}),
          ...(params ? { params } : {}),
        };
      },
    }),
  }),
});

export default ingredientsApi;
