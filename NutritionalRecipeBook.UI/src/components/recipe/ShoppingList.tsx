import { Drawer, Empty, List, Spin, Typography, InputNumber, Form, Button } from 'antd';
import { useIngredientsQuery, useMeasurementUnitsQuery, useShoppingListQuery } from '@hooks';
import {
  DeleteFromShoppingListButton, MarkAsBoughtItemButton,
  ClearShoppingListButton, MarkAsBoughtAllItemsButton, UpdateListButton, PrintShoppingListButton
} from './buttons';
import { useEffect, useState } from 'react';
import { IngredientUnitOfMeasureModel } from '@models';
import { lightLabelStyle } from '../../themes/modelStyles.ts';
import IngredientFields from './recipe-form/IngredientFields.tsx';
import { PlusOutlined } from '@ant-design/icons';
import { UNIT_CONVERSIONS } from '@utils/unitOfMeasures';

interface Props {
  isCartOpen: boolean;
  handleCloseCart: () => void;
}

function toBase(amount: number, unit: string): number {
  return amount * UNIT_CONVERSIONS[unit];
}

function fromBase(baseAmount: number, unit: string): number {
  return baseAmount / UNIT_CONVERSIONS[unit];
}

function areUnitsCompatible(unit1: string, unit2: string): boolean {
  const solid = ["g", "kg", "tsp", "tbsp"];
  const liquid = ["ml", "l", "tsp", "tbsp"];

  return (
    (solid.includes(unit1) && solid.includes(unit2)) ||
    (liquid.includes(unit1) && liquid.includes(unit2))
  );
}


