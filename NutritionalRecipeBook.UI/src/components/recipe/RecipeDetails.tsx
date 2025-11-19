import { useEffect } from 'react';
import { useLazyGetRecipeByIdQuery } from '@api';
import { Descriptions, Divider, List, Modal, Spin, Typography } from 'antd';
const { Title } = Typography;

interface RecipeModalProps {
  open: boolean;
  onClose: () => void;
  recipeId: string;
}

function RecipeDetails({ open, onClose, recipeId }: RecipeModalProps) {
  const [getData, { data: recipeData, isLoading }] = useLazyGetRecipeByIdQuery();

  useEffect(() => {
    if (open && recipeId) {
      getData(recipeId);
    }
  }, [open, recipeId, getData]);

  return (
    <Modal
      title={recipeData?.recipeDTO?.name || 'Recipe Details'}
      open={open}
      onCancel={onClose}
      footer={null}
      width={700}
      destroyOnClose
      bodyStyle={{
        color: 'var(--fg)',
        backgroundColor: 'var(--card)',
        borderColor: 'var(--border)'
      }}
    >
      {isLoading ? (
        <Spin className="w-full flex justify-center py-10" tip="Loading recipe details..." />
      ) : recipeData ? (
        <div className="p-4 rounded-lg bg-[var(--card)] text-[var(--fg)]">
          <Descriptions bordered column={1} size="small">
            <Descriptions.Item label="Description">
              {recipeData.recipeDTO.description}
            </Descriptions.Item>
            <Descriptions.Item label="Instructions">
              {recipeData.recipeDTO.instructions}
            </Descriptions.Item>
            <Descriptions.Item label="Cooking Time">
              {recipeData.recipeDTO.cookingTimeInMin} min
            </Descriptions.Item>
            <Descriptions.Item label="Servings">
              {recipeData.recipeDTO.servings}
            </Descriptions.Item>
          </Descriptions>

          <Divider className="my-4" />

          <Title level={4} className="!text-[var(--fg)]">
            Ingredients
          </Title>
          <List
            dataSource={recipeData.ingredients}
            renderItem={(item: any) => (
              <List.Item className="border-b last:border-0 border-[var(--border)] text-[var(--fg)]">
                <div className="flex justify-between w-full">
                  <span>{item.ingredientDTO.name}</span>
                  <span>
                    {item.amount} {item.unit}
                  </span>
                </div>
              </List.Item>
            )}
          />
        </div>
      ) : (
        <p className="text-center py-6 text-[var(--fg-muted)]">
          No recipe data found.
        </p>
      )}
    </Modal>
  );
}

export default RecipeDetails;