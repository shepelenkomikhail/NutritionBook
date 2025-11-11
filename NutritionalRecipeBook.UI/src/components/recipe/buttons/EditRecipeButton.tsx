import { Button, Tooltip } from 'antd';
import { EditOutlined } from '@ant-design/icons';
import { RecipeModel } from '@models';

interface EditRecipeButtonProps {
  recipe: RecipeModel;
  onEdit: (recipe: RecipeModel) => void;
}

function EditRecipeButton({ recipe, onEdit }: EditRecipeButtonProps) {
  const handleEdit = () => {
    onEdit(recipe);
  };

  return (
    <Tooltip title="Edit Recipe">
      <Button
        type="default"
        icon={<EditOutlined />}
        onClick={handleEdit}
      />
    </Tooltip>
  );
}

export default EditRecipeButton;