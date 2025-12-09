import { Drawer, Empty, List, Spin, Typography } from 'antd';
import { useShoppingListQuery } from '@hooks';
import { toast } from '@utils/toast.tsx';

interface Props {
  isCartOpen: boolean;
  handleCloseCart: () => void;
}

function ShoppingList({ isCartOpen, handleCloseCart }: Props) {
  const { shoppingList, isLoading, isFetching, isError, error } = useShoppingListQuery();
  const isBusy = isLoading || isFetching;

  if(!isBusy && isError){
    toast("Failed to load shopping list")
    console.error(error);
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
        <List
          itemLayout="horizontal"
          dataSource={shoppingList.ingredientUnitOfMeasures}
          renderItem={(item) => (
            <List.Item>
              <List.Item.Meta
                title={
                  <Typography.Text>
                    {item.ingredient.name}
                  </Typography.Text>
                }
                description={`${item.amount} ${item.unitOfMeasure}`}
              />
            </List.Item>
          )}
        />
      )}
    </Drawer>
  );
}

export default ShoppingList;