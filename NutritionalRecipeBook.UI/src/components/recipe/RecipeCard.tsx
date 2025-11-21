import { RecipeModel } from '@models';
import { DeleteRecipeButton, EditRecipeButton } from './buttons/index.ts';
import { Card, Image, Space } from 'antd';
import { PictureOutlined } from '@ant-design/icons';
import { useState } from 'react';
import { RecipeDetails } from './index.ts';
import { useSelector } from 'react-redux';
import { RootState } from '@api';

interface RecipeCardProps {
  recipe: RecipeModel;
  onEdit: (recipe: RecipeModel) => void;
}

function RecipeCard({ recipe, onEdit }: RecipeCardProps) {
  const [isModalOpen, setIsModalOpen] = useState(false);

  const buildImageSrc = (url?: string) => {
    if (!url || url.trim() === '') return undefined;
    const trimmed = url.trim();
    if (/^(https?:)?\/\//i.test(trimmed) || /^(data:|blob:)/i.test(trimmed)) {
      return trimmed;
    }
    const base = import.meta.env.VITE_API_URL as string | undefined;
    if (base) {
      const sep = trimmed.startsWith('/') ? '' : '/';
      return `${base}${sep}${trimmed}`;
    }
    return trimmed;
  };

  const handleOpen = () => {
    setIsModalOpen(true);
  };

  const handleClose = () => setIsModalOpen(false);

  const ownedRecipes = useSelector((state: RootState) => state.userRecipes.recipes || []);
  const isOwnedRecipe = ownedRecipes.some((r: RecipeModel) => r.id === recipe.id);

  console.log('Owned Recipes:', ownedRecipes);
  
  return (
    <>
      <button onClick={handleOpen} className="w-full">
        <Card
          hoverable
          className={`!rounded-2xl shadow-md hover:shadow-lg transition-all duration-300 relative ds-card`}
          cover={(() => {
            const src = buildImageSrc(recipe.imageUrl);
            return src ? (
              <Image
                alt={recipe.name}
                src={src}
                preview={false}
                className="object-cover"
                style={{ height: '180px', width: '100%', padding: '12px', borderRadius: '20px' }}
              />
            ) : (
              <div
                className="h-44 w-full flex items-center justify-center bg-[var(--card)] text-[var(--fg-muted)]"
                aria-label="No image available"
                title="No image available"
              >
                <PictureOutlined style={{ fontSize: 48 }} />
              </div>
            );
          })()}
          title={
            <span
              className={`font-semibold text-lg flex justify-between items-center text-[var(--fg)]`}
            >
              {recipe.name}
              <Space>
                { isOwnedRecipe && <EditRecipeButton recipe={recipe} onEdit={onEdit} /> }
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