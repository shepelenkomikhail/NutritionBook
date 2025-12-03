import { Button } from 'antd';

interface Props {
  isFavorite: boolean;
  setIsFavorite: (value: boolean) => void;
  setIsPersonalized: (value: boolean) => void;
}

function ToggleFavoriteRecipesButton({ isFavorite, setIsFavorite, setIsPersonalized }: Props) {
  const handleClick = () => {
    setIsFavorite(!isFavorite);
    setIsPersonalized(false);
  }

  return (
    <Button
      onClick={handleClick}
      className={"!absolute"}
      style={{ left: "10rem" }}
    >
      {!isFavorite ? 'Favorite Recipes' : 'All Recipes'}
    </Button>
  );
}

export default ToggleFavoriteRecipesButton;