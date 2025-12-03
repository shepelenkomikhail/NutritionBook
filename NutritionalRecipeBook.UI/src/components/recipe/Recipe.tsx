import 'simplebar-react/dist/simplebar.min.css';
import SimpleBar from 'simplebar-react';
import { useEffect, useState } from 'react';
import { PlusOutlined, LogoutOutlined } from '@ant-design/icons';
import { Button, FloatButton, Layout, Modal, Spin } from 'antd';
import Title from 'antd/es/typography/Title';
import { useRecipeQuery } from '@hooks';
import { useLazyGetRecipesByUserQuery } from '@api';
import { RecipeModel } from '@models'
import { RecipeList, RecipeSearchBar, RecipeForm } from './index.ts';
import { ThemeToggleButton } from '../shared';
import { RootState } from '@api';
import { useDispatch, useSelector } from 'react-redux';
import { logout } from '../../api/slices/authSlice.ts';
import { TogglePersonalizedButton, ToggleFavoriteRecipesButton } from './buttons';
import { setUserRecipes } from '../../api/slices/userRecipeSlice.ts';
const { Content, Header } = Layout;

function Recipe() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [, setIsLoading] = useState(false);
  const [editingRecipe, setEditingRecipe] = useState<RecipeModel | null>(null);
  const { username, token } = useSelector((state: RootState) => state.auth);
  const ownedRecipes = useSelector((state: RootState) => state.userRecipes.recipes || []);
  const [isPersonalizedRecipes, setIsPersonalizedRecipes] = useState<boolean>(false);
  const [isFavoriteRecipes, setIsFavoriteRecipes] = useState<boolean>(false);

  const dispatch = useDispatch();
  const [triggerOwnedFetch] = useLazyGetRecipesByUserQuery();

  const handleLogout = () => {
    dispatch(logout());
    window.location.reload();
  };

  const handleOpen = () => setIsModalOpen(true);
  const handleCancel = () => {
    setEditingRecipe(null);
    setIsModalOpen(false);
  };

  const {
    recipes, totalCount, search, setSearch, pageNumber, setPageNumber, pageSize, isLoadingQuery,
    minCookingTimeInMin, maxCookingTimeInMin, minServings, maxServings, setMinCookingTimeInMin,
    setMaxCookingTimeInMin, setMinServings, setMaxServings,
  } = useRecipeQuery(isPersonalizedRecipes, isFavoriteRecipes);

  const handleOpenEdit = (recipe: RecipeModel) => {
    setEditingRecipe(recipe);
    setIsModalOpen(true);
  };

  const handleSubmit = () => {
    setIsModalOpen(false);
    setEditingRecipe(null);
  };

  useEffect(() => {
    const shouldBootstrap = Boolean(token) && ownedRecipes.length === 0;
    if (!shouldBootstrap) return;

    triggerOwnedFetch({ pageNumber: 1, pageSize: 50 })
      .unwrap()
      .then((res) => {
        const items = res?.items ?? [];
        dispatch(setUserRecipes({ recipes: items }));
      })
      .catch(() => {
      });
  }, [token, ownedRecipes.length, triggerOwnedFetch, dispatch]);

  return (
    <>
      <Header className={`w-full !bg-[var(--bg)] !text-[var(--fg)] border-b border-[var(--border)]`}
      >
        <div className="max-w-7xl mx-auto px-4 h-16 grid grid-cols-3 items-center">
          <div className="flex items-center gap-3">
            <ThemeToggleButton variant="inline" />
          </div>

          <div className="flex items-center justify-center">
            <Title level={3} className="!mb-0 !text-[var(--fg)]">
              Recipes
            </Title>
          </div>

          <div className="flex items-center justify-end gap-6">
            <Title level={5} className="!mb-0 !text-[var(--fg-muted)]">
              Hello, {username || 'Guest'}
            </Title>
            <Button
              type="primary"
              icon={<LogoutOutlined />}
              danger
              size="small"
              onClick={handleLogout}
            >
              Logout
            </Button>
          </div>
        </div>
      </Header>

      <Content className={`flex flex-col p-6 transition-all duration-100 bg-[var(--bg)] text-[var(--fg)] min-h-screen`}>
        <TogglePersonalizedButton
          isPersonalized={isPersonalizedRecipes}
          setIsPersonalized={setIsPersonalizedRecipes}
          setIsFavorite={setIsFavoriteRecipes}
        />
        <ToggleFavoriteRecipesButton
          isFavorite={isFavoriteRecipes}
          setIsFavorite={setIsFavoriteRecipes}
          setIsPersonalized={setIsPersonalizedRecipes}
        />
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
          isLoading={isLoadingQuery}
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
          styles={{
            body: {
              color: 'var(--fg)',
              backgroundColor: 'var(--card)',
              borderColor: 'var(--border)'
            }
          }}
        >
          <Spin spinning={isLoadingQuery}>
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
