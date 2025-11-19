import { SearchOutlined } from '@ant-design/icons';
import { Button, Input, InputNumber } from 'antd';
import { lightInputStyle, lightLabelStyle } from '../../themes/modelStyles.ts';

interface Props {
  search: string;
  onSearchChange: (value: string) => void;
  minCookingTimeInMin?: number;
  maxCookingTimeInMin?: number;
  minServings?: number;
  maxServings?: number;
  onMinCookingTimeChange: (value: number | undefined) => void;
  onMaxCookingTimeChange: (value: number | undefined) => void;
  onMinServingsChange: (value: number | undefined) => void;
  onMaxServingsChange: (value: number | undefined) => void;
  onClearFilters: () => void;
}

function RecipeSearchBar({ search, onSearchChange, minCookingTimeInMin, maxCookingTimeInMin,
                           minServings, maxServings, onMinCookingTimeChange, onMaxCookingTimeChange,
                           onMinServingsChange, onMaxServingsChange, onClearFilters, }: Props) {

  return (
    <div className="flex flex-col mb-6 w-3/4 self-center">
      <div className="flex justify-center mb-4">
        <Input
          prefix={<SearchOutlined />}
          placeholder="Search recipes..."
          allowClear
          value={search}
          onChange={(e) => onSearchChange(e.target.value)}
          size="large"
          className="!w-4/6 shadow-sm custom-input"
          style={lightInputStyle}
        />
      </div>

      <div className="flex flex-wrap gap-4 items-end justify-between">
        <div className="flex flex-col w-full sm:w-1/5">
          <label className="mb-1 text-sm font-medium" style={lightLabelStyle}>Cooking time min (min)</label>
          <InputNumber
            min={0}
            step={5}
            value={minCookingTimeInMin}
            onChange={(v) => onMinCookingTimeChange(v ?? undefined)}
            className="w-full custom-input"
            style={lightInputStyle}
            placeholder="Min"
          />
        </div>

        <div className="flex flex-col w-full sm:w-1/5">
          <label className="mb-1 text-sm font-medium" style={lightLabelStyle}>Cooking time max (min)</label>
          <InputNumber
            min={0}
            step={5}
            value={maxCookingTimeInMin}
            onChange={(v) => onMaxCookingTimeChange(v ?? undefined)}
            className="w-full custom-input"
            style={lightInputStyle}
            placeholder="Max"
          />
        </div>

        <div className="flex flex-col w-full sm:w-1/5">
          <label className="mb-1 text-sm font-medium" style={lightLabelStyle}>Servings min</label>
          <InputNumber
            min={1}
            value={minServings}
            onChange={(v) => onMinServingsChange(v ?? undefined)}
            className="w-full custom-input"
            style={lightInputStyle}
            placeholder="Min"
          />
        </div>

        <div className="flex flex-col w-full sm:w-1/5">
          <label className="mb-1 text-sm font-medium" style={lightLabelStyle}>Servings max</label>
          <InputNumber
            min={1}
            value={maxServings}
            onChange={(v) => onMaxServingsChange(v ?? undefined)}
            className="w-full custom-input"
            style={lightInputStyle}
            placeholder="Max"
          />
        </div>

        <div className="flex justify-end w-full sm:w-auto mt-2 sm:mt-0">
          <Button
            onClick={onClearFilters}
            className="custom-input"
            style={lightInputStyle}
          >
            Clear filters
          </Button>
        </div>
      </div>

      <style>{`
        .custom-input input::placeholder {
          color: var(--fg-muted) !important;
          opacity: 1;
        }
      `}</style>
    </div>
  );
}

export default RecipeSearchBar;