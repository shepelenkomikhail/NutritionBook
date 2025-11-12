import 'simplebar-react/dist/simplebar.min.css';
import SimpleBar from 'simplebar-react';
import { useContext, useState } from 'react';
import { PlusOutlined, SunOutlined, MoonOutlined } from '@ant-design/icons';
import { FloatButton, Layout, Modal, Spin, Button } from 'antd';
import Title from 'antd/es/typography/Title';
const { Content, Header } = Layout;
import { ThemeContext } from '../../layout/App';
import { useRecipeQuery } from '../../hooks/useRecipeQuery';
import { RecipeModel } from '@models'
import { RecipeList, RecipeSearchBar, RecipeForm } from './index.ts';

function Recipe() {
  const { theme, setTheme } = useContext(ThemeContext);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [editingRecipe, setEditingRecipe] = useState<RecipeModel | null>(null);

  const handleOpen = () => setIsModalOpen(true);
  const handleCancel = () => {
    setEditingRecipe(null);
    setIsModalOpen(false);
  };

  const {
    recipes, totalCount, search, setSearch, pageNumber, setPageNumber, pageSize, isLoadingQuery,
    minCookingTimeInMin, maxCookingTimeInMin, minServings, maxServings, setMinCookingTimeInMin,
    setMaxCookingTimeInMin, setMinServings, setMaxServings,
  } = useRecipeQuery();

  const handleOpenEdit = (recipe: RecipeModel) => {
    setEditingRecipe(recipe);
    setIsModalOpen(true);
  };

  const handleSubmit = () => {
    setIsModalOpen(false);
    setEditingRecipe(null);
  };

  const handleThemeToggle = () => {
    setTheme(theme === 'dark' ? 'light' : 'dark');
  };

  return (
    <>
      <Header className={`flex items-center justify-center w-full relative`}
        style={{
          backgroundColor: theme === 'dark' ? undefined : '#f9f5f0',
          color: theme === 'dark' ? '#ffffff' : '#ffffff',
          paddingTop: '24px',
        }}
      >
        <Button
          icon={
            theme === 'dark' ? (
              <SunOutlined style={{ color: 'white', fontSize: '24px' }} />
            ) : (
              <MoonOutlined style={{ color: 'white', fontSize: '24px' }} />
            )
          }
          type="primary"
          className="!absolute !h-12 !w-12"
          style={{ top: '16px', left: '16px', borderRadius: '50%' }}
          onClick={() => handleThemeToggle()}
        />
        <Title
          className="mb-0"
          style={{
            color: theme == 'dark' ? 'rgb(203 213 225)' : 'rgb(55 65 81)'
          }}
        >
          Recipes
        </Title>
      </Header>

      <Content
        className={`flex flex-col p-6 transition-all duration-300
        ${theme === 'dark' ? 'bg-slate-900 text-gray-100' : 'text-gray-800'}`}
        style={{
          backgroundColor: theme === 'dark' ? undefined : '#f9f5f0',
          minHeight: '100vh'
      }}
      >
        <RecipeSearchBar
          search={search}
          onSearchChange={(v) => {
            setSearch(v);
            setPageNumber(1);
          }}
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
          title={editingRecipe ? 'Edit Recipe' : 'Create New Recipe'}
          open={isModalOpen}
          onCancel={handleCancel}
          destroyOnClose
          footer={null}
          className="max-h-[70vh]"
          bodyStyle={{
            color: theme == 'dark' ? undefined : 'rgb(31 41 55)',
            backgroundColor: theme == 'dark' ? undefined : 'whitesmoke'
          }}
        >
          <Spin spinning={isLoadingQuery} tip="Processing...">
            <SimpleBar style={{ maxHeight: '60vh' }} autoHide={false}>
              <RecipeForm
                id={editingRecipe?.id || null}
                mode={editingRecipe ? 'update' : 'create'}
                initialValues={editingRecipe || undefined}
                onSubmit={handleSubmit}
                setIsLoading={setIsLoading}
              />
            </SimpleBar>
          </Spin>
        </Modal>
      </Content>
    </>
  );
}

export default Recipe;
