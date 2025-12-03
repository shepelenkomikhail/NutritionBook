export interface RecipeDTO {
  id?: string;
  name: string;
  description: string;
  imageUrl?: string;
  instructions: string;
  cookingTimeInMin: number;
  servings: number;
}
