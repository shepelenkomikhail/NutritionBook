import { useContext, useState } from 'react';
import { PlusOutlined, SunOutlined, MoonOutlined } from '@ant-design/icons';
import { RecipeModel } from '@models';
import { useRecipeMutation } from '../../hooks';
import { RecipeForm } from './RecipeForm';
import { FloatButton, Layout, Modal, Spin, Button } from 'antd';
import Title from 'antd/es/typography/Title';
import { ThemeContext } from '../../layout/App';
import { RecipeSearchBar } from './RecipeSearchBar.tsx';
import { RecipeList } from './RecipeList.tsx';
import { useRecipeQuery } from '../../hooks/useRecipeQuery.ts';
const { Content, Header } = Layout;
import SimpleBar from 'simplebar-react';
import 'simplebar-react/dist/simplebar.min.css';

function Recipe() {
  const { theme, setTheme } = useContext(ThemeContext);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const { execute, isLoading } = useRecipeMutation('create');

  const handleOpen = () => setIsModalOpen(true);
  const handleCancel = () => {
    setEditingRecipe(null);
    setIsModalOpen(false);
  };

  const [editingRecipe, setEditingRecipe] = useState<RecipeModel | null>(null);

  const { recipes, totalCount, search, setSearch, pageNumber, setPageNumber, pageSize, isLoadingQuery,
    minCookingTimeInMin, maxCookingTimeInMin, minServings, maxServings, setMinCookingTimeInMin,
    setMaxCookingTimeInMin, setMinServings, setMaxServings,
  } = useRecipeQuery();

  const handleOpenEdit = (recipe: RecipeModel) => {
    setEditingRecipe(recipe);
    setIsModalOpen(true);
  };

  const handleSubmit = async (values: Omit<RecipeModel, 'id'>) => {
    await execute(values);
    setIsModalOpen(false);
    setEditingRecipe(null);
  };

  const handleThemeToggle = () => {
    setTheme(theme === 'dark' ? 'light' : 'dark');
    console.log(theme);
  }

  return (
    <>
      <Header className="flex items-center justify-center w-full relative">
        <Button
          icon={
            theme === 'dark' ? (
              <SunOutlined style={{ color: 'white', fontSize: '24px' }} />
            ) : (
              <MoonOutlined style={{ color: 'white', fontSize: '24px' }} />
            )
          }
          type="primary"
          className="!absolute top-2 right-4 !h-12 !w-12"
          onClick={() => handleThemeToggle()}
        />
        <Title className="text-white mb-0">Recipes</Title>
      </Header>

      <Content className={`flex flex-col p-6 min-h-screen
      ${theme === 'dark' ? 'bg-slate-900 text-gray-100' : 'bg-orange-200 text-gray-800'}`}>
        <RecipeSearchBar
          search={search}
          onSearchChange={(v) => { setSearch(v); setPageNumber(1); }}
          minCookingTimeInMin={minCookingTimeInMin}
          maxCookingTimeInMin={maxCookingTimeInMin}
          minServings={minServings}
          maxServings={maxServings}
          onMinCookingTimeChange={setMinCookingTimeInMin}
          onMaxCookingTimeChange={setMaxCookingTimeInMin}
          onMinServingsChange={setMinServings}
          onMaxServingsChange={setMaxServings}
          onClearFilters={() => {
            setMinCookingTimeInMin(undefined);
            setMaxCookingTimeInMin(undefined);
            setMinServings(undefined);
            setMaxServings(undefined);
            setPageNumber(1);
          }}
        />

        <RecipeList
          recipes={recipes}
          totalCount={totalCount}
          pageNumber={pageNumber}
          pageSize={pageSize}
          onPageChange={setPageNumber}
          isLoading={isLoading}
          onEdit={handleOpenEdit}
        />

        <FloatButton
          icon={<PlusOutlined />}
          type="primary"
          tooltip={<div>Add Recipe</div>}
          onClick={handleOpen}
        />

        <Modal
          title={editingRecipe ? "Edit Recipe" : "Create New Recipe"}
          open={isModalOpen}
          onCancel={handleCancel}
          destroyOnClose
          footer={null}
          className="max-h-[70vh]"
        >
          <Spin spinning={isLoadingQuery} tip="Processing...">
            <SimpleBar style={{ maxHeight: '60vh' }} autoHide={false}>
              <RecipeForm
                mode={editingRecipe ? "update" : "create"}
                initialValues={editingRecipe || undefined}
                onSubmit={handleSubmit}
                isLoading={isLoadingQuery}
              />
            </SimpleBar>
          </Spin>
        </Modal>
      </Content>
    </>
  );
}

export default Recipe;
