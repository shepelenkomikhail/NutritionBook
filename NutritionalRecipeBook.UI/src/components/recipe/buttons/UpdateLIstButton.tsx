import { UserIngredientUnitOfMeasuresModel } from '@models';
import { useUpdateShoppingListMutation } from '@api';
import { Button } from 'antd';
import { ReloadOutlined  } from '@ant-design/icons';
import { toast } from '@utils/toast.tsx';

function UpdateLIstButton({ updatedList, disabled }: { updatedList: any, disabled: boolean }) {
  const [updateList, { isLoading }] = useUpdateShoppingListMutation();

  const handleConfirm = async () => {
    try {
      let ingredientUnitOfMeasures = [];

      if (Array.isArray(updatedList)) {
        ingredientUnitOfMeasures = updatedList.map((i) => ({
          ingredient: {
            id: i.ingredient?.id ?? undefined,
            name: i.ingredient?.name ?? i.name,
            isLiquid: (i.ingredient?.isLiquid ?? i.isLiquid) ?? false,
            amount: Number(i.amount ?? i.ingredient?.amount ?? 0),
            unit: i.unit ?? i.unitOfMeasure ?? i.ingredient?.unit,
          },
          unitOfMeasure: i.unitOfMeasure ?? i.unit ?? i.ingredient?.unit,
          amount: Number(i.amount ?? i.ingredient?.amount ?? 0),
          isBought: !!i.isBought,
        }));
      } else if (updatedList && Array.isArray(updatedList.ingredientUnitOfMeasures)) {
        ingredientUnitOfMeasures = updatedList.ingredientUnitOfMeasures;
      } else {
        ingredientUnitOfMeasures = [];
      }

      const payload: UserIngredientUnitOfMeasuresModel = {
        ingredientUnitOfMeasures,
      };

      await updateList(payload).unwrap();
      toast('Successfully updated list!');
    } catch (e) {
      console.error(e);
      toast('Error updating list');
    }
  }

  return (
    <Button
      onClick={handleConfirm}
      loading={isLoading}
      icon={ <ReloadOutlined  /> }
      className={"mt-4"}
      disabled={disabled}
    >
      Update
    </Button>
  );
}

export default UpdateLIstButton;