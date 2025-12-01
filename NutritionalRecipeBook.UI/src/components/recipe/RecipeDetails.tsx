import { useEffect } from 'react';
import { useLazyGetRecipeByIdQuery } from '@api';
import { Button, Descriptions, Divider, Form, Image, Input, List, Modal, Rate, Spin, Typography } from 'antd';
import { PictureOutlined } from '@ant-design/icons';
import { type CommentModel, ShowIngredientModel } from '@models';
import { formContainerLightStyle, lightLabelStyle } from '../../themes/modelStyles.ts';
import TextArea from 'antd/es/input/TextArea';
import { useCommentMutation } from '@hooks';
const { Title } = Typography;

interface RecipeModalProps {
  open: boolean;
  onClose: () => void;
  recipeId: string;
}

function RecipeDetails({ open, onClose, recipeId }: RecipeModalProps) {
  const [getData, { data: recipeData, isLoading }] = useLazyGetRecipeByIdQuery();
  const [form] = Form.useForm<CommentModel>();
  const { submit, isLoading: isSubmitting } = useCommentMutation();

  const buildImageSrc = (url?: string) => {
    if (!url || url.trim() === '') return undefined;
    const trimmed = url.trim();
    if (/^(https?:)?\/\//i.test(trimmed) || /^(data:|blob:)/i.test(trimmed)) {
      return trimmed;
    }
    const base = import.meta.env.VITE_API_URL as string | undefined;
    if (base) {
      const sep = trimmed.startsWith('/') ? '' : '/';
      return `${base}${sep}${trimmed}`;
    }
    return trimmed;
  };

  const handleSubmit = async (values: { rating: number; content: string }) => {
    if (!recipeId) return;
    const ok = await submit({ recipeId, rating: values.rating, content: values.content?.trim() });
    if (ok) {
      form.resetFields(['rating', 'content']);
      getData(recipeId);
    }
  }

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
      rootClassName="recipe-details-modal"
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
          <div className="w-full mb-4">
            {(() => {
              const src = buildImageSrc((recipeData as any).recipeDTO?.imageUrl);
              return src ? (
                <Image
                  alt={recipeData.recipeDTO?.name || 'Recipe image'}
                  src={src}
                  preview={false}
                  className="object-cover"
                  style={{ height: '180px', width: '100%', padding: '12px', borderRadius: '20px' }}

                />
              ) : (
                <div
                  className="h-56 w-full flex items-center justify-center bg-[var(--card)] text-[var(--fg-muted)] rounded-md"
                  aria-label="No image available"
                  title="No image available"
                >
                  <PictureOutlined style={{ fontSize: 56 }} />
                </div>
              );
            })()}
          </div>

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

          <Divider className="my-4" />
          <Title level={4} className="!text-[var(--fg)]">
            Leve a Comment
          </Title>

          <Form
            form={form}
            layout="vertical"
            onFinish={() => handleSubmit(form.getFieldsValue() as any)}
            className="w-11/12 !p-4 rounded-lg bg-[var(--card)] text-[var(--fg)] border border-[var(--border)]"
            style={formContainerLightStyle}
          >
            <Form.Item
              name="rating"
              label={<span style={lightLabelStyle}>Rating</span>}
              rules={[{ required: true, message: 'Please, rate this recipe!' }]}
            >
              <Rate id="rating" allowClear={false} />
            </Form.Item>

            <Form.Item
              name="content"
              label={<span style={lightLabelStyle}>Comment</span>}
              rules={[{ required: true, message: 'Please, write a comment!' }]}
            >
              <TextArea
                placeholder="Your comment goes here..."
              />
            </Form.Item>

            <Button
              type="primary"
              htmlType="submit"
              className={"mt-4"}
              loading={isSubmitting}
            >
              {isSubmitting ? 'Submitting...' : 'Submit'}
            </Button>
          </Form>
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