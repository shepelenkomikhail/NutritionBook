import { Input } from 'antd';
import { SearchOutlined } from '@ant-design/icons';

interface Props {
  search: string;
  onSearchChange: (value: string) => void;
}

export function RecipeSearchBar({ search, onSearchChange }: Props) {
  return (
    <div className="w-full max-w-md mb-6 mx-auto">
      <Input
        prefix={<SearchOutlined />}
        placeholder="Search recipes..."
        allowClear
        value={search}
        onChange={(e) => onSearchChange(e.target.value)}
        size="large"
        className="!rounded-2xl !py-2 shadow-sm"
      />
    </div>
  );
}
