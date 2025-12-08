import { useEffect, useMemo, useState } from 'react';
import { useLazyGetRecipeByIdQuery, useLazyGetCommentsQuery, useDeleteCommentMutation, useLazyGetMyCommentsQuery } from '@api';
import { Divider, Modal, Spin, Typography, List } from 'antd';
import { type CommentModel, ShowIngredientModel, type PagedResult, IngredientNutritionInfoModel } from '@models';
import { useCommentMutation } from '@hooks';
import { toast } from '@utils/toast.tsx';
import RecipeRatingSummary from './details/RecipeRatingSummary';
import RecipeImage from './details/RecipeImage';
import RecipeMeta from './details/RecipeMeta';
import CommentsList from './details/CommentsList';
import { HeartFavoriteButton } from './buttons';
import { useIngredientsQuery } from '@hooks';
import CommentForm from './details/CommentForm.tsx';
const { Title } = Typography;
import { calculateNutritionTotals } from '@utils/calculateNutritionTotals';

interface RecipeModalProps {
  open: boolean;
  onClose: () => void;
  recipeId: string;
}

function RecipeDetails({ open, onClose, recipeId }: RecipeModalProps) {
  const [getData, { data: recipeData, isLoading }] = useLazyGetRecipeByIdQuery();
  const { submit, isLoading: isSubmitting } = useCommentMutation();
  const [triggerComments, { data: commentsData, isLoading: isCommentsLoading }] = useLazyGetCommentsQuery();
  const [triggerMyComments, { data: myCommentsData }] = useLazyGetMyCommentsQuery();
  const { ingredients: catalog } = useIngredientsQuery();
  const [page, setPage] = useState<number>(1);
  const pageSize = 3;
  const [deletingId, setDeletingId] = useState<string | null>(null);
  const [deleteComment] = useDeleteCommentMutation();
  
  const handleSubmit = async (values: { rating: number; content: string }) => {
    if (!recipeId) return;
    const ok = await submit({ recipeId, rating: values.rating, content: values.content?.trim() });
    if (ok) {
      getData(recipeId);
      triggerComments({ recipeId });
      triggerMyComments({ recipeId });
    }
  }

  const handleDeleteComment = async (item: CommentModel, currentPageCount: number) => {
    if (!item.id) return;
    const allow = myCommentIds.has(item.id);
    if (!allow) {
      toast('You can only delete your own comments.');
      return;
    }
    try {
      setDeletingId(item.id);
      if (currentPageCount === 1 && page > 1) {
        setPage(page - 1);
      }
      await deleteComment({ commentId: item.id }).unwrap();
      toast('Comment deleted successfully!');
      triggerComments({ recipeId });
      triggerMyComments({ recipeId });
    } catch (err) {
      console.error('Failed to delete comment:', err);
      toast('Failed to delete comment');
    } finally {
      setDeletingId(null);
    }
  };

  useEffect(() => {
    if (open && recipeId) {
      getData(recipeId);

      triggerComments({ recipeId });
      triggerMyComments({ recipeId });
      setPage(1);
    }
  }, [open, recipeId, getData, triggerComments, triggerMyComments]);

  const comments: CommentModel[] = useMemo(() => {
    const list = (commentsData ?? []) as CommentModel[];

    return Array.isArray(list) ? list : [];
  }, [commentsData]);

  const averageRating = useMemo(() => {
    if (!comments.length) return 0;
    const sum = comments.reduce((acc, c) => acc + (Number(c.rating) || 0), 0);

    return Number((sum / comments.length).toFixed(1));
  }, [comments]);

  const myCommentIds = useMemo(() => {
    const raw = (myCommentsData ?? []) as CommentModel[] | PagedResult<CommentModel>;
    const items = Array.isArray(raw) ? raw : (raw?.items ?? []);
    const ids = (items as CommentModel[])
      .map(c => c.id)
      .filter((id): id is string => typeof id === 'string' && !!id);
    return new Set(ids);
  }, [myCommentsData]);

  const nutritionTotals = useMemo(() => {
    if (!recipeData?.ingredients) return { calories: 0, proteins: 0, carbs: 0, fats: 0 };

    const formIngredients = recipeData.ingredients.map(item => {
      const ingInfo: IngredientNutritionInfoModel | undefined =
        catalog.find(c => c.name === item.ingredientDTO.name);

      if (!ingInfo) return null;

      return {
        id: item.ingredientDTO.id ?? undefined,
        name: item.ingredientDTO.name,
        amount: Number(item.amount),
        unit: item.unit,
        uom: ingInfo.uom,
        caloriesPer100: ingInfo.calories ?? 0,
        proteinsPer100: ingInfo.proteins ?? 0,
        carbsPer100: ingInfo.carbs ?? 0,
        fatsPer100: ingInfo.fats ?? 0,
        isLiquid: ingInfo.isLiquid ?? false,
      };
    }).filter(Boolean);

    return calculateNutritionTotals(formIngredients as any);
  }, [recipeData?.ingredients, catalog]);

  return (
    <Modal
      title={recipeData?.recipeDTO?.name || 'Recipe Details'}
      open={open}
      onCancel={onClose}
      footer={null}
      width={700}
      rootClassName="recipe-details-modal"
      destroyOnClose
      styles={{ body: {
        color: 'var(--fg)',
        backgroundColor: 'var(--card)',
        borderColor: 'var(--border)'
        }
      }}
    >
      {isLoading ? (
        <Spin className="w-full flex justify-center py-10" />
      ) : recipeData ? (
        <div className="p-4 rounded-lg bg-[var(--card)] text-[var(--fg)]">
          <div className="absolute right-12 top-20 z-10">
            <HeartFavoriteButton recipeId={recipeId} />
          </div>

          <RecipeRatingSummary averageRating={averageRating} totalCount={comments.length} />

          <Divider className="my-4" />

          <div className="w-full mb-4 relative">
            <RecipeImage
              name={recipeData.recipeDTO?.name}
              imageUrl={recipeData.recipeDTO?.imageUrl}
              className="object-cover"
              style={{ height: '180px', width: '100%', padding: '12px', borderRadius: '20px' }}
            />
          </div>

          <Title level={4} className="!text-[var(--fg)]">
            Description
          </Title>
          <Title level={5}> { recipeData.recipeDTO.description } </Title>

          <Divider className="my-4" />

          <Title level={4} className="!text-[var(--fg)]">
            Ingredients
          </Title>
          <List
            dataSource={recipeData.ingredients}
            renderItem={(item: ShowIngredientModel) => (
              <List.Item className="border-b last:border-0 border-[var(--border)] text-[var(--fg)]">
                <div className="flex justify-between w-full">
                  <span>{item.ingredientDTO.name}</span>
                  <span>
                    {item.amount} {item.unit}
                  </span>
                </div>
              </List.Item>
            )}
          />

          <div className="mt-4 border border-[var(--border)] rounded-md p-3">
            <div className="font-medium mb-2">Nutritional Content</div>
            <div className="grid grid-cols-4 gap-2 text-[var(--fg)]">
              <div>
                <div className="text-[var(--fg-muted)]">Calories</div>
                <div>{nutritionTotals.calories} kcal</div>
              </div>
              <div>
                <div className="text-[var(--fg-muted)]">Proteins</div>
                <div>{nutritionTotals.proteins} g</div>
              </div>
              <div>
                <div className="text-[var(--fg-muted)]">Carbs</div>
                <div>{nutritionTotals.carbs} g</div>
              </div>
              <div>
                <div className="text-[var(--fg-muted)]">Fats</div>
                <div>{nutritionTotals.fats} g</div>
              </div>
            </div>
          </div>

          <Divider className="my-4" />

          <Title level={4} className="!text-[var(--fg)]">
            Instructions
          </Title>
          <Title level={5}> { recipeData.recipeDTO.instructions } </Title>

          <Divider className="my-4" />

          <RecipeMeta
            cookingTimeInMin={recipeData.recipeDTO.cookingTimeInMin}
            servings={recipeData.recipeDTO.servings}
          />

          <Divider className="my-4" />
          <Title level={4} className="!text-[var(--fg)]">
            Comments
          </Title>

          <Spin spinning={isCommentsLoading} >
            {comments.length === 0 ? (
              <p className="text-[var(--fg-muted)]">No comments yet. Be the first to leave one!</p>
            ) : (
              <CommentsList
                comments={comments}
                page={page}
                setPage={setPage}
                pageSize={pageSize}
                myCommentIds={myCommentIds}
                deletingId={deletingId}
                onDelete={handleDeleteComment}
              />
            )}
          </Spin>

          <Divider className="my-4" />

          <Title level={4} className="!text-[var(--fg)]">
            Leave a Comment
          </Title>

          <CommentForm onSubmit={handleSubmit} loading={isSubmitting} />
        </div>
      ) : (
        <p className="text-center py-6 text-[var(--fg-muted)]">
          No recipe data found.
        </p>
      )}
    </Modal>
  );
}

export default RecipeDetails;