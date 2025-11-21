import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import type { LoginFormModel, RegisterModel, RegisterResponseModel } from '@models';
import { setCredentials } from '../slices/authSlice';

const BASE_URL = import.meta.env.VITE_API_URL;

const authApi = createApi({
  reducerPath: 'authApi',
  baseQuery: fetchBaseQuery({
    baseUrl: BASE_URL,
  }),
  tagTypes: ['Auth'],
  endpoints: (builder) => ({
    register: builder.mutation<RegisterResponseModel, RegisterModel>({
      invalidatesTags: ['Auth'],
      query: (payload) => ({
        url: '/api/auth',
        method: 'POST',
        body: payload,
      }),
      async onQueryStarted(_arg, { dispatch, queryFulfilled }) {
        try {
          const { data } = await queryFulfilled;
          if (!data?.token) return;

          const deriveUsername = (d: any): string | null => {
            const fromFields = d?.username || d?.userName || d?.email || d?.name || d?.surname;
            if (fromFields) return fromFields as string;
            try {
              const [, payload] = (d.token as string).split('.');
              if (payload) {
                const decoded = JSON.parse(atob(payload.replace(/-/g, '+').replace(/_/g, '/')));
                return (
                  decoded?.unique_name ||
                  decoded?.preferred_username ||
                  decoded?.name ||
                  decoded?.email ||
                  decoded?.sub ||
                  null
                );
              }
            } catch {}
            return null;
          };

          const username = deriveUsername(data) || 'User';
          dispatch(setCredentials({ token: data.token, username }));
        } catch (error) {
          console.error('Register error:', error);
        }
      },
    }),
    login: builder.mutation<RegisterResponseModel, LoginFormModel>({
      invalidatesTags: ['Auth'],
      query: (payload) => ({
        url: '/api/auth/login',
        method: 'POST',
        body: payload,
      }),
      async onQueryStarted(_arg, { dispatch, queryFulfilled }) {
        try {
          const { data } = await queryFulfilled;
          if (!data?.token) return;

          const deriveUsername = (d: any): string | null => {
            const fromFields = d?.username || d?.userName || d?.email || d?.name || d?.surname;
            if (fromFields) return fromFields as string;
            try {
              const [, payload] = (d.token as string).split('.');
              if (payload) {
                const decoded = JSON.parse(atob(payload.replace(/-/g, '+').replace(/_/g, '/')));
                return (
                  decoded?.unique_name ||
                  decoded?.preferred_username ||
                  decoded?.name ||
                  decoded?.email ||
                  decoded?.sub ||
                  null
                );
              }
            } catch {}
            return null;
          };

          const username = deriveUsername(data) || 'User';
          dispatch(setCredentials({ token: data.token, username }));
        } catch (error) {
          console.error('Login error:', error);
        }
      },
    }),
  }),
});

export const { useRegisterMutation, useLoginMutation } = authApi;
export default authApi;