import { observer } from 'mobx-react';
import { useState } from 'react';
import { createContext, useEffect } from 'react';
import { Toaster } from 'react-hot-toast';
import { Outlet } from 'react-router-dom';
import { ThemeContextType } from '@models';
import { MainLayout } from './MainLayout';
import { Content } from 'antd/es/layout/layout';

export const ThemeContext = createContext<ThemeContextType>({
  theme: 'light',
  setTheme: () => {},
} as ThemeContextType );

function App() {
  const getPreferredTheme = (): 'light' | 'dark' =>
    window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';

  const [theme, setTheme] = useState(getPreferredTheme());

  useEffect(() => {
    document.body.setAttribute('data-theme', theme);
  }, [theme]);

  return (
    <ThemeContext.Provider value={{theme, setTheme}}>
      <MainLayout>
        <Toaster />
        <Content>
          <Outlet />
        </Content>
      </MainLayout>
    </ThemeContext.Provider>
  );
}

const ObservedApp = observer(App);
export default ObservedApp;
