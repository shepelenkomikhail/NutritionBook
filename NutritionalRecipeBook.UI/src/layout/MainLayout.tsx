import { ReactNode } from 'react';

import { Layout } from 'antd';
import styled from 'styled-components';

interface Props {
  children?: ReactNode;
}

const StyledLayout = styled(Layout)`
  margin: -8px;
  padding-top: 1rem;
  padding-left: 2rem;
  padding-right: 2rem;
  width: 100vw;
  height: 100vh;
`;

export const MainLayout = ({ children }: Props) => (
  <StyledLayout>{children}</StyledLayout>
);
