import { useEffect } from 'react';
import { MinusCircleOutlined, PlusOutlined } from '@ant-design/icons';
import type { RecipeModel } from '@models';
import { useRecipeMutation } from '../../hooks';
import {
  formContainerLightStyle,
  lightInputStyle,
  lightLabelStyle,
} from '../../themes/modelStyles.ts';
import { Button, Checkbox, Form, Input, InputNumber, Select, Space } from 'antd';


interface RecipeFormProps {
  id?: string | null;
  mode: 'create' | 'update';
  initialValues?: RecipeModel;
  onSubmit: () => void;
  setIsLoading: (isLoading: boolean) => void;
}

function RecipeForm({ mode, initialValues, onSubmit, setIsLoading, id }: RecipeFormProps) {
  const [form] = Form.useForm<RecipeModel>();
  const { execute, isLoading } = useRecipeMutation();

  useEffect(() => {
    setIsLoading(isLoading)
  }, [isLoading, setIsLoading]);

  useEffect(() => {
    if (initialValues) {
      form.setFieldsValue(initialValues);
    }
  }, [initialValues, form]);

  const handleSubmit = async (values: RecipeModel) => {
    await execute(values, id ?? undefined, mode);
    onSubmit();
  }



  return (
    <Form
      form={form}
      layout="vertical"
      onFinish={() => handleSubmit(form.getFieldsValue())}
      initialValues={initialValues}
      className="w-11/12 !p-4 rounded-lg bg-[var(--card)] text-[var(--fg)] border border-[var(--border)]"
      style={formContainerLightStyle}
    >
      <Form.Item
        name="name"
        label={<span style={lightLabelStyle}>Recipe Name</span>}
        rules={[{ required: true, message: 'Please enter the recipe name' }]}
      >
        <Input
          placeholder="e.g. Spaghetti Carbonara"
          style={lightInputStyle}
        />
      </Form.Item>

      <Form.Item
        name="description"
        label={<span style={lightLabelStyle}>Description</span>}
        rules={[{ required: true, message: 'Please enter a description' }]}
      >
        <Input.TextArea
          rows={2}
          placeholder="Brief description"
          style={lightInputStyle}
        />
      </Form.Item>

      <Form.List name="ingredients">
        {(fields, { add, remove }) => (
          <>
            <label
              className="font-medium mb-2 block"
              style={lightLabelStyle}
            >
              Ingredients
            </label>

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
                  <Input
                    placeholder="Ingredient name"
                    style={lightInputStyle}
                  />
                </Form.Item>

                <Form.Item
                  {...restField}
                  name={[name, 'amount']}
                  rules={[{ required: true, message: 'Enter amount' }]}
                >
                  <InputNumber
                    min={0.1}
                    placeholder="Amount"
                    style={lightInputStyle}
                  />
                </Form.Item>

                <Form.Item
                  {...restField}
                  name={[name, 'unit']}
                  rules={[{ required: true, message: 'Enter unit' }]}
                >
                  <Select
                    defaultValue="g"
                    style={lightInputStyle}
                    className={"!w-16"}
                    options={[
                      { value: "g", label: "g" },
                      { value: "kg", label: "kg" },
                      { value: "ml", label: "ml" },
                      { value: "l", label: "l" },
                      { value: "tsp", label: "tsp" },
                      { value: "tbsp", label: "tbsp" },
                      { value: "cup", label: "cup" },
                      { value: "pcs", label: "pcs" }
                    ]}
                  />
                </Form.Item>

                <Form.Item
                  {...restField}
                  name={[name, 'isLiquid']}
                  valuePropName="checked"
                  style={{ marginBottom: 0 }}
                >
                  <Checkbox className="text-[var(--fg)]">
                    Liquid
                  </Checkbox>
                </Form.Item>

                <MinusCircleOutlined
                  onClick={() => remove(name)}
                  style={{
                    color: 'var(--danger)',
                    marginLeft: 8,
                  }}
                />
              </Space>
            ))}

            <Form.Item>
              <Button
                type="dashed"
                onClick={() => add()}
                block
                icon={<PlusOutlined />}
                style={lightInputStyle}
              >
                Add Ingredient
              </Button>
            </Form.Item>
          </>
        )}
      </Form.List>

      <Form.Item
        name="instructions"
        label={<span style={lightLabelStyle}>Instructions</span>}
        rules={[{ required: true, message: 'Please enter instructions' }]}
      >
        <Input.TextArea
          rows={3}
          placeholder="Step-by-step instructions..."
          style={lightInputStyle}
        />
      </Form.Item>

      <Form.Item
        name="cookingTimeInMin"
        label={
          <span style={lightLabelStyle}>
            Cooking Time (minutes)
          </span>
        }
        rules={[{ required: true, message: 'Please enter cooking time' }]}
      >
        <InputNumber min={1} style={{ ...lightInputStyle, width: '100%' }} />
      </Form.Item>

      <Form.Item
        name="servings"
        label={<span style={lightLabelStyle}>Servings</span>}
        rules={[{ required: true, message: 'Please enter number of servings' }]}
      >
        <InputNumber min={1} style={{ ...lightInputStyle, width: '100%' }} />
      </Form.Item>

      <Form.Item>
        <Button
          type="primary"
          htmlType="submit"
          block
          loading={isLoading}
        >
          {isLoading
            ? mode === 'create'
              ? 'Creating...'
              : 'Updating...'
            : mode === 'create'
              ? 'Create Recipe'
              : 'Update Recipe'}
        </Button>
      </Form.Item>
    </Form>
  );
}

export default RecipeForm;