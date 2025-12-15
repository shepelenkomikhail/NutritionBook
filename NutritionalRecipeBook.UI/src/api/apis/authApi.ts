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
        credentials: "include",
      }),
      async onQueryStarted(_arg, { dispatch, queryFulfilled }) {
        try {
          const { data } = await queryFulfilled;
          if (!data?.token) return;

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
        credentials: "include",
      }),
      async onQueryStarted(_arg, { dispatch, queryFulfilled }) {
        try {
          const { data } = await queryFulfilled;
          if (!data?.token) return;

          const username = deriveUsername(data) || 'User';
          dispatch(setCredentials({ token: data.token, username }));
        } catch (error) {
          console.error('Login error:', error);
        }
      },
    }),
  }),
});

export default authApi;

function deriveUsername(
  data: RegisterResponseModel | { token: string } | undefined
): string | null {
  if (!data) return null;

  const direct = extractDirectUsername(data);
  if (direct) return direct;

  const alt = extractAlternativeUsername(data);
  if (alt) return alt;

  return extractUsernameFromToken(data);
}

function extractDirectUsername(data: unknown): string | null {
  const model = data as Partial<RegisterResponseModel>;

  return typeof model.username === "string" && model.username
    ? model.username
    : null;
}

function extractAlternativeUsername(data: unknown): string | null {
  const obj = data as Record<string, unknown>;
  const altKeys = ["userName", "email", "name", "surname"] as const;

  for (const key of altKeys) {
    const val = obj[key];
    if (typeof val === "string" && val) return val;
  }

  return null;
}

function extractUsernameFromToken(data: unknown): string | null {
  try {
    const token = (data as { token?: string }).token;
    if (!token) return null;

    const payloadJson = decodeJwtPayload(token);
    if (!payloadJson) return null;

    return (
      payloadJson["unique_name"] ||
      payloadJson["preferred_username"] ||
      payloadJson["name"] ||
      payloadJson["email"] ||
      payloadJson["sub"] ||
      null
    ) as string | null;
  } catch {
    return null;
  }
}

function decodeJwtPayload(token: string): Record<string, unknown> | null {
  const parts = token.split(".");
  if (parts.length < 2) return null;

  const base64 = parts[1]
    .replace(/-/g, "+")
    .replace(/_/g, "/");

  try {
    const json = atob(base64);

    return JSON.parse(json) as Record<string, unknown>;
  } catch {
    return null;
  }
}
