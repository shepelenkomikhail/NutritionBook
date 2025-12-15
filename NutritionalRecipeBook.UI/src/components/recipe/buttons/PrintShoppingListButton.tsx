import { useLazyGetPrintedShoppingListQuery } from '@api';
import { Button } from 'antd';
import { DownloadOutlined } from '@ant-design/icons';

function PrintShoppingListButton() {
  const [downloadPdf, { isLoading }] = useLazyGetPrintedShoppingListQuery();

  const handleDownload = async () => {
    const pdfBlob = await downloadPdf().unwrap();

    const url = URL.createObjectURL(pdfBlob);
    const link = document.createElement("a");
    link.href = url;
    link.download = "shopping-list.pdf";
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
      className="flex items-center gap-2 ml-4"
    >
      {isLoading ? "Preparing PDF..." : "Download PDF"}
    </Button>
  );
}

export default PrintShoppingListButton;