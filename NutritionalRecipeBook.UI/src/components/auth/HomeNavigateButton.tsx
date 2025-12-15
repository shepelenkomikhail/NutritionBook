import { LeftOutlined } from '@ant-design/icons';
import { Button } from 'antd';
import { useNavigate } from 'react-router-dom';

function HomeNavigateButton() {
  const navigate = useNavigate();

  const handleClick = () => navigate('/');

  return (
    <Button
      icon={<LeftOutlined style={{ fontSize: 18 }} />}
      type="default"
      shape="circle"
      aria-label="Go to home"
      onClick={handleClick}
      className="!absolute !h-12 !w-12"
      style={{
        top: '16px',
        left: '64px',
        marginLeft: '24px',
        borderRadius: '50%',
        color: 'var(--fg)',
        borderColor: 'var(--border)',
        backgroundColor: 'var(--bg-muted)'
      }}
    />
  );
}

export default HomeNavigateButton;
