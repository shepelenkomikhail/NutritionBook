import { useContext, useEffect } from 'react';



import { MinusCircleOutlined, PlusOutlined } from '@ant-design/icons';
import type { RecipeModel } from '@models';



import { useRecipeMutation } from '../../hooks';
import { ThemeContext } from '../../layout/App.tsx';
import {
  formContainerLightStyle,
  lightInputStyle,
  lightLabelStyle,
} from '../../themes/modelStyles.ts';
import { Button, Checkbox, Form, Input, InputNumber, Space } from 'antd';


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
  const {theme, } = useContext(ThemeContext);
  const isDark = theme === 'dark';

  useEffect(() => {
    setIsLoading(isLoading)
  }, [isLoading, setIsLoading]);

  useEffect(() => {
    if (initialValues) {
      form.setFieldsValue(initialValues);
    }
  }, [initialValues, form]);

  const handleSubmit = async (values: RecipeModel, id: string) => {
    await execute(values, id, mode);
    onSubmit();
  }



  return (
    <Form
      form={form}
      layout="vertical"
      onFinish={() => handleSubmit(form.getFieldsValue(), id!)}
      initialValues={initialValues}
      className="w-11/12"
      style={ !isDark ? formContainerLightStyle : {}}
    >
      <Form.Item
        name="name"
        label={<span style={isDark ? {} : lightLabelStyle}>Recipe Name</span>}
        rules={[{ required: true, message: 'Please enter the recipe name' }]}
      >
        <Input
          placeholder="e.g. Spaghetti Carbonara"
          style={isDark ? {} : lightInputStyle}
        />
      </Form.Item>

      <Form.Item
        name="description"
        label={<span style={isDark ? {} : lightLabelStyle}>Description</span>}
        rules={[{ required: true, message: 'Please enter a description' }]}
      >
        <Input.TextArea
          rows={2}
          placeholder="Brief description"
          style={isDark ? {} : lightInputStyle}
        />
      </Form.Item>

      <Form.List name="ingredients">
        {(fields, { add, remove }) => (
          <>
            <label
              className="font-medium mb-2 block"
              style={isDark ? {} : lightLabelStyle}
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
                    style={isDark ? {} : lightInputStyle}
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
                    style={isDark ? {} : lightInputStyle}
                  />
                </Form.Item>

                <Form.Item
                  {...restField}
                  name={[name, 'unit']}
                  rules={[{ required: true, message: 'Enter unit' }]}
                >
                  <Input
                    placeholder="e.g. g, ml"
                    style={isDark ? {} : lightInputStyle}
                  />
                </Form.Item>

                <Form.Item
                  {...restField}
                  name={[name, 'isLiquid']}
                  valuePropName="checked"
                  style={{ marginBottom: 0 }}
                >
                  <Checkbox
                    style={{
                      color: isDark
                        ? 'rgb(203 213 225)'
                        : 'rgb(55 65 81)',
                    }}
                  >
                    Liquid
                  </Checkbox>
                </Form.Item>

                <MinusCircleOutlined
                  onClick={() => remove(name)}
                  style={{
                    color: 'rgb(239 68 68)',
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
                style={isDark ? {} : lightInputStyle}
              >
                Add Ingredient
              </Button>
            </Form.Item>
          </>
        )}
      </Form.List>

      <Form.Item
        name="instructions"
        label={<span style={isDark ? {} : lightLabelStyle}>Instructions</span>}
        rules={[{ required: true, message: 'Please enter instructions' }]}
      >
        <Input.TextArea
          rows={3}
          placeholder="Step-by-step instructions..."
          style={isDark ? {} : lightInputStyle}
        />
      </Form.Item>

      <Form.Item
        name="cookingTimeInMin"
        label={
          <span style={isDark ? {} : lightLabelStyle}>
            Cooking Time (minutes)
          </span>
        }
        rules={[{ required: true, message: 'Please enter cooking time' }]}
      >
        <InputNumber min={1} style={isDark ? {} : { ...lightInputStyle, width: '100%' }} />
      </Form.Item>

      <Form.Item
        name="servings"
        label={<span style={isDark ? {} : lightLabelStyle}>Servings</span>}
        rules={[{ required: true, message: 'Please enter number of servings' }]}
      >
        <InputNumber min={1} style={isDark ? {} : { ...lightInputStyle, width: '100%' }} />
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