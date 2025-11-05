import hotToast from 'react-hot-toast';

import { darkTheme } from '@themes';

export function toast(message: string, opts?: any) {
  const { token } = darkTheme;
  const baseOpts = {
    icon: '👀',
    position: 'top-right',
    style: {
      border: `1px solid ${token.colorPrimary}`,
      color: token.colorInfo,
      backgroundColor: token.colorBgContainer,
    },
  };

  return hotToast(message, { ...baseOpts, ...opts });
}
