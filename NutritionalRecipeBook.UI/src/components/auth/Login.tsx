import { useEffect } from 'react';
import { Button, Form, Input, Layout } from 'antd';
import type { LoginFormModel } from '@models';
import { useAuthMutation } from '@hooks';
import { ThemeToggleButton } from '../shared';
import HomeNavigateButton from './HomeNavigateButton';
import Title from 'antd/es/typography/Title';
import { lightLabelStyle } from '../../themes/modelStyles.ts';
import { UserOutlined } from '@ant-design/icons';

const { Content } = Layout;

function Login() {
  const [form] = Form.useForm<LoginFormModel>();
  const { execute, isLoading, isError } = useAuthMutation('login');

  const handleSubmit = async (values: LoginFormModel) => {
    await execute({ username: values.username, password: values.password });
  };

  const handleRedirect = () => {
    window.location.href = '/register';
  };

  useEffect(() => {
    if (isError) {
      form.resetFields();
    }
  }, [isError, form]);

  return (
    <Content className="flex flex-col p-6 transition-all duration-300 items-center justify-center text-[var(--fg)] bg-[var(--bg)] min-h-screen">
      <div className="flex flex-col p-6 transition-all duration-300 !min-h-2/3 !min-w-1/2 ds-card shadow-md items-center">
        <ThemeToggleButton />
        <HomeNavigateButton />
        <Title level={2} className="!text-[var(--fg)]">
          Login
        </Title>
        <Form
          form={form}
          layout="vertical"
          onFinish={handleSubmit}
          className="w-11/12 !p-4 rounded-lg bg-[var(--card)] text-[var(--fg)] border border-[var(--border)]"
        >
          <Form.Item
            name="username"
            label={<span style={lightLabelStyle}>Username</span>}
            rules={[
              { required: true, message: 'Please enter your username' },
              {
                pattern: /^[a-zA-Z0-9_]{3,20}$/,
                message: ""
              },
            ]}
          >
            <Input prefix={<UserOutlined className="mr-2" />} placeholder="e.g. JohnSmith"  />
          </Form.Item>

          <Form.Item
            name="password"
            label={<span style={lightLabelStyle}>Password</span>}
            rules={[
              { required: true, message: 'Please enter your password' },
              {
                pattern: /^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_\-+={}[\]|:;"'<>,.?/~`]).{8,}$/,
                message: ""
              },
            ]}
          >
            <Input.Password placeholder="e.g. StrongPassword123!" />
          </Form.Item>

          <Form.Item className="flex justify-center">
            <Button
              type="primary"
              htmlType="submit"
              block
              loading={isLoading}
              className="!w-[12rem] mt-4"
            >
              {isLoading ? 'Login...' : 'Login'}
            </Button>
          </Form.Item>

          <Form.Item className="flex justify-center !-mt-4 !mb-0">
            <a onClick={handleRedirect} className="self-center">
              Register
            </a>
          </Form.Item>
        </Form>
      </div>
    </Content>
  );
}

export default Login;