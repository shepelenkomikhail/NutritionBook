import { Toaster } from 'react-hot-toast';
import { Outlet } from 'react-router-dom';
import { MainLayout } from './MainLayout';
import { Content } from 'antd/es/layout/layout';

function App() {
  return (
    <MainLayout>
      <Toaster />
      <Content>
        <Outlet />
      </Content>
    </MainLayout>
  );
}

export default App;
