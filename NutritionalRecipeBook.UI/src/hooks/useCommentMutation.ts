import { useCreateCommentMutation } from '@api';
import { toast } from '@utils/toast.tsx';

export function useCommentMutation() {
  const [createComment, state] = useCreateCommentMutation();

  const submit = async (params: { recipeId: string; rating: number; content: string }) => {
    try {
      await createComment(params).unwrap();
      toast('Comment submitted successfully!');

      return true;
    } catch (error) {
      console.error('Failed to submit comment:', error);
      toast('Failed to submit comment');

      return false;
    }
  };

  return { submit, isLoading: state.isLoading, isError: state.isError };
}
