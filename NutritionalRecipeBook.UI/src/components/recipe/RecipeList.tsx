import { RecipeModel } from '@models';
import { RecipeCard } from './index.ts';
import { Col, Empty, Pagination, Row, Spin } from 'antd';

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
        />
      </div>
    </div>
  );
}

export default RecipeList;