import { Form, Input, InputNumber, Select, Space } from 'antd';
import type { FormInstance } from 'antd';
import { MinusCircleOutlined } from '@ant-design/icons';
import type { IngredientNutritionInfoModel, UnitOfMeasureModel } from '@models';
import { applyIngredientMetaToForm } from './ingredientHelpers';

type FieldMeta = {
  key: number;
  name: number;
  restField: Record<string, unknown>;
};

interface Props {
  form: FormInstance;
  field: FieldMeta;
  ingredients: IngredientNutritionInfoModel[];
  liquidUnits: UnitOfMeasureModel[];
  normalUnits: UnitOfMeasureModel[];
  isUnitsLoading: boolean;
  onRemove: (name: number) => void;
}

function IngredientFields({ form, field, ingredients, liquidUnits, normalUnits, isUnitsLoading, onRemove }: Props) {
  const { key, name, restField } = field;

  return (
    <Space
      key={key}
      style={{ display: 'flex', marginBottom: 8, alignItems: 'center' }}
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
          placeholder={'Select ingredient'}
          options={ingredients.map((i) => ({ value: i.name, label: i.name }))}
          filterOption={(input, option) => (option?.label as string)?.toLowerCase().includes(input.toLowerCase())}
          onChange={(val) => {
            const ingMeta = ingredients.find((i) => i.name === val);
            applyIngredientMetaToForm(form, name, ingMeta);

            // Reset unit if incompatible
            const inferred = ingMeta?.isLiquid ?? false;
            const currentUnit = form.getFieldValue(['ingredients', name, 'unit']);
            const availableUnits = inferred ? liquidUnits : normalUnits;
            if (currentUnit && !availableUnits?.some((u) => u.name === currentUnit)) {
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
        <InputNumber min={0.1} placeholder="Amount" />
      </Form.Item>

      <Form.Item {...restField} shouldUpdate noStyle>
        {() => {
          const ingredientSelected = !!form.getFieldValue(['ingredients', name, 'name']);
          const isIngLiquid = form.getFieldValue(['ingredients', name, 'isLiquid']);
          const disabled = !ingredientSelected;
          const opts = (isIngLiquid ? liquidUnits : normalUnits)?.map((u) => ({ value: u.name, label: u.name })) ?? [];

          return (
            <Form.Item
              {...restField}
              name={[name, 'unit']}
              rules={[{ required: true, message: 'Select unit' }]}
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

      {/* hidden fields to preserve meta */}
      <Form.Item {...restField} name={[name, 'id']} hidden>
        <Input type="hidden" />
      </Form.Item>
      <Form.Item {...restField} name={[name, 'isLiquid']} hidden>
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

      <MinusCircleOutlined onClick={() => onRemove(name)} style={{ color: 'var(--danger)', marginLeft: -52 }} />
    </Space>
  );
}

export default IngredientFields;
