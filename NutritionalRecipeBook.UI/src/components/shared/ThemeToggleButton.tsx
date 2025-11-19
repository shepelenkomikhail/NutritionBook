import { MoonOutlined, SunOutlined } from '@ant-design/icons';
import { Button } from 'antd';
import { useTheme } from '@themes';

type Props = {
  variant?: 'inline' | 'floating';
  className?: string;
};

function ThemeToggleButton({ variant = 'floating', className }: Props){
  const { mode, set } = useTheme();
  const handleThemeToggle = () => {
    set(mode === 'dark' ? 'light' : 'dark');
  };

  const icon = mode === 'dark'
    ? <SunOutlined style={{ fontSize: 20 }} />
    : <MoonOutlined style={{ fontSize: 20 }} />;

  if (variant === 'inline') {
    return (
      <Button
        icon={icon}
        type="default"
        shape="circle"
        className={className}
        aria-label="Toggle theme"
        onClick={handleThemeToggle}
        style={{ color: 'var(--fg)', borderColor: 'var(--border)', backgroundColor: 'var(--bg-muted)' }}
      />
    );
  }

  return (
    <Button
      icon={icon}
      type="primary"
      className={`!absolute !h-12 !w-12 ${className ?? ''}`}
      style={{ top: '16px', left: '16px', borderRadius: '50%' }}
      aria-label="Toggle theme"
      onClick={handleThemeToggle}
    />
  );
}

export default ThemeToggleButton;