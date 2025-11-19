export type ThemeMode = 'light' | 'dark';

export type SemanticTokens = {
  brand: string;
  brandFg: string;

  fg: string;
  fgMuted: string;
  fgOnMuted: string;
  bg: string;
  bgMuted: string;
  card: string;
  border: string;

  success: string;
  warning: string;
  danger: string;
  info: string;

  ring: string;
  radius: number;
};

export const lightTokens: SemanticTokens = {
  brand: '#722ED1',
  brandFg: '#ffffff',

  fg: '#111827',
  fgMuted: '#4B5563',
  fgOnMuted: '#111827',
  bg: '#FFFFFF',
  bgMuted: '#F9FAFB',
  card: '#FFFFFF',
  border: '#E5E7EB',

  success: '#22C55E',
  warning: '#F59E0B',
  danger: '#EF4444',
  info: '#2563EB',

  ring: 'rgba(114, 46, 209, 0.4)',
  radius: 8,
};

export const darkTokens: SemanticTokens = {
  brand: '#722ED1',
  brandFg: '#ffffff',

  fg: '#E5E7EB',
  fgMuted: '#9CA3AF',
  fgOnMuted: '#E5E7EB',
  bg: '#0A0A0A',
  bgMuted: '#111827',
  card: '#181818',
  border: '#2D2D2D',

  success: '#22C55E',
  warning: '#F59E0B',
  danger: '#F87171',
  info: '#60A5FA',

  ring: 'rgba(114, 46, 209, 0.5)',
  radius: 8,
};

export function tokensToCssVars(tokens: SemanticTokens) {
  return {
    '--brand': tokens.brand,
    '--brand-fg': tokens.brandFg,
    '--fg': tokens.fg,
    '--fg-muted': tokens.fgMuted,
    '--fg-on-muted': tokens.fgOnMuted,
    '--bg': tokens.bg,
    '--bg-muted': tokens.bgMuted,
    '--card': tokens.card,
    '--border': tokens.border,
    '--success': tokens.success,
    '--warning': tokens.warning,
    '--danger': tokens.danger,
    '--info': tokens.info,
    '--ring': tokens.ring,
    '--radius': String(tokens.radius),
  } as const;
}
