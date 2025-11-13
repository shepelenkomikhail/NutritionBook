import { MoonOutlined, SunOutlined } from '@ant-design/icons';
import { Button } from 'antd';
import { ThemeContext } from '../../layout/App.tsx';
import { useContext } from 'react';

function ThemeToggleButton(){
  const {theme, setTheme} = useContext(ThemeContext);
  const handleThemeToggle = () => {
    setTheme(theme == 'dark' ? 'light' : 'dark');
  }

  return (
   <Button
     icon={
       theme == 'dark' ? (
         <SunOutlined style={{ color: 'white', fontSize: '24px' }} />
       ) : (
         <MoonOutlined style={{ color: 'white', fontSize: '24px' }} />
       )
     }
     type="primary"
     className="!absolute !h-12 !w-12"
     style={{ top: '16px', left: '16px', borderRadius: '50%' }}
     onClick={() => handleThemeToggle()}
   />
 );
}

export default ThemeToggleButton;