import type { UnitOfMeasureModel } from './UnitOfMeasureModel';

export interface NutrientDTO {
  name: string;
  unitOfMeasureDTO: UnitOfMeasureModel;
  amount: number;
}
