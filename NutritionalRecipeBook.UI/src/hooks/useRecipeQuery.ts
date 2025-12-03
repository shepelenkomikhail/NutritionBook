import { useState, useEffect, useMemo } from 'react';
import { useLazyGetRecipesQuery, useLazyGetRecipesByUserQuery, useGetFavoriteRecipesQuery } from '@api';

export const useRecipeQuery = (isPersonalizedRequest: boolean, isFavoriteRequest: boolean) => {
  const [search, setSearch] = useState('');
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize] = useState(10);

  const [minCookingTimeInMin, setMinCookingTimeInMin] = useState<number | undefined>(undefined);
  const [maxCookingTimeInMin, setMaxCookingTimeInMin] = useState<number | undefined>(undefined);
  const [minServings, setMinServings] = useState<number | undefined>(undefined);
  const [maxServings, setMaxServings] = useState<number | undefined>(undefined);

  const [triggerUser, userResult] = useLazyGetRecipesByUserQuery();
  const [triggerPublic, publicResult] = useLazyGetRecipesQuery();
  const favoriteResult = useGetFavoriteRecipesQuery(undefined, { skip: !isFavoriteRequest });

  const trigger = isPersonalizedRequest ? triggerUser : triggerPublic;
  const result = isPersonalizedRequest ? userResult : publicResult;

  useEffect(() => {
    if (isFavoriteRequest) return;
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
    isFavoriteRequest,
  ]);

  const favoritePaged = useMemo(() => {
    if (!isFavoriteRequest) return { items: [], total: 0 };
    const items = favoriteResult.data ?? [];
    const normalizedSearch = (search || '').trim().toLowerCase();
    const filtered = items.filter((r) => {
      const matchesSearch = !normalizedSearch
        || r.name.toLowerCase().includes(normalizedSearch)
        || (r.description || '').toLowerCase().includes(normalizedSearch);
      const matchesMinTime = typeof minCookingTimeInMin === 'number' ? r.cookingTimeInMin >= minCookingTimeInMin : true;
      const matchesMaxTime = typeof maxCookingTimeInMin === 'number' ? r.cookingTimeInMin <= maxCookingTimeInMin : true;
      const matchesMinServ = typeof minServings === 'number' ? r.servings >= minServings : true;
      const matchesMaxServ = typeof maxServings === 'number' ? r.servings <= maxServings : true;
      return matchesSearch && matchesMinTime && matchesMaxTime && matchesMinServ && matchesMaxServ;
    });
    const total = filtered.length;
    const start = (pageNumber - 1) * pageSize;
    const end = start + pageSize;
    return { items: filtered.slice(start, end), total };
  }, [
    isFavoriteRequest,
    favoriteResult.data,
    search,
    pageNumber,
    pageSize,
    minCookingTimeInMin,
    maxCookingTimeInMin,
    minServings,
    maxServings,
  ]);

  const isLoadingQuery = isFavoriteRequest
    ? (favoriteResult.isLoading || favoriteResult.isFetching)
    : (result.isLoading || result.isFetching);

  return {
    recipes: isFavoriteRequest ? favoritePaged.items : (result.data?.items ?? []),
    totalCount: isFavoriteRequest ? favoritePaged.total : (result.data?.totalCount ?? 0),
    isLoadingQuery,

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
