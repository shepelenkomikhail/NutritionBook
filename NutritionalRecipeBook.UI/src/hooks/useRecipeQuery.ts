import { useState, useEffect } from 'react';
import { useLazyGetRecipesQuery } from '@api';

export const useRecipeQuery = () => {
  const [search, setSearch] = useState('');
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize] = useState(5);
  const [minCookingTimeInMin, setMinCookingTimeInMin] = useState<number | undefined>(undefined);
  const [maxCookingTimeInMin, setMaxCookingTimeInMin] = useState<number | undefined>(undefined);
  const [minServings, setMinServings] = useState<number | undefined>(undefined);
  const [maxServings, setMaxServings] = useState<number | undefined>(undefined);

  const [getData, { data, isLoading, isFetching }] = useLazyGetRecipesQuery();

  useEffect(() => {
    const handler = setTimeout(() => {
      getData({ search, pageNumber, pageSize, minCookingTimeInMin,
        maxCookingTimeInMin, minServings, maxServings,
      });
    }, 400);

    return () => clearTimeout(handler);
  }, [search, pageNumber, minCookingTimeInMin,
    maxCookingTimeInMin, minServings, maxServings]);


  return {
    recipes: data?.items ?? [],
    totalCount: data?.totalCount ?? 0,
    isLoadingQuery: isLoading || isFetching,
    search,
    setSearch,
    pageNumber,
    setPageNumber,
    pageSize,
    minCookingTimeInMin,
    maxCookingTimeInMin,
    minServings,
    maxServings,
    setMinCookingTimeInMin: (v: number | undefined) => { setMinCookingTimeInMin(v); setPageNumber(1); },
    setMaxCookingTimeInMin: (v: number | undefined) => { setMaxCookingTimeInMin(v); setPageNumber(1); },
    setMinServings: (v: number | undefined) => { setMinServings(v); setPageNumber(1); },
    setMaxServings: (v: number | undefined) => { setMaxServings(v); setPageNumber(1); },
  };
};
