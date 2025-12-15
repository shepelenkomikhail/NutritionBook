import { Navigate, Outlet } from 'react-router-dom';

export function RedirectIfAuthenticated() {
  const token = localStorage.getItem('token');

  if (token) {
    return <Navigate to="/recipes" replace />;
  }

  return <Outlet />;
}
