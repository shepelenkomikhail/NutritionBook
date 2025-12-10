import { Drawer, Empty, List, Spin, Typography, InputNumber } from 'antd';
import { useShoppingListQuery } from '@hooks';
import { DeleteFromShoppingListButton, MarkAsBoughtItemButton,
  ClearShoppingListButton, MarkAsBoughtAllItemsButton, UpdateListButton } from './buttons';
import { useEffect, useState } from 'react';
import { IngredientUnitOfMeasureModel,  } from '@models';

interface Props {
  isCartOpen: boolean;
  handleCloseCart: () => void;
}

function ShoppingList({ isCartOpen, handleCloseCart }: Props) {
  const { shoppingList, isLoading, isFetching, isError, error } = useShoppingListQuery();
  const isBusy = isLoading || isFetching;
  const [isAllBought, setIsAllBought] = useState(false);
  const [isDisabledUpdateButton, setIsDisabledUpdateButton] = useState(true);

  const [editableList, setEditableList] = useState<IngredientUnitOfMeasureModel[]>([]);
  const [originalList, setOriginalList] = useState<IngredientUnitOfMeasureModel[]>([]);

  useEffect(() => {
    if (shoppingList && shoppingList.ingredientUnitOfMeasures) {
      const cloned: IngredientUnitOfMeasureModel[] = shoppingList.ingredientUnitOfMeasures.map((i: IngredientUnitOfMeasureModel) => ({ ...i }));
      setEditableList(cloned);
      setOriginalList(cloned.map((i: IngredientUnitOfMeasureModel) => ({ ...i })));
      setIsDisabledUpdateButton(true);
    } else {
      setEditableList([]);
      setOriginalList([]);
      setIsDisabledUpdateButton(true);
    }
  }, [shoppingList]);

  if(!isBusy && isError){
    console.log("shopping list does not exist or another error:", error);
  }

  const handleAmountChange = (itemId: number | string | undefined, value: number | undefined) => {
    const updated = editableList.map((it) =>
      it.ingredient?.id === itemId ? { ...it, amount: value ?? 0 } : it
    );
    setEditableList(updated);

    const changed = JSON.stringify(updated) !== JSON.stringify(originalList);
    setIsDisabledUpdateButton(!changed);
  };

  return (
    <Drawer
      title="Shopping list"
      placement="right"
      open={isCartOpen}
      onClose={handleCloseCart}
      width={400}
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

      {!isBusy && !isError && (!shoppingList || shoppingList.ingredientUnitOfMeasures.length === 0) && (
        <Empty description="No items in your shopping list yet" />
      )}

      {!isBusy && !isError && shoppingList && shoppingList.ingredientUnitOfMeasures.length > 0 && (
        <>
          <div className={"flex gap-2"}>
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

          <div className={"flex gap-2 !w-full justify-end"}>
            <UpdateListButton updatedList={editableList} disabled={isDisabledUpdateButton} />
          </div>
        </>
      )}
    </Drawer>
  );
}

export default ShoppingList;