import { Upload, Button } from "antd";
import { UploadOutlined } from "@ant-design/icons";
import { useUploadRecipeJsonMutation } from "@api";
import { useState } from 'react';
import { toast } from '@utils/toast';

function UploadJsonRecipe() {
  const [uploadJson, { isLoading }] = useUploadRecipeJsonMutation();
  const [file, setFile] = useState<File | null>(null);

  const handleChange = (info: any) => {
    console.log(info);
    const selected: string = info.file.type;
    console.log(selected);

    if (selected !== "application/json") {
      toast("Only JSON files allowed");
      return;
    }

    setFile(info.file);
  };

  const handleUpload = async () => {
    if (!file) return toast("Choose a file first");

    try {
      await uploadJson(file).unwrap();
      toast("Uploaded successfully!");
    } catch (err) {
      console.error(err);
      toast("Upload failed");
    }
  };

  return (
    <div
      className={"!absolute flex flex-col justify-end"}
       style={{right: "2rem" }}
    >
      <Upload beforeUpload={() => false} accept=".json" maxCount={1} onChange={handleChange}>
        <Button icon={<UploadOutlined />}>Choose JSON file</Button>
      </Upload>

      <Button type="primary" onClick={handleUpload} loading={isLoading} disabled={!file} style={{ marginTop: 16 }}>
        Upload
      </Button>
    </div>
  );
}

export default UploadJsonRecipe;