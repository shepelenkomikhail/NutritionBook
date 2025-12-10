import { Drawer, Empty, List, Spin, Typography } from 'antd';
import { useShoppingListQuery } from '@hooks';
import DeleteFromShoppingListButton from './buttons/DeleteFromShoppingListButton.tsx';
import MarkAsBoughtItemButton from './buttons/MarkAsBoughtItemButton.tsx';
import MarkAsBoughtAllItemsButton from './buttons/MarkAsBoughtAllItemsButton.tsx';
import { useState } from 'react';
import { ClearShoppingListButton } from './buttons';

interface Props {
  isCartOpen: boolean;
  handleCloseCart: () => void;
}

function ShoppingList({ isCartOpen, handleCloseCart }: Props) {
  const { shoppingList, isLoading, isFetching, isError, error } = useShoppingListQuery();
  const isBusy = isLoading || isFetching;
  const [isAllBought, setIsAllBought] = useState(false);

  if(!isBusy && isError){
    console.log("shopping list does not exist or another error:", error);
  }

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
            dataSource={shoppingList.ingredientUnitOfMeasures}
            renderItem={(item) => (
              <List.Item>
                <List.Item.Meta
                  title={
                    <Typography.Text className={`${item.isBought ? 'line-through !text-gray-500' : ''}`}>
                      {item.ingredient.name}
                    </Typography.Text>
                  }
                  description={`${item.amount} ${item.unitOfMeasure}`}
                />
                <MarkAsBoughtItemButton itemId={item.ingredient.id} isBought={item.isBought} />
                <DeleteFromShoppingListButton itemId={item.ingredient.id} />
              </List.Item>
            )}
          />
        </>
      )}
    </Drawer>
  );
}

export default ShoppingList;