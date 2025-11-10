import { useState } from 'react';
import { PlusOutlined } from '@ant-design/icons';
import { Modal, FloatButton, Spin, Layout } from 'antd';
import { RecipeForm } from './RecipeForm';
import { useRecipeMutation } from '../../hooks';
import { RecipeModel } from '@models';

const { Content } = Layout;

function Recipe() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const { execute, isLoading } = useRecipeMutation('create');

  const handleOpen = () => setIsModalOpen(true);
  const handleCancel = () => setIsModalOpen(false);

  const handleSubmit = async (values: Omit<RecipeModel, 'id'>) => {
    await execute(values);
    setIsModalOpen(false);
  };

  return (
    <Content className="p-6 relative min-h-screen flex">
      <FloatButton
        icon={<PlusOutlined />}
        type="primary"
        tooltip={<div>Add Recipe</div>}
        onClick={handleOpen}
      />

      <Modal
        title="Create New Recipe"
        open={isModalOpen}
        onCancel={handleCancel}
        destroyOnClose
        footer={null}
        className="max-h-[70vh] overflow-y-auto"
      >
        <Spin spinning={isLoading} tip="Processing...">
          <RecipeForm mode="create" onSubmit={handleSubmit} isLoading={isLoading} />
        </Spin>
      </Modal>
    </Content>
  );
}

export default Recipe;
