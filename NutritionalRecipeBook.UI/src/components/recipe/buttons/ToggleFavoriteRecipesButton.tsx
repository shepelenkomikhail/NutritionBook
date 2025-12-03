import { Button } from 'antd';

interface Props {
  isFavorite: boolean;
  setIsFavorite: (value: boolean) => void;
}

function ToggleFavoriteRecipesButton({ isFavorite, setIsFavorite }: Props) {
  return (
    <Button
      onClick={() => setIsFavorite(!isFavorite)}
      className={"!absolute right-20"}
      style={{ left: "10rem" }}
    >
      {!isFavorite ? 'Favorite Recipes' : 'All Recipes'}
    </Button>
  );
}

export default ToggleFavoriteRecipesButton;