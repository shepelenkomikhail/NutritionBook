import { useEffect } from 'react';
import { PlusOutlined, LoadingOutlined } from '@ant-design/icons';
import type { RecipeModel } from '@models';
import { useIngredientsQuery, useMeasurementUnitsQuery, useRecipeMutation } from '@hooks';
import { formContainerLightStyle, lightLabelStyle } from '../../themes/modelStyles.ts';
import { Button, Form, Input, InputNumber, Upload } from 'antd';
import SecureImage from '../shared/SecureImage';
import IngredientFields from './recipe-form/IngredientFields';
import RecipeNutritionSummary from './recipe-form/RecipeNutritionSummary';
import { prepareRecipeInitialValues } from './recipe-form/ingredientHelpers';
import { useRecipeImageUpload } from './recipe-form/useRecipeImageUpload';

interface RecipeFormProps {
  id?: string | null;
  mode: 'create' | 'update';
  initialValues?: RecipeModel;
  onSubmit: () => void;
  setIsLoading: (isLoading: boolean) => void;
}

const BASE_URL = import.meta.env.VITE_API_URL;

function RecipeForm({ mode, initialValues, onSubmit, setIsLoading, id }: RecipeFormProps) {
  const [form] = Form.useForm<RecipeModel>();
  const { execute, isLoading } = useRecipeMutation();
  const { ingredients,  } = useIngredientsQuery();
  const { units: liquidUnits, isLoading: isLoadingLiquidUnits } = useMeasurementUnitsQuery({ isLiquid: true });
  const { units: normalUnits, isLoading: isLoadingNormalUnits } = useMeasurementUnitsQuery({ isLiquid: false });
  const isUnitsLoading = isLoadingLiquidUnits || isLoadingNormalUnits;

  const { imageUrl, setImageUrl, loading: uploadLoading, beforeUpload, handleChange } = useRecipeImageUpload({ form });

  useEffect(() => setIsLoading(isLoading), [isLoading, setIsLoading]);

  useEffect(() => {
    if (!initialValues) return;
    const prepared = prepareRecipeInitialValues(initialValues, ingredients);
    form.setFieldsValue(prepared as RecipeModel);
    if (prepared?.imageUrl) setImageUrl(prepared.imageUrl);
  }, [initialValues, ingredients, form, setImageUrl]);

  const handleSubmit = async (values: RecipeModel) => {
    await execute({ values, id: id ?? '', mode });
    onSubmit();
  };

  const uploadButton = (
    <button
      style={{ border: 0, background: 'none' }}
      type="button"
    >
      {uploadLoading ? <LoadingOutlined /> : <PlusOutlined />}
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
      <Form.Item className="!w-full">
        <Upload
          name="file"
          listType="picture-card"
          className="avatar-uploader !w-full"
          showUploadList={false}
          action={`${BASE_URL}/api/recipes/image`}
          headers={localStorage.getItem('token') ? { Authorization: `Bearer ${localStorage.getItem('token')}` } : undefined}
          withCredentials
          beforeUpload={beforeUpload}
          onChange={handleChange}
          style={{ width: '100%' }}
        >
          {imageUrl ? (
            <SecureImage
              src={imageUrl}
              alt="avatar"
              style={{ width: '400px', height: '120px', borderRadius: '10px', marginTop: '16px' }}
            />
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
            <label className="font-medium mb-2 block" style={lightLabelStyle}>
              Ingredients
            </label>

            {fields.map(({ key, name, ...restField }) => (
              <IngredientFields
                key={key}
                form={form}
                field={{ key, name, restField }}
                ingredients={ingredients}
                liquidUnits={liquidUnits}
                normalUnits={normalUnits}
                isUnitsLoading={isUnitsLoading}
                onRemove={remove}
              />
            ))}

            <Form.Item>
              <Button type="dashed" onClick={() => add()} block icon={<PlusOutlined />}>
                Add Ingredient
              </Button>
            </Form.Item>
          </>
        )}
      </Form.List>

      <RecipeNutritionSummary form={form} />

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
        <InputNumber min={1} style={{ width: '100%' }} />
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
