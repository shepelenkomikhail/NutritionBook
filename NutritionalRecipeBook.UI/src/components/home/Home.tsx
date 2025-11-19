import { Content, Header } from 'antd/es/layout/layout';
import Title from 'antd/es/typography/Title';
import { Button, Typography } from 'antd';
import { ThemeToggleButton } from '../shared';
import { useNavigate } from 'react-router-dom';

const { Paragraph } = Typography;

function Home() {
  const navigate = useNavigate();

  const handleRegisterClick = () => navigate('/register');
  const handleLoginClick = () => navigate('/login');

  return (
    <>
      <Header className="w-full !bg-[var(--bg)] !text-[var(--fg)] border-b border-[var(--border)]">
        <div className="max-w-7xl mx-auto px-4 h-16 grid grid-cols-3 items-center">
          <div className="flex items-center gap-3">
            <ThemeToggleButton variant="inline" />
          </div>
          <div className="flex items-center justify-center">
            <Title level={3} className="!mb-0 !text-[var(--fg)]">Nutritional Recipe Book</Title>
          </div>
          <div className="flex items-center justify-end gap-3">
            <Button onClick={handleLoginClick}>Login</Button>
            <Button type="primary" onClick={handleRegisterClick}>Register</Button>
          </div>
        </div>
      </Header>

      <Content className="bg-[var(--bg)] text-[var(--fg)]">
        <section className="max-w-7xl mx-auto px-4 py-16 grid grid-cols-1 lg:grid-cols-2 gap-10 items-center">
          <div>
            <span className="inline-block text-xs font-semibold tracking-wider uppercase px-2 py-1 rounded ds-card mb-4">Healthy • Simple • Smart</span>
            <Title className="!text-[var(--fg)]" style={{ marginBottom: 12 }}>
              Eat better with smart, token‑themed recipes
            </Title>
            <Paragraph className="text-lg !text-[var(--fg-muted)]" style={{ marginBottom: 24 }}>
              Discover, create, and manage recipes tailored to your goals. Filter by cooking time, servings, and more. Works beautifully in light and dark modes.
            </Paragraph>
            <div className="flex flex-col sm:flex-row gap-3">
              <Button size="large" type="primary" onClick={handleRegisterClick} className="sm:!w-auto !w-full">Get started</Button>
              <Button size="large" onClick={handleLoginClick} className="sm:!w-auto !w-full">I already have an account</Button>
            </div>
          </div>

          <div className="relative">
            <div className="ds-card p-6 rounded-2xl shadow-md">
              <div className="grid grid-cols-2 gap-4">
                <div className="rounded-xl ds-muted p-4">
                  <div className="text-3xl mb-2">🍲</div>
                  <div className="font-semibold">Curated recipes</div>
                  <div className="text-[var(--fg-muted)] text-sm">Save and organize your favorites</div>
                </div>
                <div className="rounded-xl ds-muted p-4">
                  <div className="text-3xl mb-2">⏱️</div>
                  <div className="font-semibold">Quick filters</div>
                  <div className="text-[var(--fg-muted)] text-sm">Time and servings at a glance</div>
                </div>
                <div className="rounded-xl ds-muted p-4">
                  <div className="text-3xl mb-2">🌗</div>
                  <div className="font-semibold">Themed UI</div>
                  <div className="text-[var(--fg-muted)] text-sm">Auto light/dark with tokens</div>
                </div>
                <div className="rounded-xl ds-muted p-4">
                  <div className="text-3xl mb-2">🔎</div>
                  <div className="font-semibold">Smart search</div>
                  <div className="text-[var(--fg-muted)] text-sm">Find exactly what you need</div>
                </div>
              </div>
            </div>
          </div>
        </section>

        <section className="max-w-7xl mx-auto px-4 pb-20">
          <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
            <div className="ds-card p-6 rounded-xl">
              <div className="text-xl font-semibold mb-2">Why this app?</div>
              <p className="text-[var(--fg-muted)]">Plan meals, reduce waste, and keep nutrition on track with a clean, accessible interface.</p>
            </div>
            <div className="ds-card p-6 rounded-xl">
              <div className="text-xl font-semibold mb-2">Privacy first</div>
              <p className="text-[var(--fg-muted)]">Your data stays yours. We only store what’s necessary for your account.</p>
            </div>
            <div className="ds-card p-6 rounded-xl">
              <div className="text-xl font-semibold mb-2">Powered by tokens</div>
              <p className="text-[var(--fg-muted)]">Consistent design via semantic tokens across Ant Design and Tailwind.</p>
            </div>
          </div>
        </section>
      </Content>
    </>
  );
}

export default Home;
