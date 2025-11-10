import { createRoot } from 'react-dom/client';
import { RouterProvider } from 'react-router-dom';
import { Provider } from 'react-redux'

import { router } from '@router';
import { darkTheme } from '@themes';
import { ConfigProvider } from 'antd';
import store from '@api';

const container = document.getElementById('root');
const root = createRoot(container!);
root.render(
  <ConfigProvider theme={darkTheme}>
    <Provider store={store}>
      <RouterProvider router={router} />
    </Provider>
  </ConfigProvider>,
);
