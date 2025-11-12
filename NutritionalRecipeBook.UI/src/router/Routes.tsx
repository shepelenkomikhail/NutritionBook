import { createBrowserRouter, Navigate, RouteObject } from 'react-router-dom';
import { Home, Recipe, Register } from '@components';
import { RequireAuth, RedirectIfAuthenticated } from '@router';
import { App } from '@layout';
import Title from 'antd/es/typography/Title';

export const routes: RouteObject[] = [
  {
    path: '/',
    element: <App />,
    children: [
      {
        element: <RedirectIfAuthenticated />,
        children: [
          { path: '/', element: <Home /> },
          { path: '/register', element: <Register /> },
        ],
      },
      {
        element: <RequireAuth />,
        children: [
          { path: '/recipes', element: <Recipe /> },
          { path: '/requires-auth', element: <Title>Requires authentication</Title> },
        ],
      },
      { path: '*', element: <Navigate replace to="/" /> },
    ],
  },
];

export const router = createBrowserRouter(routes);
