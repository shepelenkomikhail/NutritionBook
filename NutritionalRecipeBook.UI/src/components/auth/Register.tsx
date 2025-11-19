import { useEffect } from 'react';
import { EyeInvisibleOutlined, EyeTwoTone, MailOutlined, UserOutlined } from '@ant-design/icons';
import type { RegisterFormModel, RegisterModel } from '@models';
import { useAuthMutation } from '../../hooks';
import { lightInputStyle, lightLabelStyle } from '../../themes/modelStyles.ts';
import { ThemeToggleButton } from '../shared';
import HomeNavigateButton from './HomeNavigateButton';
import { Button, Form, Input, Layout } from 'antd';
import Title from 'antd/es/typography/Title';

const { Content } = Layout;

function Register() {
  const [form] = Form.useForm<RegisterFormModel>();
  const { execute, isLoading, isError } = useAuthMutation('register');

  const handleSubmit = async (values: RegisterFormModel) => {
    const registerData: RegisterModel = {
      username: values.username,
      password: values.password,
      email: values.email,
      name: values.name,
      surname: values.surname,
    };
    await execute(registerData);
  };

  const handleRedirect = () => {
    window.location.href = '/login';
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
          Registration
        </Title>
        <Form
          form={form}
          layout="vertical"
          onFinish={() => handleSubmit(form.getFieldsValue())}
          className="w-11/12 !p-4 rounded-lg bg-[var(--card)] text-[var(--fg)] border border-[var(--border)]"
        >
          <Form.Item
            name="username"
            label={<span style={lightLabelStyle}>Username</span>}
            rules={[
              { required: true, message: 'Please enter your username' },
              {
                pattern: /^[a-zA-Z0-9_]{3,20}$/,
                message:
                  'Username must be 3–20 characters and contain only letters, numbers, or underscores',
              },
            ]}
          >
            <Input prefix={<UserOutlined className="mr-2" />} placeholder="e.g. JohnSmith" style={lightInputStyle} />
          </Form.Item>

          <Form.Item
            name="email"
            label={<span style={lightLabelStyle}>Email</span>}
            rules={[
              { required: true, message: 'Please enter your email' },
              { type: 'email', message: 'Please enter a valid email address' },
            ]}
          >
            <Input prefix={<MailOutlined className="mr-2" />} placeholder="e.g. johnsmith@nixs.com" style={lightInputStyle} />
          </Form.Item>

          <Form.Item
            name="name"
            label={<span style={lightLabelStyle}>Name</span>}
            rules={[
              { required: true, message: 'Please enter your name' },
              {
                pattern: /^[A-Za-zÀ-ÖØ-öø-ÿ\s'-]{2,50}$/,
                message: 'Name must be 2–50 letters',
              },
            ]}
          >
            <Input placeholder="e.g. John" style={lightInputStyle} />
          </Form.Item>

          <Form.Item
            name="surname"
            label={<span style={lightLabelStyle}>Surname</span>}
            rules={[
              { required: true, message: 'Please enter your surname' },
              {
                pattern: /^[A-Za-zÀ-ÖØ-öø-ÿ\s'-]{2,50}$/,
                message: 'Surname must be 2–50 letters',
              },
            ]}
          >
            <Input placeholder="e.g. Smith" style={lightInputStyle} />
          </Form.Item>

          <Form.Item
            name="password"
            label={<span style={lightLabelStyle}>Password</span>}
            rules={[
              { required: true, message: 'Please enter your password' },
              {
                pattern: /^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_\-+={}[\]|:;"'<>,.?/~`]).{8,}$/,
                message: (
                  <>
                    Password must follow the rules:<br/>
                    - at least 8 characters long<br/>
                    - include one uppercase letter<br/>
                    - one number<br/>
                    - one special character
                  </>
                ),
              },
            ]}
          >
            <Input.Password placeholder="e.g. StrongPassword123!" style={lightInputStyle} />
          </Form.Item>

          <Form.Item
            name="repeatPassword"
            label={<span style={lightLabelStyle}>Repeat Password</span>}
            dependencies={['password']}
            hasFeedback
            rules={[
              { required: true, message: 'Please confirm your password' },
              ({ getFieldValue }) => ({
                validator(_, value) {
                  if (!value || getFieldValue('password') === value) {
                    return Promise.resolve();
                  }
                  return Promise.reject(new Error('Passwords do not match'));
                },
              }),
            ]}
          >
            <Input.Password
              placeholder="e.g. StrongPassword123!"
              style={lightInputStyle}
              iconRender={(visible) => (visible ? <EyeTwoTone /> : <EyeInvisibleOutlined />)}
            />
          </Form.Item>

          <Form.Item className="flex justify-center">
            <Button type="primary" htmlType="submit" block loading={isLoading} className="!w-[12rem] mt-4">
              {isLoading ? 'Registering...' : 'Register'}
            </Button>
          </Form.Item>

          <Form.Item className="flex justify-center !-mt-4 !mb-0">
            <a onClick={handleRedirect} className="self-center">
              Login
            </a>
          </Form.Item>
        </Form>
      </div>
    </Content>
  );
}

export default Register;