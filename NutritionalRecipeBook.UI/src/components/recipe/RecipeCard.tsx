import { Card, Space } from 'antd';
import { RecipeModel } from '@models';
import DeleteRecipeButton from './buttons/DeleteRecipeButton';
import EditRecipeButton  from './buttons/EditRecipeButton';

interface RecipeCardProps {
  recipe: RecipeModel;
  onEdit: (recipe: RecipeModel) => void;
}

function RecipeCard({ recipe, onEdit }: RecipeCardProps) {
  return (
    <Card
      hoverable
      className="!rounded-2xl shadow-md hover:shadow-lg transition-all duration-300 bg-white dark:bg-slate-800 relative"
      title={
        <span className="font-semibold text-lg flex justify-between items-center">
          {recipe.name}
          <Space>
            <EditRecipeButton recipe={recipe} onEdit={onEdit} />
            <DeleteRecipeButton id={recipe.id || "error"} />
          </Space>
        </span>
      }
    >
      <p className="text-sm text-gray-600 dark:text-gray-300 mb-2">
        {recipe.description}
      </p>
      <p className="text-xs text-gray-500 dark:text-gray-400">
        ⏱️ {recipe.cookingTimeInMin} min | 🍽️ Serves {recipe.servings}
      </p>
    </Card>
  );
}

export default RecipeCard;