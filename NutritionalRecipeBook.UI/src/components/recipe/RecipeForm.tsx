import { useEffect, useState } from 'react';
import { MinusCircleOutlined, PlusOutlined } from '@ant-design/icons';
import type { RecipeModel } from '@models';
import { useRecipeMutation } from '@hooks';
import {
  formContainerLightStyle,
  lightLabelStyle,
} from '../../themes/modelStyles.ts';
import { Button, Checkbox, Form, GetProp, Input, InputNumber, message, Select, Space, Upload, UploadProps } from 'antd';


interface RecipeFormProps {
  id?: string | null;
  mode: 'create' | 'update';
  initialValues?: RecipeModel;
  onSubmit: () => void;
  setIsLoading: (isLoading: boolean) => void;
}

type FileType = Parameters<GetProp<UploadProps, 'beforeUpload'>>[0];

const getBase64 = (img: FileType, callback: (url: string) => void) => {
  const reader = new FileReader();
  reader.addEventListener('load', () => callback(reader.result as string));
  reader.readAsDataURL(img);
};

const beforeUpload = (file: FileType) => {
  const isJpgOrPng = file.type === 'image/jpeg' || file.type === 'image/png';
  if (!isJpgOrPng) {
    message.error('You can only upload JPG/PNG file!');
  }
  const isLt2M = file.size / 1024 / 1024 < 2;
  if (!isLt2M) {
    message.error('Image must smaller than 2MB!');
  }
  return isJpgOrPng && isLt2M;
};

function LoadingOutlined() {
  return null;
}

const BASE_URL = import.meta.env.VITE_API_URL;

function RecipeForm({ mode, initialValues, onSubmit, setIsLoading, id }: RecipeFormProps) {
  const [form] = Form.useForm<RecipeModel>();
  const { execute, isLoading } = useRecipeMutation();

  useEffect(() => {
    setIsLoading(isLoading)
  }, [isLoading, setIsLoading]);

  useEffect(() => {
    if (initialValues) {
      form.setFieldsValue(initialValues);
      if (initialValues.imageUrl) {
        setImageUrl(initialValues.imageUrl);
      }
    }
  }, [initialValues, form]);

  const handleSubmit = async (values: RecipeModel) => {
    await execute(values, id ?? undefined, mode);
    onSubmit();
  }

  const [loading, setLoading] = useState(false);
  const [imageUrl, setImageUrl] = useState<string>();

  const handleChange: UploadProps['onChange'] = (info) => {
    if (info.file.status === 'uploading') {
      setLoading(true);
      return;
    }
    if (info.file.status === 'done') {
      const serverUrl = (info.file.response as any)?.url as string | undefined;
      if (serverUrl) {
        setLoading(false);
        setImageUrl(serverUrl);
        form.setFieldValue('imageUrl', serverUrl);
      } else {
        getBase64(info.file.originFileObj as FileType, (url) => {
          setLoading(false);
          setImageUrl(url);
          form.setFieldValue('imageUrl', url);
        });
      }
    }
  };

  const uploadButton = (
    <button style={{ border: 0, background: 'none' }} type="button">
      {loading ? <LoadingOutlined /> : <PlusOutlined />}
      <div style={{ marginTop: 8 }}>Upload</div>
    </button>
  );


  return (
    <Form
      form={form}
      layout="vertical"
      onFinish={() => handleSubmit(form.getFieldsValue())}
      initialValues={initialValues}
      className="w-11/12 !p-4 rounded-lg bg-[var(--card)] text-[var(--fg)] border border-[var(--border)]"
      style={formContainerLightStyle}
    >
      <Form.Item className={"!w-full"}>
        <Upload
          name="file"
          listType="picture-card"
          className="avatar-uploader !w-full"
          showUploadList={false}
          action="http://localhost:5039/api/recipes/image"
          beforeUpload={beforeUpload}
          onChange={handleChange}
          style={{ width: '100%' }}
        >
          {imageUrl ? (
            <img draggable={false} src={BASE_URL + imageUrl} alt="avatar"
                 style={{ width: '100%', height: '140px', borderRadius: '10px', marginTop: '16px' }} />
          ) : (
            uploadButton
          )}
        </Upload>
        </Form.Item>
        <Form.Item name="imageUrl" hidden>
          <Input type="hidden" />
        </Form.Item>

      <Form.Item
        name="name"
        label={<span style={lightLabelStyle}>Recipe Name</span>}
        rules={[{ required: true, message: 'Please enter the recipe name' }]}
      >
        <Input
          placeholder="e.g. Spaghetti Carbonara"
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
                  />
                </Form.Item>

                <Form.Item
                  {...restField}
                  name={[name, 'unit']}
                  rules={[{ required: true, message: 'Enter unit' }]}
                >
                  <Select
                    defaultValue="g"
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
        <InputNumber min={1} style={{width: '100%' }} />
      </Form.Item>

      <Form.Item
        name="servings"
        label={<span style={lightLabelStyle}>Servings</span>}
        rules={[{ required: true, message: 'Please enter number of servings' }]}
      >
        <InputNumber min={1} style={{ width: '100%' }} />
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