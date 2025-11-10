import { PlusOutlined, MinusCircleOutlined } from '@ant-design/icons';
import { Button, Checkbox, Form, Input, InputNumber, Space } from 'antd';
import type { RecipeModel } from '@models';

interface RecipeFormProps {
  mode?: 'create' | 'update';
  initialValues?: RecipeModel;
  onSubmit: (values: RecipeModel) => Promise<void>;
  isLoading?: boolean;
}

export function RecipeForm({ mode = 'create', initialValues, onSubmit, isLoading }: RecipeFormProps) {
  const [form] = Form.useForm<RecipeModel>();

  return (
    <Form
      form={form}
      layout="vertical"
      onFinish={onSubmit}
      initialValues={initialValues}
      className="w-11/12"
    >
      <Form.Item
        name="name"
        label="Recipe Name"
        rules={[{ required: true, message: 'Please enter the recipe name' }]}
      >
        <Input placeholder="e.g. Spaghetti Carbonara" />
      </Form.Item>

      <Form.Item
        name="description"
        label="Description"
        rules={[{ required: true, message: 'Please enter a description' }]}
      >
        <Input.TextArea rows={2} placeholder="Brief description" />
      </Form.Item>

      <Form.List name="ingredients">
        {(fields, { add, remove }) => (
          <>
            <label className="font-medium mb-2 block">Ingredients</label>
            {fields.map(({ key, name, ...restField }) => (
              <Space
                key={key}
                style={{
                  display: 'flex',
                  marginBottom: 8,
                  alignItems: 'center',
                }}
                align="baseline"
              >
                <Form.Item
                  {...restField}
                  name={[name, 'name']}
                  rules={[{ required: true, message: 'Enter ingredient name' }]}
                >
                  <Input placeholder="Ingredient name" />
                </Form.Item>

                <Form.Item
                  {...restField}
                  name={[name, 'amount']}
                  rules={[{ required: true, message: 'Enter amount' }]}
                >
                  <InputNumber min={0.1} placeholder="Amount" />
                </Form.Item>

                <Form.Item
                  {...restField}
                  name={[name, 'unit']}
                  rules={[{ required: true, message: 'Enter unit' }]}
                >
                  <Input placeholder="e.g. g, ml" />
                </Form.Item>

                <Form.Item
                  {...restField}
                  name={[name, 'isLiquid']}
                  valuePropName="checked"
                  style={{ marginBottom: 0 }}
                >
                  <Checkbox>Liquid</Checkbox>
                </Form.Item>

                <MinusCircleOutlined
                  onClick={() => remove(name)}
                  style={{ color: 'red', marginLeft: 8 }}
                />
              </Space>
            ))}
            <Form.Item>
              <Button
                type="dashed"
                onClick={() => add()}
                block
                icon={<PlusOutlined />}
              >
                Add Ingredient
              </Button>
            </Form.Item>
          </>
        )}
      </Form.List>

      <Form.Item
        name="instructions"
        label="Instructions"
        rules={[{ required: true, message: 'Please enter instructions' }]}
      >
        <Input.TextArea rows={3} placeholder="Step-by-step instructions..." />
      </Form.Item>

      <Form.Item
        name="cookingTimeInMin"
        label="Cooking Time (minutes)"
        rules={[{ required: true, message: 'Please enter cooking time' }]}
      >
        <InputNumber min={1} style={{ width: '100%' }} />
      </Form.Item>

      <Form.Item
        name="servings"
        label="Servings"
        rules={[{ required: true, message: 'Please enter number of servings' }]}
      >
        <InputNumber min={1} style={{ width: '100%' }} />
      </Form.Item>

      <Form.Item>
        <Button type="primary" htmlType="submit" block loading={isLoading}>
          {isLoading ?
            `${mode === 'create' ? 'Creating...' : 'Updating...'}`
            : mode === 'create' ? 'Create Recipe' : 'Update Recipe'}
        </Button>
      </Form.Item>
    </Form>
  );
}

export default RecipeForm;