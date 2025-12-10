import { useClearShoppingListMutation } from '@api';
import { toast } from '@utils/toast.tsx';
import { Button, Popconfirm } from 'antd';
import { DeleteOutlined } from '@ant-design/icons';

function ClearShoppingListButton() {
  const [deleteRecipes, { isLoading }] = useClearShoppingListMutation();

  const handleDelete = async (e: React.MouseEvent<HTMLElement, MouseEvent> | undefined) => {
    if(typeof e !== 'undefined')
      e.stopPropagation();

    try {
      await deleteRecipes().unwrap();
      toast('Recipe deleted successfully!');
    } catch (error) {
      console.error('Failed to delete recipe:', error);
      toast('Failed to delete recipe');
    }
  };

  return (
    <Popconfirm
      title="Are you sure you want to delete this recipe?"
      onConfirm={(e) => handleDelete(e)}
      okText="Yes"
      cancelText="No"
    >
      <Button
        type="primary"
        danger
        icon={<DeleteOutlined />}
        loading={isLoading}
        onClick={(e) => e.stopPropagation()}
      >
        Delete All Items
      </Button>
    </Popconfirm>
  );
}

export default ClearShoppingListButton;