import { Content, Header } from 'antd/es/layout/layout';
import Title from 'antd/es/typography/Title';
import { Button } from 'antd';

function Home() {
  const handleRegisterClick = () => {
    window.location.href = '/register';
  }

  const handleLoginClick = () => {
    window.location.href = '/login';
  }

  return (
    <>
      <Header className={"flex"}>
        <Title>Well, this looks like home 🏡</Title>

      </Header>
      <Content>
          <Content>API is working!</Content>
          <Button type={"primary"} className={"mr-4"} onClick={handleRegisterClick}> Register</Button>
        <Button type={"primary"} onClick={handleLoginClick}> Login</Button>
      </Content>
    </>
  );
}

export default Home;
