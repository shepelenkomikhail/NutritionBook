import { createContext, useContext } from 'react';

import { DummyAuthStore } from '@stores/dummyAuthStore';
import { DummyStore } from '@stores/dummyStore';

interface RootStore {
  dummyStore: DummyStore;
  dummyAuthStore: DummyAuthStore;
}

export const rootStore: RootStore = {
  dummyStore: new DummyStore(),
  dummyAuthStore: new DummyAuthStore(),
};

export const StoreContext = createContext(rootStore);

export function useStore() {
  return useContext(StoreContext);
}
