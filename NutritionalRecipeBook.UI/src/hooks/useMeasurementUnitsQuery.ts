import { useGetMeasurementUnitsQuery } from '@api';
import type { UnitOfMeasureModel } from '@models';

export function useMeasurementUnitsQuery(params: { isLiquid: boolean }) {
  const { data, isLoading, isFetching, isError, error } = useGetMeasurementUnitsQuery(params);

  const units: UnitOfMeasureModel[] = data ?? [];

  return {
    units,
    isLoading: isLoading || isFetching,
    isError,
    error,
  } as const;
}
