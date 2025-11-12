import { useContext } from 'react';
import { useAuthMutation } from '../../hooks';
import type { RegisterModel, RegisterFormModel } from '@models';
import { EyeInvisibleOutlined, EyeTwoTone, MailOutlined, UserOutlined } from '@ant-design/icons';
import { formContainerLightStyle, lightInputStyle, lightLabelStyle, } from '../../themes/modelStyles.ts';
import { Button, Form, Input, Layout } from 'antd';
import Title from 'antd/es/typography/Title';
import { ThemeContext } from '../../layout/App.tsx';
const { Content  } = Layout;

function Register(){
  const {theme, } = useContext(ThemeContext);
  const isDark = theme === 'dark';
  const [form] = Form.useForm<RegisterFormModel>();
  const { execute, isLoading } = useAuthMutation();

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

  return (
    <Content
      className={`flex flex-col p-6 transition-all duration-300 items-center justify-center
        ${isDark ? 'bg-slate-900 text-gray-100' : 'text-gray-800'}`}
      style={{
        backgroundColor: isDark ? undefined : '#f9f5f0',
        minHeight: '100vh'
      }}
    >
      <div className={`flex flex-col p-6 transition-all duration-300 !min-h-2/3 !min-w-1/2 
                        rounded-lg bg-gray-800 shadow-md items-center`}
      >
        <Title level={2}>Registration Form</Title>
        <Form
          form={form}
          layout="vertical"
          onFinish={() => handleSubmit(form.getFieldsValue())}
          className="w-11/12"
          style={ !isDark ? formContainerLightStyle : {}}
        >
          <Form.Item
            name="username"
            label={<span style={isDark ? {} : lightLabelStyle}>Username</span>}
            rules={[{ required: true, message: 'Please enter the username' }]}
          >
            <Input
              prefix={<UserOutlined className="mr-2" />}
              placeholder="e.g. JohnSmith"
              style={isDark ? {} : lightInputStyle}
            />
          </Form.Item>

          <Form.Item
            name="email"
            label={<span style={isDark ? {} : lightLabelStyle}>Email</span>}
            rules={[{ required: true, message: 'Please enter your email' }]}
          >
            <Input
              prefix={<MailOutlined className="mr-2" />}
              placeholder="e.g. johnsmith@nixs.com"
              style={isDark ? {} : lightInputStyle}
            />
          </Form.Item>
          <Form.Item

            name="name"
            label={<span style={isDark ? {} : lightLabelStyle}>Name</span>}
            rules={[{ required: true, message: 'Please enter your name' }]}
          >
            <Input
              placeholder="e.g. John"
              style={isDark ? {} : lightInputStyle}
            />
          </Form.Item>

          <Form.Item
            name="surname"
            label={<span style={isDark ? {} : lightLabelStyle}>Surname</span>}
            rules={[{ required: true, message: 'Please enter your surname' }]}
          >
            <Input
              placeholder="e.g. Smith"
              style={isDark ? {} : lightInputStyle}
            />
          </Form.Item>

          <Form.Item
            name="password"
            label={<span style={isDark ? {} : lightLabelStyle}>Password</span>}
            rules={[{ required: true, message: 'Please enter your password' }]}
          >
            <Input.Password
              placeholder="e.g. StrongPassword123!"
              style={isDark ? {} : lightInputStyle}
            />
          </Form.Item>

          <Form.Item
            name="repeatPassword"
            label={<span style={isDark ? {} : lightLabelStyle}>Repeat Password</span>}
            dependencies={['password']}
            hasFeedback
            rules={[
              { required: true, message: 'Please confirm your password' },
              ({ getFieldValue }) => ({
                validator(_, value) {
                  if (!value || getFieldValue('password') === value) {
                    return Promise.resolve();
                  }
                  return Promise.reject(
                    new Error('Passwords do not match')
                  );
                },
              }),
            ]}
          >
            <Input.Password
              placeholder="e.g. StrongPassword123!"
              style={isDark ? {} : lightInputStyle}
              iconRender={(visible) =>
                (visible ? <EyeTwoTone /> : <EyeInvisibleOutlined />)}
            />
          </Form.Item>

          <Form.Item className={"flex justify-center"}>
            <Button
              type="primary"
              htmlType="submit"
              block
              loading={isLoading}
              className={"!w-[12rem] mt-4"}
            >
              {isLoading ? 'Registering...' : 'Register'}
            </Button>
          </Form.Item>
        </Form>
      </div>
    </Content>
  );
}

export default Register;