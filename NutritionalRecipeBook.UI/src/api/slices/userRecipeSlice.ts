import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RecipeModel } from '@models';

interface UserRecipeState {
  recipes: RecipeModel[];
}

const initialState: UserRecipeState = {
  recipes: [],
};

const userRecipesSlice = createSlice({
  name: 'userRecipes',
  initialState,
  reducers: {
    setUserRecipes: (state,
                     action: PayloadAction<{ recipes: RecipeModel[] }>) => {
      const { recipes }  = action.payload;
      console.log('SET USER RECIPES', recipes);
      state.recipes = recipes ;
    },
  },
});

export const { setUserRecipes } = userRecipesSlice.actions;
export default userRecipesSlice.reducer;