function ShoppingList({ isCartOpen, handleCloseCart }: Props) {
  const { shoppingList, isLoading, isFetching, isError } = useShoppingListQuery();
  const isBusy = isLoading || isFetching;

  const [isAllBought, setIsAllBought] = useState(false);
  const [isDisabledUpdateButton, setIsDisabledUpdateButton] = useState(true);

  const { ingredients } = useIngredientsQuery();
  const { units: liquidUnits, isLoading: isLoadingLiquidUnits } = useMeasurementUnitsQuery({ isLiquid: true });
  const { units: normalUnits, isLoading: isLoadingNormalUnits } = useMeasurementUnitsQuery({ isLiquid: false });

  const isUnitsLoading = isLoadingLiquidUnits || isLoadingNormalUnits;

  const [editableList, setEditableList] = useState<IngredientUnitOfMeasureModel[]>([]);
  const [originalList, setOriginalList] = useState<IngredientUnitOfMeasureModel[]>([]);

  const [form] = Form.useForm();

  useEffect(() => {
    if (shoppingList?.ingredientUnitOfMeasures) {
      const cloned = shoppingList.ingredientUnitOfMeasures.map(i => ({ ...i }));
      setEditableList(cloned);
      setOriginalList(cloned.map(i => ({ ...i })));
      setIsDisabledUpdateButton(true);
    } else {
      setEditableList([]);
      setOriginalList([]);
      setIsDisabledUpdateButton(true);
    }
  }, [shoppingList]);


  const handleAmountChange = (itemId: string | number | undefined, value: number | undefined) => {
    const updated = editableList.map(it =>
      it.ingredient?.id === itemId ? { ...it, amount: value ?? 0 } : it
    );

    setEditableList(updated);

    const changed = JSON.stringify(updated) !== JSON.stringify(originalList);
    setIsDisabledUpdateButton(!changed);
  };


  const handleAddNewIngredients = (values: any) => {
    if (!values.ingredients) return;

    const mapped: IngredientUnitOfMeasureModel[] = values.ingredients
      .map((ing: any) => {
        const info = ingredients.find(i => i.name == ing.name);
        console.log('info', info);
        console.log('ing', ing.name);
        console.log("MATCH in list:", ingredients.find(i => i.name === ing.name));
        console.log(editableList)
        if (!info) return null;

        return {
          ingredient: info,
          amount: ing.amount,
          unitOfMeasure: ing.unit,
          isBought: false
        };
      })
      .filter(Boolean) as IngredientUnitOfMeasureModel[];

    setEditableList(prev => {
      const updated = [...prev];

      mapped.forEach(newItem => {
        const existing = updated.find(x => x.ingredient.name === newItem.ingredient.name);

        if (!existing) {
          updated.push(newItem);
          return;
        }

        if (!areUnitsCompatible(existing.unitOfMeasure, newItem.unitOfMeasure)) {
          updated.push(newItem);
          return;
        }

        const existingBase = toBase(existing.amount, existing.unitOfMeasure);
        const newBase = toBase(newItem.amount, newItem.unitOfMeasure);
        const totalBase = existingBase + newBase;
        const combinedAmount = fromBase(totalBase, existing.unitOfMeasure);

        existing.amount = combinedAmount;
      });

      const changed = JSON.stringify(updated) !== JSON.stringify(originalList);
      setIsDisabledUpdateButton(!changed);

      return updated;
    });

    form.resetFields();
  };

  return (
    <Drawer
      title={
        <>
          Shopping List
          <PrintShoppingListButton />
        </>
      }
      placement="right"
      open={isCartOpen}
      onClose={handleCloseCart}
      width={500}
      styles={{
        body: {
          backgroundColor: 'var(--bg)',
          color: 'var(--fg)',
          padding: 16,
        },
      }}
    >
      {isBusy && (
        <div className="flex items-center justify-center py-8">
          <Spin />
        </div>
      )}

      {!isBusy && !isError && editableList.length === 0 && (
        <Empty description="No items in your shopping list yet" />
      )}

      {!isBusy && !isError && editableList.length > 0 && (
        <>
          <div className="flex gap-2 mb-2">
            <MarkAsBoughtAllItemsButton isAllBought={isAllBought} setAllBought={setIsAllBought} />
            <ClearShoppingListButton />
          </div>

          <List
            itemLayout="horizontal"
            dataSource={editableList}
            renderItem={(item) => (
              <List.Item>
                <List.Item.Meta
                  title={
                    <Typography.Text className={`${item.isBought ? 'line-through !text-gray-500' : ''}`}>
                      {item.ingredient.name}
                    </Typography.Text>
                  }
                  description={
                    <div className="flex items-center gap-2">
                      <InputNumber
                        min={0}
                        value={item.amount}
                        onChange={(val) => handleAmountChange(item.ingredient.id, val as number | undefined)}
                        style={{ width: 120 }}
                        disabled={item.isBought}
                      />
                      <span>{item.unitOfMeasure}</span>
                    </div>
                  }
                />
                <MarkAsBoughtItemButton itemId={item.ingredient.id} isBought={item.isBought} />
                <DeleteFromShoppingListButton itemId={item.ingredient.id} />
              </List.Item>
            )}
          />

          <Form
            form={form}
            layout="vertical"
            className="!mt-8"
            onFinish={handleAddNewIngredients}
          >
            <Form.List name="ingredients">
              {(fields, { add, remove }) => (
                <>
                  <label className="font-medium mb-2 block" style={lightLabelStyle}>
                    Add Ingredients To Shopping List
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

                  <Button block htmlType="submit"
                          className="mb-12 !w-1/3"
                          type="primary"
                          disabled={fields.length === 0}
                  >
                    Submit Ingredient
                  </Button>

                  <Button type="dashed" onClick={() => add()} block icon={<PlusOutlined />}>
                    Add Ingredient
                  </Button>

                  {fields.length > 0 && (
                    <Button
                      className="mt-3"
                      type="primary"
                      htmlType="submit"
                      block
                    >
                      Add To Shopping List
                    </Button>
                  )}
                </>
              )}
            </Form.List>
          </Form>

          <div className="flex gap-2 w-full justify-end mt-4">
            <UpdateListButton updatedList={editableList} disabled={isDisabledUpdateButton} />
          </div>
        </>
      )}
    </Drawer>
  );
}

export default ShoppingList;