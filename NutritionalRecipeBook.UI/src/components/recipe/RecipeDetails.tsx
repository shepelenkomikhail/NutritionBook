import { useEffect } from 'react';
import { useLazyGetRecipeByIdQuery } from '@api';
import { Descriptions, Divider, List, Modal, Spin, Typography } from 'antd';
import { ShowIngredientModel } from '@models';
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
      styles={{ body: {
        color: 'var(--fg)',
        backgroundColor: 'var(--card)',
        borderColor: 'var(--border)'
        }
      }}
    >
      {isLoading ? (
        <Spin className="w-full flex justify-center py-10" tip="Loading recipe details..." />
      ) : recipeData ? (
        <div className="p-4 rounded-lg bg-[var(--card)] text-[var(--fg)]">

          <Title level={4} className="!text-[var(--fg)]">
            Description
          </Title>
          <Title level={5}> { recipeData.recipeDTO.description } </Title>

          <Divider className="my-4" />

          <Title level={4} className="!text-[var(--fg)]">
            Ingredients
          </Title>
          <List
            dataSource={recipeData.ingredients}
            renderItem={(item: ShowIngredientModel) => (
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

          <Divider className="my-4" />

          <Title level={4} className="!text-[var(--fg)]">
            Instructions
          </Title>
          <Title level={5}> { recipeData.recipeDTO.instructions } </Title>

          <Divider className="my-4" />

          <Descriptions bordered column={1} size="small">
            <Descriptions.Item label="Cooking Time">
              {recipeData.recipeDTO.cookingTimeInMin} min
            </Descriptions.Item>
            <Descriptions.Item label="Servings">
              {recipeData.recipeDTO.servings}
            </Descriptions.Item>
          </Descriptions>
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