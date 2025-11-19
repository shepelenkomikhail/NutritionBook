import { PropsWithChildren, useEffect, useMemo, useState } from 'react';
import { ConfigProvider } from 'antd';
import { antdDarkTheme, antdLightTheme } from '../antdTheme';
import { darkTokens, lightTokens, tokensToCssVars, ThemeMode } from '../tokens';

type ThemeProviderProps = PropsWithChildren<{
  mode?: ThemeMode;
}>;

export default function ThemeProvider({ mode, children }: ThemeProviderProps) {
  const [current, setCurrent] = useState<ThemeMode>(() => {
    if (mode) return mode;
    const attr = document.body.getAttribute('data-theme');
    if (attr === 'light' || attr === 'dark') return attr;
    return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
  });

  useEffect(() => {
    if (mode) {
      setCurrent(mode);
      return;
    }
    const observer = new MutationObserver(() => {
      const attr = document.body.getAttribute('data-theme');
      if (attr === 'light' || attr === 'dark') setCurrent(attr);
    });
    observer.observe(document.body, { attributes: true, attributeFilter: ['data-theme'] });
    return () => observer.disconnect();
  }, [mode]);

  useEffect(() => {
    const target = document.body;
    const vars = tokensToCssVars(current === 'dark' ? darkTokens : lightTokens);
    Object.entries(vars).forEach(([k, v]) => target.style.setProperty(k, v));
  }, [current]);

  const theme = useMemo(() => (current === 'dark' ? antdDarkTheme : antdLightTheme), [current]);

  return <ConfigProvider theme={theme}>{children}</ConfigProvider>;
}

export function useTheme() {
  const get = () => (document.body.getAttribute('data-theme') === 'dark' ? 'dark' : 'light');
  const set = (m: ThemeMode) => document.body.setAttribute('data-theme', m);
  return { get, set };
}
