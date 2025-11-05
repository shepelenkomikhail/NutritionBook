import { makeAutoObservable } from 'mobx';

import { agent } from '@api';
import { DummyModel } from '@models';

export class DummyStore {
  dummyDetails: DummyModel | null = null;

  constructor() {
    makeAutoObservable(this);
  }

  getDummyDetails = async (id: string) => {
    this.dummyDetails = await agent.Dummy.getDummyDetails(id);
  };
}
