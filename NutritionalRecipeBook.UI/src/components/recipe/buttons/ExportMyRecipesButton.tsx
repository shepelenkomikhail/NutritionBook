import { useLazyExportMyRecipesQuery } from '@api';
import { Button } from 'antd';
import { DownloadOutlined } from '@ant-design/icons';

function PrintShoppingListButton() {
  const [downloadJson, { isLoading }] = useLazyExportMyRecipesQuery();

  const handleDownload = async () => {
    const jsonBlob = await downloadJson().unwrap();

    const url = URL.createObjectURL(jsonBlob);
    const link = document.createElement("a");
    link.href = url;
    link.download = "my-recipes.json";
    link.click();

    URL.revokeObjectURL(url);
  };


  return (
    <Button
      type="primary"
      icon={!isLoading ? <DownloadOutlined /> : null}
      loading={isLoading}
      disabled={isLoading}
      onClick={handleDownload}
      className="mb-8 w-42"
    >
      {isLoading ? "Preparing JSON..." : "Export My Recipes"}
    </Button>
  );
}

export default PrintShoppingListButton;