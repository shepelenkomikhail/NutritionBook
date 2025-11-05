import { axiosInstance } from './setup';
import { AxiosResponse } from 'axios';

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

export const requests = {
  get: <T>(url: string) => axiosInstance.get<T>(url).then(responseBody),
  post: <T>(url: string, body: object) =>
    axiosInstance.post<T>(url, body).then(responseBody),
  put: <T>(url: string, body: object) =>
    axiosInstance.put<T>(url, body).then(responseBody),
  delete: <T>(url: string) => axiosInstance.delete<T>(url).then(responseBody),
};
