import { makeAutoObservable } from 'mobx';

export class DummyAuthStore {
  accessToken: string | null = null;

  constructor() {
    makeAutoObservable(this);
  }

  get isLoggedIn() {
    return !!this.accessToken;
  }
}
