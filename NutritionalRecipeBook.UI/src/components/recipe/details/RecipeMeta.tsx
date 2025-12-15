import { Descriptions } from 'antd';

export function RecipeMeta({ cookingTimeInMin, servings }: { cookingTimeInMin: number; servings: number }) {
  return (
    <Descriptions bordered column={1} size="small">
      <Descriptions.Item label="Cooking Time">{cookingTimeInMin} min</Descriptions.Item>
      <Descriptions.Item label="Servings">{servings}</Descriptions.Item>
    </Descriptions>
  );
}

export default RecipeMeta;
