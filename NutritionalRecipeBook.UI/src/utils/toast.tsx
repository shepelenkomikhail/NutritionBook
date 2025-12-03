import hotToast, { type ToastOptions } from 'react-hot-toast';

export function toast(message: string, opts?: ToastOptions) {
  const styles = getComputedStyle(document.body);
  const colorPrimary = styles.getPropertyValue('--brand').trim() || '#722ED1';
  const colorInfo = styles.getPropertyValue('--fg').trim() || '#ffffff';
  const colorBg = styles.getPropertyValue('--card').trim() || '#181818';

  const baseOpts: ToastOptions = {
    icon: '👀',
    position: 'top-right' as const,
    style: {
      border: `1px solid ${colorPrimary}`,
      color: colorInfo,
      backgroundColor: colorBg,
    },
  };

  return hotToast(message, { ...baseOpts, ...opts });
}
