import { observer } from 'mobx-react';
import { useEffect } from 'react';

import { useStore } from '@stores';
import { toast } from '@utils/toast';

import { Content } from 'antd/es/layout/layout';
import Title from 'antd/es/typography/Title';

function Home() {
  const { dummyStore } = useStore();
  const { getDummyDetails, dummyDetails } = dummyStore;

  const dummyId = '00000000-0000-0000-0000-000000000001';

  useEffect(
    () => {
      getDummyDetails(dummyId).then((/*value*/) => {
        toast('Got the data!');
      });
    } /*, []*/,
  );

  return (
    <Content>
      <Title>Well, this looks like home 🏡</Title>
      {dummyDetails ? (
        <Content>API is working! Data: ${dummyDetails.id}</Content>
      ) : null}
    </Content>
  );
}

const ObservedHome = observer(Home);
export default ObservedHome;
