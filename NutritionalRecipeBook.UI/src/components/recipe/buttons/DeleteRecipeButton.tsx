import { Button, Popconfirm } from 'antd';
import { DeleteOutlined } from '@ant-design/icons';
import { useDeleteRecipeMutation } from '@api';
import { toast } from '@utils/toast.tsx';

export function DeleteRecipeButton({ id }: {id: string}) {
  const [deleteRecipe, { isLoading }] = useDeleteRecipeMutation();

  const handleDelete = async () => {
    try {
      await deleteRecipe(id).unwrap();
      toast('Recipe deleted successfully!');
    } catch (error) {
      console.error('Failed to delete recipe:', error);
      toast('Failed to delete recipe');
    }
  };

  return (
    <Popconfirm
      title="Are you sure you want to delete this recipe?"
      onConfirm={handleDelete}
      okText="Yes"
      cancelText="No"
    >
      <Button
        type="primary"
        danger
        icon={<DeleteOutlined />}
        loading={isLoading}
      >
      </Button>
    </Popconfirm>
  );
}

export default DeleteRecipeButton;