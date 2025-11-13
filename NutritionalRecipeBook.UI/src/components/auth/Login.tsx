import { useContext, useEffect } from 'react';
import { ThemeContext } from '../../layout/App.tsx';
import { Button, Form, Input, Layout } from 'antd';
import type { LoginFormModel } from '@models';
import { useAuthMutation } from '../../hooks';
import { ThemeToggleButton } from '../shared';
import Title from 'antd/es/typography/Title';
import { formContainerLightStyle, lightInputStyle, lightLabelStyle } from '../../themes/modelStyles.ts';
import { EyeInvisibleOutlined, EyeTwoTone, UserOutlined } from '@ant-design/icons';
const { Content  } = Layout;

function Login(){
  const {theme, } = useContext(ThemeContext);
  const isDark = theme === 'dark';
  const [form] = Form.useForm<LoginFormModel>();
  const { execute, isLoading, isError } = useAuthMutation("login");

  const handleSubmit = async (values: LoginFormModel) => {
    const loginData: LoginFormModel = {
      username: values.username,
      password: values.password,
    };

    await execute(loginData);
  };

  useEffect(() => {
    if (isError) {
      form.resetFields();
    }
  }, [isError, form])

  return (
    <Content
      className={`flex flex-col p-6 transition-all duration-300 items-center justify-center
        ${isDark ? 'bg-slate-900 text-gray-100' : 'text-gray-800'}`}
      style={{
        backgroundColor: isDark ? undefined : '#f9f5f0',
        minHeight: '100vh'
      }}
    >
      <div className={`flex flex-col p-6 transition-all duration-300 !min-h-2/3 !min-w-1/2 rounded-lg shadow-md items-center
                        ${isDark ? 'bg-slate-800' : 'bg-white'}`}
      >
        <ThemeToggleButton />
        <Title
          level={2}
          className={`${isDark ? '!text-gray-100' : '!text-gray-700'}`}
        >
          Registration Form
        </Title>
        <Form
          form={form}
          layout="vertical"
          onFinish={() => handleSubmit(form.getFieldsValue())}
          className="w-11/12 !p-4 rounded-lg"
          style={!isDark ? formContainerLightStyle : {}}
        >
          <Form.Item
            name="username"
            label={<span style={isDark ? {} : lightLabelStyle}>Username</span>}
            rules={[
              { required: true, message: 'Please enter your username' },
              {
                pattern: /^[a-zA-Z0-9_]{3,20}$/,
                message:
                  'Username must be 3–20 characters and contain only letters, numbers, or underscores',
              },
            ]}
          >
            <Input
              prefix={<UserOutlined className="mr-2" />}
              placeholder="e.g. JohnSmith"
              style={isDark ? {} : lightInputStyle}
            />
          </Form.Item>


          <Form.Item
            name="password"
            label={<span style={isDark ? {} : lightLabelStyle}>Password</span>}
            rules={[
              { required: true, message: 'Please enter your password' },
              {
                pattern: /^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_\-+={}[\]|:;"'<>,.?/~`]).{8,}$/,
                message:
                  'Password must be at least 8 characters long and include one uppercase letter, one number, and one special character',
              },
            ]}
          >
            <Input.Password
              prefix={<EyeTwoTone className="mr-2" />}
              iconRender={visible => (visible ? <EyeTwoTone /> : <EyeInvisibleOutlined />)}
              placeholder="e.g. StrongPassword123!"
              style={isDark ? {} : lightInputStyle}
            />
          </Form.Item>


          <Form.Item className="flex justify-center">
            <Button
              type="primary"
              htmlType="submit"
              block
              loading={isLoading}
              className="!w-[12rem] mt-4"
            >
              {isLoading ? 'Registering...' : 'Register'}
            </Button>
          </Form.Item>
        </Form>
      </div>
    </Content>
  );
}

export default Login;