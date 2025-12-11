import { Button } from 'antd';

interface Props {
  isPersonalized: boolean;
  setIsPersonalized: (value: boolean) => void;
  setIsFavorite: (value: boolean) => void;
}

function TogglePersonalizedButton({ isPersonalized, setIsPersonalized, setIsFavorite }: Props) {
  const handleClick = () => {
    setIsPersonalized(!isPersonalized);
    setIsFavorite(false);
  }

  return (
    <Button
      onClick={handleClick}
      className={"w-32"}
    >
      {!isPersonalized ? 'My Recipes' : 'All Recipes'}
    </Button>
  );
}

export default TogglePersonalizedButton;