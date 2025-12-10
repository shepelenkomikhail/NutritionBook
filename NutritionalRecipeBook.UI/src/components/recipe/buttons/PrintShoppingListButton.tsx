import { useGetPrintedShoppingListQuery } from '@api';
import { toast } from '@utils/toast.tsx';
import { Button, Popconfirm } from 'antd';
import { DeleteOutlined } from '@ant-design/icons';

function PrintShoppingListButton() {
  const result = useGetPrintedShoppingListQuery();

  const handleClick = async () => {
    try {
      if (!result.data) {
        toast('Failed to generate shopping list!');
      }

      toast('Recipe deleted successfully!');
    } catch (error) {
      console.error('Failed to delete recipe:', error);
      toast('Failed to delete recipe');
    }
  };

  return (
    <Popconfirm
      title="Are you sure you want to delete this recipe?"
      onConfirm={handleClick}
      okText="Yes"
      cancelText="No"
    >
      <Button
        type="primary"
        danger
        icon={<DeleteOutlined />}
        loading={result.isLoading}
        onClick={(e) => e.stopPropagation()}
      >
        Delete All Items
      </Button>
    </Popconfirm>
  );
}

export default PrintShoppingListButton;