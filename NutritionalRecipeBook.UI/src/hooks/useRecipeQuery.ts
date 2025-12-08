import { useState, useEffect, useCallback } from 'react';
import { useLazyGetRecipesQuery, useLazyGetRecipesByUserQuery, useLazyGetFavoriteRecipesQuery, } from '@api';

export const useRecipeQuery = (
  isPersonalizedRequest: boolean,
  isFavoriteRequest: boolean
) => {
  const [search, setSearch] = useState('');
  const [pageNumber, setPageNumber] = useState(1);
  const pageSize = 10;

  const [minCookingTimeInMin, setMinCookingTimeInMin] = useState<number | undefined>(undefined);
  const [maxCookingTimeInMin, setMaxCookingTimeInMin] = useState<number | undefined>(undefined);
  const [minServings, setMinServings] = useState<number | undefined>(undefined);
  const [maxServings, setMaxServings] = useState<number | undefined>(undefined);
  const [minCaloriesPerServing, setMinCalories] = useState<number | undefined>(undefined);
  const [maxCaloriesPerServing, setMaxCalories] = useState<number | undefined>(undefined);

  const [triggerPublic, publicResult] = useLazyGetRecipesQuery();
  const [triggerUser, userResult] = useLazyGetRecipesByUserQuery();
  const [triggerFavorite, favoriteResult] = useLazyGetFavoriteRecipesQuery();

  const trigger = isFavoriteRequest
    ? triggerFavorite
    : isPersonalizedRequest
      ? triggerUser
      : triggerPublic;

  const result = isFavoriteRequest
    ? favoriteResult
    : isPersonalizedRequest
      ? userResult
      : publicResult;

  const buildParams = useCallback(() => {
    return {
      search,
      pageNumber,
      pageSize,
      minCookingTimeInMin,
      maxCookingTimeInMin,
      minServings,
      maxServings,
      minCaloriesPerServing,
      maxCaloriesPerServing
    };
  }, [
    search,
    pageNumber,
    pageSize,
    minCookingTimeInMin,
    maxCookingTimeInMin,
    minServings,
    maxServings,
    minCaloriesPerServing,
    maxCaloriesPerServing
  ]);

  console.log(buildParams());


  useEffect(() => {
    const handler = setTimeout(() => {
      trigger(buildParams());
    }, 200);

    return () => clearTimeout(handler);

  }, [trigger, buildParams]);

  const isLoadingQuery = result.isLoading || result.isFetching;

  return {
    recipes: result.data?.items ?? [],
    totalCount: result.data?.totalCount ?? 0,
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
    minCaloriesPerServing,
    maxCaloriesPerServing,

    setMinCookingTimeInMin: (v: number | undefined) => {
      setMinCookingTimeInMin(v);
      setPageNumber(1);
    },
    setMaxCookingTimeInMin: (v: number | undefined) => {
      setMaxCookingTimeInMin(v);
      setPageNumber(1);
    },
    setMinServings: (v: number | undefined) => {
      setMinServings(v);
      setPageNumber(1);
    },
    setMaxServings: (v: number | undefined) => {
      setMaxServings(v);
      setPageNumber(1);
    },
    setMinCalories: (v: number | undefined) => {
      setMinCalories(v);
      setPageNumber(1);
    },
    setMaxCalories: (v: number | undefined) => {
      setMaxCalories(v);
      setPageNumber(1);
    }
  };
};