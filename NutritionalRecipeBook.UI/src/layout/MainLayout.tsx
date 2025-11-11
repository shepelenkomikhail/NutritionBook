import { ReactNode } from 'react';
import { Layout } from 'antd';
import styled from 'styled-components';

interface Props {
  children?: ReactNode;
}

const StyledLayout = styled(Layout)`
  width: 100vw;
  height: 100vh;
`;

export const MainLayout = ({ children }: Props) => (
  <StyledLayout>{children}</StyledLayout>
);
