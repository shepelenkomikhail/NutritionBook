import { useRegisterMutation, useLoginMutation } from '@api';
import type { RegisterModel, LoginFormModel } from '@models';
import { toast } from '@utils/toast.tsx';
import { useNavigate } from 'react-router-dom';

export function useAuthMutation(mode: 'register' | 'login') {
  const [registerUser, registerUserState] = useRegisterMutation();
  const [loginUser, loginUserState] = useLoginMutation();

  const navigate = useNavigate();

  const execute = async (payload: RegisterModel | LoginFormModel) => {
    try {
        if(mode === 'register') {
          // @ts-ignore
          const res = await registerUser(payload).unwrap();
          if(res.token) {
            localStorage.setItem('token', res.token);

            toast('Please, confirm your email!');
            navigate('/login');
          }
        } else if (mode === 'login') {
          const res = await loginUser(payload).unwrap();

          if(res.token) {
            localStorage.setItem('token', res.token);
            navigate('/recipes');
          }
        }

      toast(`${mode} is successful!`);
    } catch (error) {
      console.error(`Failed to ${mode} user:`, error);
      toast(`Failed to ${mode} user`);
    }
  };

  const isLoading = registerUserState.isLoading || loginUserState.isLoading;
  const isError = registerUserState.isError || loginUserState.isError;

  return { execute, isLoading, isError };
}