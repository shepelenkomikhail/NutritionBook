import { createBrowserRouter, Navigate, RouteObject } from 'react-router-dom';

import { Home } from '@components';

import { App } from '@layout';
import { RequireAuth } from '@router';

import Title from 'antd/es/typography/Title';

export const routes: RouteObject[] = [
  {
    path: '/',
    element: <App />,
    children: [
      {
        element: <RequireAuth />,
        children: [
          {
            path: '/requires-auth',
            element: <Title>Requires authentication</Title>,
          },
        ],
      },
      { path: '/', element: <Home /> },
      { path: '/home', element: <Home /> },
      { path: '*', element: <Navigate replace to="/not-found" /> },
    ],
  },
];

export const router = createBrowserRouter(routes);
