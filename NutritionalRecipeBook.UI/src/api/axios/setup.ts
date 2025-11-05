import { rootStore } from '@stores';
import { toast } from '@utils/toast';

import axios, { AxiosError, AxiosResponse } from 'axios';

export const axiosInstance = axios.create();
axiosInstance.defaults.baseURL = import.meta.env.VITE_API_URL;
axiosInstance.interceptors.request.use((config) => {
  const accessToken = rootStore.dummyAuthStore.accessToken;
  if (accessToken) {
    config.headers.Authorization = `Bearer ${accessToken}`;
  }

  return config;
});
axiosInstance.interceptors.response.use(
  async (response) => {
    return response;
  },
  (error: AxiosError) => {
    if (!error.response) {
      toast('Network Error', { icon: '❌' });
      return Promise.reject(error);
    }

    const { /*data,*/ status } = error.response as AxiosResponse;
    switch (status) {
      case 400:
        toast('Client Error', { icon: '👎🏻' });
        break;
      case 401:
        toast('Unauthorized', { icon: '🚫' });
        break;
      case 403:
        toast('Forbidden', { icon: '🙅🏻' });
        break;
      case 404:
        toast('Not Found', { icon: '❔' });
        break;
      case 500:
        toast('Server Error', { icon: '❌' });
        break;
      default:
        toast(status.toString(), { icon: '❌' });
        break;
    }

    return Promise.reject(error);
  },
);
