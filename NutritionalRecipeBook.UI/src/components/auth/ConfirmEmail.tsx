import { useEffect } from 'react';
import { useSearchParams } from 'react-router-dom';
import { toast } from '@utils/toast.tsx';

function ConfirmEmail() {
  const [params] = useSearchParams();

  useEffect(() => {
    const userId = params.get('userId');
    const token = params.get('token');

    if (userId && token) {
      fetch(`https://localhost:3000/api/auth/confirm-email?userId=${userId}&token=${token}`)
        .then(res => res.ok ? toast('Email confirmed! You can now log in.')
          : toast('Failed to confirm email.'));
    }
  }, [params]);

  return <p>Confirming your email...</p>;
}

export default ConfirmEmail;