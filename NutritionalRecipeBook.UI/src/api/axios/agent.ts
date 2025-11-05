import { DummyModel } from '@models';

import { requests } from './requests';

const Dummy = {
  getDummyDetails: (id: string) =>
    requests.get<DummyModel>(`/dummy/details/${id}`),
};

export const agent = {
  Dummy,
};
