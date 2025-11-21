import { theme as antdTheme } from 'antd';
import type { ThemeConfig } from 'antd';
import { darkTokens, lightTokens } from './tokens';

function toAntd(tokens: typeof lightTokens, algorithm: any): ThemeConfig {
  return {
    algorithm,
    token: {
      colorPrimary: tokens.brand,
      colorInfo: tokens.info,
      colorSuccess: tokens.success,
      colorWarning: tokens.warning,
      colorError: tokens.danger,
      colorTextBase: tokens.fg,
      colorText: tokens.fg,
      colorTextSecondary: tokens.fgMuted,
      colorBgBase: tokens.bg,
      colorBgContainer: tokens.card,
      colorBorder: tokens.border,
      borderRadius: tokens.radius,
      wireframe: true,
    },
  } satisfies ThemeConfig;
}

export const antdLightTheme: ThemeConfig = toAntd(lightTokens, antdTheme.defaultAlgorithm);
export const antdDarkTheme: ThemeConfig = toAntd(darkTokens, antdTheme.darkAlgorithm);
