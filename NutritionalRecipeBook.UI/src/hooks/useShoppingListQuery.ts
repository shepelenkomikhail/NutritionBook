import { useGetShoppingListQuery } from '@api';

export const useShoppingListQuery = () => {
  const result = useGetShoppingListQuery();

  return {
    shoppingList: result.data ?? null,
    isLoading: result.isLoading,
    isFetching: result.isFetching,
    isError: result.isError,
    error: result.error,
    raw: result,
  };
};