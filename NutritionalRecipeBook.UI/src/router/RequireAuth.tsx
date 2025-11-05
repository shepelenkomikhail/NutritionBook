import { Navigate, Outlet, useLocation } from 'react-router-dom';

import { useStore } from '@stores';

export function RequireAuth() {
  const {
    dummyAuthStore: { isLoggedIn },
  } = useStore();
  const location = useLocation();

  if (!isLoggedIn) {
    return <Navigate to="/" state={{ from: location }} />;
  }

  return <Outlet />;
}
