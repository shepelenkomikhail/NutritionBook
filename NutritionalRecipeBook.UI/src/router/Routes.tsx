import { createBrowserRouter, Navigate, RouteObject } from 'react-router-dom';
import { ConfirmEmail, Home, Recipe, Register, Login } from '@components';
import { App } from '@layout';
import { RedirectIfAuthenticated, RequireAuth } from '@router';
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
          { path: '/login', element: <Login /> },
          { path: '/confirm-email', element: <ConfirmEmail /> },
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
