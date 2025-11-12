import { useContext, useEffect } from 'react';
import { useLazyGetRecipeByIdQuery } from '@api';
import { ThemeContext } from '../../layout/App.tsx';
import { Descriptions, Divider, List, Modal, Spin, Typography } from 'antd';
const { Title } = Typography;

interface RecipeModalProps {
  open: boolean;
  onClose: () => void;
  recipeId: string;
}

function RecipeDetails({ open, onClose, recipeId }: RecipeModalProps) {
  const [getData, { data: recipeData, isLoading }] = useLazyGetRecipeByIdQuery();
  const { theme } = useContext(ThemeContext);

  useEffect(() => {
    if (open && recipeId) {
      getData(recipeId);
    }
  }, [open, recipeId, getData]);

  const isDark = theme === 'dark';

  const containerClass = `
    p-4 rounded-lg
   ${isDark ? 'bg-slate-800 text-gray-100' : 'bg-white text-gray-900'}
  `;

  return (
    <Modal
      title={recipeData?.recipeDTO?.name || 'Recipe Details'}
      open={open}
      onCancel={onClose}
      footer={null}
      width={700}
      destroyOnClose
      className={isDark ? 'dark-modal' : ''}
      bodyStyle={{
        backgroundColor: isDark ? '#1e293b' : 'whitesmoke',
      }}
    >
      {isLoading ? (
        <Spin className="w-full flex justify-center py-10" tip="Loading recipe details..." />
      ) : recipeData ? (
        <div className={containerClass}>
          <Descriptions
            bordered
            column={1}
            size="small"
            labelStyle={{
              color: isDark ? 'rgb(203 213 225)' : 'rgb(55 65 81)',
              backgroundColor: isDark ? 'rgb(30 41 59)' : 'rgb(243 244 246)',
              borderColor: isDark ? 'rgb(71 85 105)' : 'rgb(209 213 219)',
            }}
            contentStyle={{
              color: isDark ? 'rgb(241 245 249)' : 'rgb(17 24 39)',
              backgroundColor: isDark ? 'rgb(15 23 42)' : 'rgb(255 255 255)',
              borderColor: isDark ? 'rgb(71 85 105)' : 'rgb(209 213 219)',
            }}
          >
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

          <Divider className={`my-4}`} style={{
            borderColor: isDark ? 'rgb(71 85 105)' : 'rgb(209 213 219)',
          }}/>

          <Title
            level={4}
            style={{
              color: isDark ? 'rgb(203 213 225)' : 'rgb(55 65 81)'
            }}
          >
            Ingredients
          </Title>
          <List
            dataSource={recipeData.ingredients}
            renderItem={(item: any) => (
              <List.Item
                className="border-b last:border-0"
                style={{
                  borderColor: isDark ? 'rgb(51 65 85)' : 'rgb(209 213 219)',
                  color: isDark ? 'rgb(203 213 225)' : 'rgb(55 65 81)'
                }}
              >
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
        <p
          className={`text-center py-6 ${
            isDark ? 'text-gray-400' : 'text-gray-500'
          }`}
        >
          No recipe data found.
        </p>
      )}
    </Modal>
  );
}

export default RecipeDetails;