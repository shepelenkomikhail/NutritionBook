import { createRoot } from 'react-dom/client';
import { RouterProvider } from 'react-router-dom';
import { Provider } from 'react-redux'

import { router } from '@router';
import { ThemeProvider } from '@themes';
import store from '@api';

const container = document.getElementById('root');
const root = createRoot(container!);
root.render(
  <ThemeProvider>
    <Provider store={store}>
      <RouterProvider router={router} />
    </Provider>
  </ThemeProvider>,
);
