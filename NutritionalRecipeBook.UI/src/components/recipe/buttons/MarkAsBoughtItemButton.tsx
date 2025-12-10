import { Button } from 'antd';
import { CheckOutlined, CheckCircleFilled } from '@ant-design/icons';
import { useUpdateShoppingListItemIsBoughtStatusMutation } from '@api';
import { toast } from '@utils/toast.tsx';
import React from 'react';

function MarkAsBoughtItemButton({ itemId, isBought }: { itemId: string | undefined, isBought: boolean }) {
  const [updateItemStatus, { isLoading }] = useUpdateShoppingListItemIsBoughtStatusMutation();

  const handleConfirm = async () => {
    try {
      console.log('Updating item status', { itemId, isBought: !isBought });
      await updateItemStatus({ itemId, isBought: !isBought }).unwrap();
    } catch (e) {
      console.error(e);
      toast('Error updating item status');
    }
  };

  return (
      <Button
        onClick={handleConfirm}
        loading={isLoading}
        icon={
          isBought ? <CheckCircleFilled style={{ color: 'green' }} /> : <CheckOutlined />
        }
        className={"mr-2"}
      />
  );
}

export default React.memo(MarkAsBoughtItemButton);
