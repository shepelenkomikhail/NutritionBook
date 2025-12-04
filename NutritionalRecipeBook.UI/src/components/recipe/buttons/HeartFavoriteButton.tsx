import { Button, Tooltip } from 'antd';
import { HeartFilled, HeartOutlined } from '@ant-design/icons';
import { useLazyGetFavoriteRecipesQuery } from '@api';
import { useRecipeMutation } from '@hooks';
import { RecipeModel, PagedResult } from '@models';
import { useEffect } from 'react';

interface Props {
  recipeId: string | undefined;
  className?: string;
  size?: 'small' | 'middle' | 'large';
  variant?: 'icon' | 'text';
}

function HeartFavoriteButton({ recipeId, className, size = 'middle', variant = 'icon' }: Props) {
  const [getFavorites, { data: favorites }] = useLazyGetFavoriteRecipesQuery();

  useEffect(() => {
    getFavorites(undefined);
  }, [getFavorites]);

  const { execute, isLoading } = useRecipeMutation();

  const favoriteItems: RecipeModel[] = (
    (favorites as PagedResult<RecipeModel> | undefined)?.items ??
    (Array.isArray(favorites) ? (favorites as RecipeModel[]) : [])
  );

  const isFavorite = favoriteItems.some((f: RecipeModel) => f.id === recipeId);

  const handleToggle = async (e: any) => {
    e.stopPropagation();
    if (!recipeId) return;

    try {
      await execute({ id: recipeId, mode: 'markFavorite' });
      // Ensure favorites are refreshed after toggle in case tag invalidation isn't enough
      getFavorites(undefined);
    } catch (err) {
      console.error(err);
    }
  };

  const content = (
    <Button
      type={variant === 'icon' ? 'text' : 'default'}
      size={size}
      className={className}
      aria-label={isFavorite ? 'Remove from favorites' : 'Add to favorites'}
      aria-pressed={isFavorite}
      onClick={(e) => handleToggle(e)}
      loading={isLoading}
      icon={
        isFavorite ? (
          <HeartFilled style={{ color: 'var(--fg)', fontSize: 18 }} />
        ) : (
          <HeartOutlined style={{ color: 'var(--fg-muted)', fontSize: 18 }} />
        )
      }
    >
      {variant === 'text' ? (isFavorite ? 'Favorited' : 'Favorite') : null}
    </Button>
  );

  return (
    <Tooltip title={isFavorite ? 'Remove from favorites' : 'Add to favorites'}>
      {content}
    </Tooltip>
  );
}

export default HeartFavoriteButton;