import { Content } from 'antd/es/layout/layout';
import Title from 'antd/es/typography/Title';
import { Button } from 'antd';

function Home() {
  const handleClick = () => {
    window.location.href = '/register';
  }

  return (
    <Content>
      <Title>Well, this looks like home 🏡</Title>
        <Content>API is working!</Content>
        <Button type={"primary"} onClick={handleClick}> Register</Button>
    </Content>
  );
}

export default Home;
