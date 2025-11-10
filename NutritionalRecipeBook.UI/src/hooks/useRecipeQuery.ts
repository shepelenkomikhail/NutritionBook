import { useState, useEffect } from 'react';
import { useLazyGetRecipesQuery } from '@api';

export const useRecipeQuery = () => {
  const [search, setSearch] = useState('');
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize] = useState(10);

  const [getData, { data, isLoading, isFetching }] = useLazyGetRecipesQuery();

  useEffect(() => {
    getData({ search, pageNumber, pageSize });
    }, [search, pageNumber]
  );

  return {
    recipes: data?.items ?? [],
    totalCount: data?.totalCount ?? 0,
    isLoadingQuery: isLoading || isFetching,
    search,
    setSearch,
    pageNumber,
    setPageNumber,
    pageSize,
  };
};
