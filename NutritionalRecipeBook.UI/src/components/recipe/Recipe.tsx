import { useContext, useState } from 'react';
import { PlusOutlined, SunOutlined, MoonOutlined } from '@ant-design/icons';
import { RecipeModel } from '@models';
import { useRecipeMutation } from '../../hooks';
import { RecipeForm } from './RecipeForm';
import { FloatButton, Layout, Modal, Spin, Button } from 'antd';
import Title from 'antd/es/typography/Title';
import { ThemeContext } from '../../layout/App';
const { Content, Header } = Layout;

function Recipe() {
  const { theme, setTheme } = useContext(ThemeContext);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const { execute, isLoading } = useRecipeMutation('create');

  const handleOpen = () => setIsModalOpen(true);
  const handleCancel = () => setIsModalOpen(false);

  const handleSubmit = async (values: Omit<RecipeModel, 'id'>) => {
    await execute(values);
    setIsModalOpen(false);
  };

  const handleThemeToggle = () => {
    setTheme(theme === 'dark' ? 'light' : 'dark');
    console.log(theme);
  }

  return (
    <>
      <Header className={"flex items-center justify-center w-full relative"}>
        <Button
          icon={theme === 'dark' ? <SunOutlined style={{ color: 'white', fontSize: '24px'}} /> : <MoonOutlined style={{ color: 'white', fontSize: '24px'}} />}
          type="primary"
          className="!absolute top-2 right-4 !h-12 !w-12"
          onClick={handleThemeToggle}
        />
        <Title>Recipes</Title>
      </Header>

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
    </>
  );
}

export default Recipe;
