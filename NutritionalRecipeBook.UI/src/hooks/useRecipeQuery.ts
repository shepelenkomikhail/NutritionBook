import { useState, useEffect } from 'react';
import { useLazyGetRecipesQuery, useLazyGetRecipesByUserQuery } from '@api';

export const useRecipeQuery = (isPersonalizedRequest: boolean) => {
  const [search, setSearch] = useState('');
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize] = useState(10);

  const [minCookingTimeInMin, setMinCookingTimeInMin] = useState<number | undefined>(undefined);
  const [maxCookingTimeInMin, setMaxCookingTimeInMin] = useState<number | undefined>(undefined);
  const [minServings, setMinServings] = useState<number | undefined>(undefined);
  const [maxServings, setMaxServings] = useState<number | undefined>(undefined);

  const [triggerUser, userResult] = useLazyGetRecipesByUserQuery();
  const [triggerPublic, publicResult] = useLazyGetRecipesQuery();

  const trigger = isPersonalizedRequest ? triggerUser : triggerPublic;
  const result = isPersonalizedRequest ? userResult : publicResult;

  useEffect(() => {
    const handler = setTimeout(() => {
      trigger({
        search,
        pageNumber,
        pageSize,
        minCookingTimeInMin,
        maxCookingTimeInMin,
        minServings,
        maxServings,
      });
    }, 200);

    return () => clearTimeout(handler);
  }, [
    search,
    pageNumber,
    minCookingTimeInMin,
    maxCookingTimeInMin,
    minServings,
    maxServings,
    trigger,
  ]);

  return {
    recipes: result.data?.items ?? [],
    totalCount: result.data?.totalCount ?? 0,
    isLoadingQuery: result.isLoading || result.isFetching,

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
