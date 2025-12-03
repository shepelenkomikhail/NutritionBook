import SecureImage from '../../shared/SecureImage';
import { PictureOutlined } from '@ant-design/icons';
import { CSSProperties } from 'react';

function buildImageSrc(url?: string) {
  if (!url || url.trim() === '') return undefined;
  return url.trim();
}

export function RecipeImage({
  name,
  imageUrl,
  className,
  style,
}: {
  name?: string;
  imageUrl?: string;
  className?: string;
  style?: CSSProperties;
}) {
  const src = buildImageSrc(imageUrl);
  return src ? (
    <SecureImage
      alt={name || 'Recipe image'}
      src={src}
      preview={false}
      className={className}
      style={style}
    />
  ) : (
    <div
      className="h-56 w-full flex items-center justify-center bg-[var(--card)] text-[var(--fg-muted)] rounded-md"
      aria-label="No image available"
      title="No image available"
      style={style}
    >
      <PictureOutlined style={{ fontSize: 56 }} />
    </div>
  );
}

export default RecipeImage;
