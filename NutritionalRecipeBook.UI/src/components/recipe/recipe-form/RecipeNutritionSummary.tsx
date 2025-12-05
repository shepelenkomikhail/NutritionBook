import { Form } from 'antd';
import type { FormInstance } from 'antd';
import { calculateNutritionTotals } from './calculateNutritionTotals';
import type { FormIngredientModel } from '@models';
import { lightLabelStyle } from '../../../themes/modelStyles';

interface Props {
  form: FormInstance;
}

function RecipeNutritionSummary({ form }: Props) {
  return (
    <Form.Item shouldUpdate noStyle>
      {() => {
        const rows = (form.getFieldValue('ingredients') as FormIngredientModel[] | undefined) ?? [];
        const totals = calculateNutritionTotals(rows);

        return (
          <div className="mt-4 border border-[var(--border)] rounded-md p-3">
            <div className="font-medium mb-2" style={lightLabelStyle}>Current Nutritional Content</div>
            <div className="grid grid-cols-4 gap-2 text-[var(--fg)]">
              <div>
                <div className="text-[var(--fg-muted)]">Calories</div>
                <div>{totals.calories} kcal</div>
              </div>
              <div>
                <div className="text-[var(--fg-muted)]">Proteins</div>
                <div>{totals.proteins} g</div>
              </div>
              <div>
                <div className="text-[var(--fg-muted)]">Carbs</div>
                <div>{totals.carbs} g</div>
              </div>
              <div>
                <div className="text-[var(--fg-muted)]">Fats</div>
                <div>{totals.fats} g</div>
              </div>
            </div>
            <div className="text-xs text-[var(--fg-muted)] mt-2">
              Note: Nutrients are computed when the selected unit matches the ingredient's base unit (g/ml).
            </div>
          </div>
        );
      }}
    </Form.Item>
  );
}

export default RecipeNutritionSummary;
