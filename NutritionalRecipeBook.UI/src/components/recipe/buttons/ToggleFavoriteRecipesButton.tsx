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
      className={"w-32"}
    >
      {!isFavorite ? 'Favorite Recipes' : 'All Recipes'}
    </Button>
  );
}

export default ToggleFavoriteRecipesButton;