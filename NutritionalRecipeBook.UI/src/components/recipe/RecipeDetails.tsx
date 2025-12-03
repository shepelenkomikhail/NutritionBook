import { useEffect, useMemo, useState } from 'react';
import { useLazyGetRecipeByIdQuery, useLazyGetCommentsQuery, useDeleteCommentMutation, useLazyGetMyCommentsQuery } from '@api';
import { Divider, Modal, Spin, Typography, List } from 'antd';
import { type CommentModel, ShowIngredientModel, type PagedResult } from '@models';
import { useCommentMutation } from '@hooks';
import { toast } from '@utils/toast.tsx';
import RecipeRatingSummary from './details/RecipeRatingSummary';
import RecipeImage from './details/RecipeImage';
import RecipeMeta from './details/RecipeMeta';
import CommentsList from './details/CommentsList';
import CommentForm from './details/CommentForm';
const { Title } = Typography;

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
        <Spin className="w-full flex justify-center py-10" tip="Loading recipe details..." />
      ) : recipeData ? (
        <div className="p-4 rounded-lg bg-[var(--card)] text-[var(--fg)]">
          <RecipeRatingSummary averageRating={averageRating} totalCount={comments.length} />

          <Divider className="my-4" />

          <div className="w-full mb-4">
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

          <Spin spinning={isCommentsLoading} tip="Loading comments...">
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