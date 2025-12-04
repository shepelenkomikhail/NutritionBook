import { Image } from 'antd';
import { PictureOutlined } from '@ant-design/icons';
import { CSSProperties, useEffect, useMemo, useState } from 'react';

interface SecureImageProps {
  src?: string;
  alt?: string;
  className?: string;
  style?: CSSProperties;
  preview?: boolean;
  draggable?: boolean;
}

const buildAbsoluteUrl = (url?: string) => {
  if (!url) return undefined;
  const trimmed = url.trim();
  if (!trimmed) return undefined;
  if (/^(https?:)?\/\//i.test(trimmed) || /^(data:|blob:)/i.test(trimmed)) {
    return trimmed;
  }
  const base = import.meta.env.VITE_API_URL as string | undefined;
  if (base) {
    const sep = trimmed.startsWith('/') ? '' : '/';

    return `${base}${sep}${trimmed}`;
  }

  return trimmed;
};

function SecureImage({ src, alt, className, style, preview = false, draggable }: SecureImageProps) {
  const absoluteUrl = useMemo(() => buildAbsoluteUrl(src), [src]);
  const [blobUrl, setBlobUrl] = useState<string | undefined>(undefined);
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState<boolean>(false);

  useEffect(() => {
    let aborted = false;
    let currentObjectUrl: string | undefined;

    async function load() {
      if (!absoluteUrl) {
        setBlobUrl(undefined);
        setError(null);
        return;
      }

      if (/^(data:|blob:)/i.test(absoluteUrl)) {
        setBlobUrl(absoluteUrl);
        setError(null);

        return;
      }

      setLoading(true);
      setError(null);

      try {
        const token = localStorage.getItem('token');
        const res = await fetch(absoluteUrl, {
          headers: token ? { Authorization: `Bearer ${token}` } : undefined,
          credentials: 'include',
        });

        if (!res.ok) {
          throw new Error(`HTTP ${res.status}`);
        }

        const blob = await res.blob();
        if (aborted) return;

        currentObjectUrl = URL.createObjectURL(blob);

        setBlobUrl(currentObjectUrl);
      } catch (e: unknown) {
        if (!aborted) {
          const msg = e instanceof Error ? e.message : 'Failed to load image';
          setError(msg);
          setBlobUrl(undefined);
        }
      } finally {
        if (!aborted) setLoading(false);
      }
    }

    load();

    return () => {
      aborted = true;
      if (currentObjectUrl) URL.revokeObjectURL(currentObjectUrl);
    };
  }, [absoluteUrl]);

  if (!absoluteUrl || error) {
    return (
      <div
        className={`flex items-center justify-center bg-[var(--card)] text-[var(--fg-muted)] ${className || ''}`}
        style={style}
        aria-label={error ? 'Image failed to load' : 'No image available'}
        title={error ? 'Image failed to load' : 'No image available'}
      >
        <PictureOutlined style={{ fontSize: 124 }} />
      </div>
    );
  }

  return (
    <Image
      alt={alt}
      src={blobUrl}
      preview={preview}
      className={className}
      style={style}
      draggable={draggable}
      placeholder={loading ? 'loading' : undefined}
    />
  );
}

export default SecureImage;
