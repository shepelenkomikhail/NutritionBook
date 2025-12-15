import { useUpdateAllShoppingListItemsIsBoughtStatusMutation } from '@api';
import { toast } from '@utils/toast.tsx';
import { Button } from 'antd';
import { CheckCircleFilled, CheckOutlined } from '@ant-design/icons';

interface Props {
  isAllBought: boolean;
  setAllBought: (value: boolean) => void;
}

function MarkAsBoughtAllItemsButton({isAllBought, setAllBought}: Props) {
  const [updateItemStatus, { isLoading }] = useUpdateAllShoppingListItemsIsBoughtStatusMutation();

  const handleConfirm = async () => {
    try {
      await updateItemStatus({ isBought: !isAllBought }).unwrap();
      setAllBought(!isAllBought);
    } catch (e) {
      console.error(e);
      toast('Error updating item status');
    }
  };

  return (
    <Button
      onClick={handleConfirm}
      loading={isLoading}
      icon={
        isAllBought ? <CheckCircleFilled style={{ color: 'green' }} /> : <CheckOutlined />
      }
      className={"mr-2"}
    >
      {isAllBought ? 'Mark all as unbought' : 'Mark all as bought'}
    </Button>
  );
}

export default MarkAsBoughtAllItemsButton;