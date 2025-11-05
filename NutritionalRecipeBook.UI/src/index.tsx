import { createRoot } from 'react-dom/client';
import { RouterProvider } from 'react-router-dom';

import { router } from '@router';
import { rootStore, StoreContext } from '@stores';
import { darkTheme } from '@themes';

import { ConfigProvider } from 'antd';

const container = document.getElementById('root');
const root = createRoot(container!);
root.render(
  <ConfigProvider theme={darkTheme}>
    <StoreContext.Provider value={rootStore}>
      <RouterProvider router={router} />
    </StoreContext.Provider>
  </ConfigProvider>,
);
