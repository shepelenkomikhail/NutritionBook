import { Button } from 'antd';

interface Props {
  isPersonalized: boolean;
  setIsPersonalized: (value: boolean) => void;
}

function TogglePersonalizedButton({ isPersonalized, setIsPersonalized }: Props) {
  return (
    <Button
      onClick={() => setIsPersonalized(!isPersonalized)}
      className={"!absolute"}
    >
      {!isPersonalized ? 'My Recipes' : 'All Recipes'}
    </Button>
  );
}

export default TogglePersonalizedButton;