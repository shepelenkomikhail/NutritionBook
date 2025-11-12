import { RecipeModel } from '@models';
import { RecipeCard } from './index.ts';
import { Col, Empty, Pagination, Row, Spin } from 'antd';
import { useContext } from 'react';
import { ThemeContext } from '../../layout/App.tsx';

interface Props {
  recipes: RecipeModel[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  onPageChange: (page: number) => void;
  isLoading: boolean;
  onEdit: (recipe: RecipeModel) => void;
}

function RecipeList({ recipes, totalCount, pageNumber, pageSize,
                             onPageChange, isLoading, onEdit }: Props) {
  const {theme, } = useContext(ThemeContext);
  const isDark = theme === 'dark';

  if (isLoading) return <Spin className="m-auto" tip="Loading recipes..." />;
  if (!recipes.length) return <Empty description="No recipes found" />;

  return (
    <div className="w-full">
      <Row gutter={[16, 16]}>
        {recipes.map((r) => (
          <Col xs={24} sm={12} md={8} lg={6} key={r.id}>
            <RecipeCard recipe={r} onEdit={onEdit}/>
          </Col>
        ))}
      </Row>

      <div className="flex justify-center mt-8">
        <Pagination
          current={pageNumber}
          pageSize={pageSize}
          total={totalCount}
          onChange={onPageChange}
          showSizeChanger={false}
          style={{
            color: isDark ? 'rgb(241 245 249)' : 'rgb(17 24 39)',
          }}
        />
      </div>
    </div>
  );
}

export default RecipeList;