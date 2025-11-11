import { Button, Tooltip } from 'antd';
import { EditOutlined } from '@ant-design/icons';
import { RecipeModel } from '@models';
import React from 'react';

interface EditRecipeButtonProps {
  recipe: RecipeModel;
  onEdit: (recipe: RecipeModel) => void;
}

function EditRecipeButton({ recipe, onEdit }: EditRecipeButtonProps) {
  const handleEdit = (e: React.MouseEvent<HTMLElement, MouseEvent>) => {
    e.stopPropagation();
    onEdit(recipe);
  };

  return (
    <Tooltip title="Edit Recipe">
      <Button
        type="default"
        icon={<EditOutlined />}
        onClick={(e) => {handleEdit(e)}}
      />
    </Tooltip>
  );
}

export default EditRecipeButton;