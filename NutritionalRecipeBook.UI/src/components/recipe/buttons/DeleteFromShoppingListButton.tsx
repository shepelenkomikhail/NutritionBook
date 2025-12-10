import { Button, Popconfirm } from 'antd';
import { DeleteOutlined } from '@ant-design/icons';
import { useDeleteShoppingListItemMutation } from '@api';
import { useState, useEffect } from 'react';
import { toast } from '@utils/toast.tsx';
import React from 'react';

function DeleteFromShoppingListButton({ itemId }: {itemId: string|undefined}) {
  const [deleteItem, { isLoading, isSuccess, isError }] = useDeleteShoppingListItemMutation();
  const [open, setOpen] = useState(false);

  const handleConfirm = async () => {
    await deleteItem(itemId!).unwrap();
  };

  useEffect(() => {
    if (isSuccess) {
      setOpen(false);
    }
    if (isError) {
      toast("Error deleting item");
      setOpen(false);
    }
  }, [isSuccess, isError]);

  return (
    <Popconfirm
      open={open}
      onOpenChange={(nextOpen) => {
        if (!isLoading) setOpen(nextOpen);
      }}
      title="Are you sure?"
      onConfirm={handleConfirm}
      okButtonProps={{ loading: isLoading }}
      onCancel={() => setOpen(false)}
    >
      <Button
        danger
        icon={<DeleteOutlined />}
        onClick={(e) => {
          e.stopPropagation();
          setOpen(true);
        }}
      />
    </Popconfirm>
  );
}

export default React.memo(DeleteFromShoppingListButton);
