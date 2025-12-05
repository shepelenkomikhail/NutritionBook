import { useEffect, useState } from 'react';
import { MinusCircleOutlined, PlusOutlined, LoadingOutlined } from '@ant-design/icons';
import type { FormIngredientModel, RecipeModel } from '@models';
import { useIngredientsQuery, useMeasurementUnitsQuery, useRecipeMutation } from '@hooks';
import { formContainerLightStyle, lightLabelStyle } from '../../themes/modelStyles.ts';
import { Button, Form, GetProp, Input, InputNumber, message, Select, Space, Upload, UploadProps } from 'antd';
import SecureImage from '../shared/SecureImage';

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
  const isJpgOrPng =
    file.type === 'image/jpeg' || file.type === 'image/png';
  if (!isJpgOrPng) message.error('You can only upload JPG/PNG files!').then(() => {});
  const isLt2M = file.size / 1024 / 1024 < 2;
  if (!isLt2M) message.error('Image must be smaller than 2MB!').then(() => {});
  return isJpgOrPng && isLt2M;
};

const BASE_URL = import.meta.env.VITE_API_URL;

function RecipeForm({ mode, initialValues, onSubmit, setIsLoading, id }: RecipeFormProps) {
  const [form] = Form.useForm<RecipeModel>();
  const { execute, isLoading } = useRecipeMutation();
  const { ingredients, isLoading: isLoadingIngredients } = useIngredientsQuery();
  const { units: liquidUnits, isLoading: isLoadingLiquidUnits } = useMeasurementUnitsQuery({ isLiquid: true });
  const { units: normalUnits, isLoading: isLoadingNormalUnits } = useMeasurementUnitsQuery({ isLiquid: false });
  const isUnitsLoading = isLoadingLiquidUnits || isLoadingNormalUnits;

  useEffect(() => setIsLoading(isLoading), [isLoading, setIsLoading]);

  useEffect(() => {
    if (!initialValues) return;
    form.setFieldsValue(initialValues);

    if (initialValues.imageUrl) setImageUrl(initialValues.imageUrl);
    const currentIngredients = initialValues.ingredients as unknown as | FormIngredientModel[] | undefined;

    if (Array.isArray(currentIngredients) && currentIngredients.length > 0) {
      const patched = currentIngredients.map((ing) => {
        const ingMeta = ingredients.find((i) => i.name === ing.name);
        return {
          ...ing,
          isLiquid: typeof ing.isLiquid === 'boolean' ? ing.isLiquid : (ingMeta?.isLiquid ?? false),
          uom: ing.uom ?? ingMeta?.uom,
          caloriesPer100: ing.caloriesPer100 ?? ingMeta?.calories,
          proteinsPer100: ing.proteinsPer100 ?? ingMeta?.proteins,
          carbsPer100: ing.carbsPer100 ?? ingMeta?.carbs,
          fatsPer100: ing.fatsPer100 ?? ingMeta?.fats,
        } as FormIngredientModel;
      });
      form.setFieldValue('ingredients', patched);
    }
  }, [initialValues, ingredients, form]);

  const handleSubmit = async (values: RecipeModel) => {
    await execute({ values, id: id ?? '', mode });
    onSubmit();
  };

  const [loading, setLoading] = useState(false);
  const [imageUrl, setImageUrl] = useState<string>();

  const handleChange: UploadProps['onChange'] = (info) => {
    if (info.file.status === 'uploading') return setLoading(true);

    if (info.file.status === 'done') {
      const resp = info.file.response as | { url?: string } | undefined;
      const serverUrl = resp?.url;

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
    <button
      style={{ border: 0, background: 'none' }}
      type="button"
    >
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
                  alignItems: 'center'
                }}
                align="baseline"
              >
                <Form.Item
                  {...restField}
                  name={[name, 'name']}
                  rules={[{ required: true, message: 'Select ingredient' }]}
                  className="!w-42"
                >
                  <Select
                    showSearch
                    placeholder={
                      isLoadingIngredients
                        ? 'Loading ingredients...'
                        : 'Select ingredient'
                    }
                    loading={isLoadingIngredients}
                    options={ingredients.map((i) => ({ value: i.name, label: i.name }))}

                    filterOption={(input, option) =>
                      (option?.label as string)
                        ?.toLowerCase()
                        .includes(input.toLowerCase())
                    }
                    onChange={(val) => {
                      const ingMeta = ingredients.find((i) => i.name === val);
                      const inferred = ingMeta?.isLiquid ?? false;

                      form.setFieldValue(['ingredients', name, 'isLiquid'], inferred);
                      if (ingMeta?.uom) {
                        form.setFieldValue(['ingredients', name, 'uom'], ingMeta.uom);
                      }
                      form.setFieldValue(['ingredients', name, 'caloriesPer100'], ingMeta?.calories ?? undefined);
                      form.setFieldValue(['ingredients', name, 'proteinsPer100'], ingMeta?.proteins ?? undefined);
                      form.setFieldValue(['ingredients', name, 'carbsPer100'], ingMeta?.carbs ?? undefined);
                      form.setFieldValue(['ingredients', name, 'fatsPer100'], ingMeta?.fats ?? undefined);

                      const currentUnit = form.getFieldValue(['ingredients', name, 'unit']);
                      const availableUnits = inferred ? liquidUnits : normalUnits;

                      if (currentUnit &&
                        !availableUnits?.some((u) => u.name === currentUnit)){
                        form.setFieldValue(['ingredients', name, 'unit'], undefined);
                      }
                    }}
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
                  shouldUpdate
                  noStyle
                >
                  {() => {
                    const ingredientSelected = !!form.getFieldValue(['ingredients', name, 'name']);
                    const isIngLiquid = form.getFieldValue(['ingredients', name, 'isLiquid']);
                    const disabled = !ingredientSelected;

                    const opts = (isIngLiquid ? liquidUnits : normalUnits
                    )?.map((u) => ({ value: u.name, label: u.name })) ?? [];

                    return (
                      <Form.Item
                        {...restField}
                        name={[name, 'unit']}
                        rules={[
                          {
                            required: true,
                            message: 'Select unit'
                          }
                        ]}
                      >
                        <Select
                          disabled={disabled}
                          loading={isUnitsLoading}
                          placeholder={disabled ? 'Select ingredient first' : 'Select unit'}
                          className="!w-28"
                          options={opts}
                          showSearch
                          filterOption={(input, option) => (option?.label as string)?.toLowerCase().includes(input.toLowerCase())}
                        />
                      </Form.Item>
                    );
                  }}
                </Form.Item>

                <Form.Item
                  {...restField}
                  name={[name, 'id']}
                  hidden
                >
                  <Input type="hidden" />
                </Form.Item>

                <Form.Item
                  {...restField}
                  name={[name, 'isLiquid']}
                  hidden
                >
                  <Input type="hidden" />
                </Form.Item>

                <Form.Item {...restField} name={[name, 'uom']} hidden>
                  <Input type="hidden" />
                </Form.Item>
                <Form.Item {...restField} name={[name, 'caloriesPer100']} hidden>
                  <Input type="hidden" />
                </Form.Item>
                <Form.Item {...restField} name={[name, 'proteinsPer100']} hidden>
                  <Input type="hidden" />
                </Form.Item>
                <Form.Item {...restField} name={[name, 'carbsPer100']} hidden>
                  <Input type="hidden" />
                </Form.Item>
                <Form.Item {...restField} name={[name, 'fatsPer100']} hidden>
                  <Input type="hidden" />
                </Form.Item>

                <MinusCircleOutlined
                  onClick={() => remove(name)}
                  style={{ color: 'var(--danger)', marginLeft: -52 }}
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

      <Form.Item shouldUpdate noStyle>
        {() => {
          const rows = (form.getFieldValue('ingredients') as FormIngredientModel[] | undefined) ?? [];
          const totals = rows.reduce(
            (acc, r) => {
              const unitMatchesBase = r && r.unit && r.uom && r.unit === r.uom;
              const amount = typeof r?.amount === 'number' ? r.amount : 0;
              const factor = unitMatchesBase ? amount / 100 : 0;
              if (factor > 0) {
                acc.calories += (r.caloriesPer100 ?? 0) * factor;
                acc.proteins += (r.proteinsPer100 ?? 0) * factor;
                acc.carbs += (r.carbsPer100 ?? 0) * factor;
                acc.fats += (r.fatsPer100 ?? 0) * factor;
              }
              return acc;
            },
            { calories: 0, proteins: 0, carbs: 0, fats: 0 }
          );

          const round = (n: number) => Math.round(n * 100) / 100;

          return (
            <div className="mt-4 border border-[var(--border)] rounded-md p-3">
              <div className="font-medium mb-2" style={lightLabelStyle}>Current Nutritional Content</div>
              <div className="grid grid-cols-4 gap-2 text-[var(--fg)]">
                <div>
                  <div className="text-[var(--fg-muted)]">Calories</div>
                  <div>{round(totals.calories)} kcal</div>
                </div>
                <div>
                  <div className="text-[var(--fg-muted)]">Proteins</div>
                  <div>{round(totals.proteins)} g</div>
                </div>
                <div>
                  <div className="text-[var(--fg-muted)]">Carbs</div>
                  <div>{round(totals.carbs)} g</div>
                </div>
                <div>
                  <div className="text-[var(--fg-muted)]">Fats</div>
                  <div>{round(totals.fats)} g</div>
                </div>
              </div>
              <div className="text-xs text-[var(--fg-muted)] mt-2">
                Note: Nutrients are computed when the selected unit matches the ingredient's base unit (g/ml).
              </div>
            </div>
          );
        }}
      </Form.Item>

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
