import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { RegisterModel, RegisterResponseModel } from '@models';

const BASE_URL = import.meta.env.VITE_API_URL;

const authApi = createApi({
  reducerPath: 'auth',
  baseQuery: fetchBaseQuery({ baseUrl: BASE_URL }),
  tagTypes: ['Auth'],
  endpoints: (builder) => ({
      register: builder.mutation<RegisterResponseModel, RegisterModel>({
        invalidatesTags: ['Auth'],
        query: (payload: RegisterModel) => ({
          url: '/api/auth',
          method: 'POST',
          body: payload,
        }),
      })
  })
});

export default authApi;