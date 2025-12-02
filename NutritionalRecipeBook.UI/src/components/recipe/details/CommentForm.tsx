import { Button, Form, Rate } from 'antd';
import TextArea from 'antd/es/input/TextArea';
import { lightLabelStyle, formContainerLightStyle } from '../../../themes/modelStyles';

interface CommentFormValues {
  rating: number;
  content: string;
}

export function CommentForm({ onSubmit, loading }: { onSubmit: (v: CommentFormValues) => void | Promise<void>; loading?: boolean }) {
  const [form] = Form.useForm<CommentFormValues>();

  const handleFinish = async (values: CommentFormValues) => {
    await onSubmit(values);
    form.resetFields(['rating', 'content']);
  };

  return (
    <Form
      form={form}
      layout="vertical"
      onFinish={handleFinish}
      className="w-11/12 !p-4 rounded-lg bg-[var(--card)] text-[var(--fg)] border border-[var(--border)]"
      style={formContainerLightStyle}
    >
      <Form.Item
        name="rating"
        label={<span style={lightLabelStyle}>Rating</span>}
        rules={[{ required: true, message: 'Please, rate this recipe!' }]}
      >
        <Rate id="rating" allowClear={false} />
      </Form.Item>

      <Form.Item
        name="content"
        label={<span style={lightLabelStyle}>Comment</span>}
        rules={[{ required: true, message: 'Please, write a comment!' }]}
      >
        <TextArea placeholder="Your comment goes here..." />
      </Form.Item>

      <Button type="primary" htmlType="submit" className={"mt-4"} loading={!!loading}>
        {loading ? 'Submitting...' : 'Submit'}
      </Button>
    </Form>
  );
}

export default CommentForm;
