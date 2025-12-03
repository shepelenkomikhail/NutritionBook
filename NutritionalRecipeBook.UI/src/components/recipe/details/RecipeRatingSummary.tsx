import { Rate, Typography } from 'antd';
const { Title } = Typography;

export function RecipeRatingSummary({ averageRating, totalCount }: { averageRating: number; totalCount: number }) {
  return (
    <div className="flex items-center gap-3 mb-2">
      <Title level={4} className="!text-[var(--fg)] !mb-0">
        Rating
      </Title>
      <Rate disabled value={averageRating} allowHalf />
      <span className="text-[var(--fg-muted)]">{averageRating} ({totalCount})</span>
    </div>
  );
}

export default RecipeRatingSummary;
