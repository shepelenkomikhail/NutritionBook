import { useCallback, useState } from 'react';
import type { GetProp, UploadProps } from 'antd';
import { message } from 'antd';
import type { FormInstance } from 'antd';

type FileType = Parameters<GetProp<UploadProps, 'beforeUpload'>>[0];

type Options = {
  form?: FormInstance;
  imageFieldPath?: (string | number)[];
  onLoadingChange?: (loading: boolean) => void;
};

const getBase64 = (img: FileType, callback: (url: string) => void) => {
  const reader = new FileReader();
  reader.addEventListener('load', () => callback(reader.result as string));
  reader.readAsDataURL(img);
};

export function useRecipeImageUpload(opts?: Options) {
  const { form, imageFieldPath = ['imageUrl'], onLoadingChange } = opts || {};

  const [loading, setLoading] = useState(false);
  const [imageUrl, setImageUrl] = useState<string | undefined>(undefined);

  const notifyLoading = useCallback((val: boolean) => {
    setLoading(val);
    if (onLoadingChange) onLoadingChange(val);
  }, [onLoadingChange]);

  const beforeUpload: UploadProps['beforeUpload'] = useCallback((file: FileType) => {
    const isJpgOrPng = file.type === 'image/jpeg' || file.type === 'image/png';
    if (!isJpgOrPng) message.error('You can only upload JPG/PNG files!').then(() => {});
    const isLt2M = file.size / 1024 / 1024 < 2;
    if (!isLt2M) message.error('Image must be smaller than 2MB!').then(() => {});
    return isJpgOrPng && isLt2M;
  }, []);

  const handleChange: UploadProps['onChange'] = useCallback((info) => {
    if (info.file.status === 'uploading') {
      notifyLoading(true);
      return;
    }

    if (info.file.status === 'done') {
      const resp = info.file.response as { url?: string } | undefined;
      const serverUrl = resp?.url;

      if (serverUrl) {
        notifyLoading(false);
        setImageUrl(serverUrl);
        if (form) form.setFieldValue(imageFieldPath, serverUrl);
      } else if (info.file.originFileObj) {
        getBase64(info.file.originFileObj as FileType, (url) => {
          notifyLoading(false);
          setImageUrl(url);
          if (form) form.setFieldValue(imageFieldPath, url);
        });
      } else {
        notifyLoading(false);
      }
    }
  }, [form, imageFieldPath, notifyLoading]);

  return {
    imageUrl,
    setImageUrl,
    loading,
    beforeUpload,
    handleChange,
  } as const;
}
