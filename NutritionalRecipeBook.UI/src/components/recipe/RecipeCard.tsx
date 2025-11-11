import { RecipeModel } from '@models';
import { DeleteRecipeButton, EditRecipeButton } from './buttons/index.ts';
import { Card, Space } from 'antd';
import { useContext, useState } from 'react';
import { RecipeDetails } from './index.ts';
import { ThemeContext } from '../../layout/App';

interface RecipeCardProps {
  recipe: RecipeModel;
  onEdit: (recipe: RecipeModel) => void;
}

function RecipeCard({ recipe, onEdit }: RecipeCardProps) {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const {theme, } = useContext(ThemeContext);
  const isDark = theme === 'dark';

  const handleOpen = () => {
    setIsModalOpen(true);
  };

  const handleClose = () => setIsModalOpen(false);

  return (
    <>
      <button onClick={handleOpen} className="w-full">
        <Card
          hoverable
          className={`
            !rounded-2xl shadow-md hover:shadow-lg transition-all duration-300 relative
            ${isDark ? 'text-gray-100' : 'text-gray-900'}
          `}
          style={{backgroundColor: isDark ? '#1e293b' : 'whitesmoke'}}
          title={
            <span
              className={`font-semibold text-lg flex justify-between items-center ${
                isDark ? 'text-gray-100' : 'text-gray-900'
              }`}
            >
              {recipe.name}
              <Space>
                <EditRecipeButton recipe={recipe} onEdit={onEdit} />
                <DeleteRecipeButton id={recipe.id || 'error'} />
              </Space>
            </span>
          }
        >
          <p
            className={`text-sm mb-2 ${
              isDark ? 'text-gray-300' : 'text-gray-600'
            }`}
          >
            {recipe.description}
          </p>
          <p
            className={`text-xs ${
              isDark ? 'text-gray-400' : 'text-gray-500'
            }`}
          >
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