import { RecipeModel } from '@models';
import { DeleteRecipeButton, EditRecipeButton } from './buttons/index.ts';
import { Card, Space } from 'antd';
import { useState } from 'react';
import { RecipeDetails } from './index.ts';

interface RecipeCardProps {
  recipe: RecipeModel;
  onEdit: (recipe: RecipeModel) => void;
}

function RecipeCard({ recipe, onEdit }: RecipeCardProps) {
  const [isModalOpen, setIsModalOpen] = useState(false);

  const handleOpen = () => {
    setIsModalOpen(true);
  };

  const handleClose = () => setIsModalOpen(false);

  return (
    <>
      <button onClick={handleOpen} className="w-full">
        <Card
          hoverable
          className={`!rounded-2xl shadow-md hover:shadow-lg transition-all duration-300 relative ds-card`}
          title={
            <span
              className={`font-semibold text-lg flex justify-between items-center text-[var(--fg)]`}
            >
              {recipe.name}
              <Space>
                <EditRecipeButton recipe={recipe} onEdit={onEdit} />
                <DeleteRecipeButton id={recipe.id || 'error'} />
              </Space>
            </span>
          }
        >
          <p className={`text-sm mb-2 text-[var(--fg-muted)]`}>
            {recipe.description}
          </p>
          <p className={`text-xs text-[var(--fg-muted)]`}>
            ⏱️ {recipe.cookingTimeInMin} min | 🍽️ Serves {recipe.servings}
          </p>
        </Card>
      </button>

      <RecipeDetails
        open={isModalOpen}
        onClose={handleClose}
        recipeId={recipe.id!}
      />
    </>
  );
}

export default RecipeCard;