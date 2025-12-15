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
    const persisted = typeof window !== 'undefined' ? localStorage.getItem('theme') : null;

    if (persisted === 'light' || persisted === 'dark') return persisted;
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
    target.setAttribute('data-theme', current);
    try {
      localStorage.setItem('theme', current);
    } catch {
    }
    const vars = tokensToCssVars(current === 'dark' ? darkTokens : lightTokens);
    Object.entries(vars).forEach(([k, v]) => target.style.setProperty(k, v));
  }, [current]);

  const theme = useMemo(() => (current === 'dark' ? antdDarkTheme : antdLightTheme), [current]);

  return <ConfigProvider theme={theme}>{children}</ConfigProvider>;
}

export function useTheme() {
  const [mode, setMode] = useState<ThemeMode>(() => {
    const persisted = typeof window !== 'undefined' ? localStorage.getItem('theme') : null;
    if (persisted === 'light' || persisted === 'dark') return persisted;
    const attr = document.body.getAttribute('data-theme');
    if (attr === 'light' || attr === 'dark') return attr;
    return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
  });

  useEffect(() => {
    const observer = new MutationObserver(() => {
      const attr = document.body.getAttribute('data-theme');
      if (attr === 'light' || attr === 'dark') {
        setMode(attr);
      } else {
        setMode('light');
      }
    });
    observer.observe(document.body, { attributes: true, attributeFilter: ['data-theme'] });
    return () => observer.disconnect();
  }, []);

  const set = (m: ThemeMode) => {
    document.body.setAttribute('data-theme', m);
    try {
      localStorage.setItem('theme', m);
    } catch {
    }
  };
  return { mode, set };
}
