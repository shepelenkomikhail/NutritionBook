import { useRegisterMutation, useLoginMutation } from '@api';
import type { RegisterModel, LoginFormModel } from '@models';
import { toast } from '@utils/toast.tsx';

export function useAuthMutation(mode: 'register' | 'login') {
  const [registerUser, registerUserState] = useRegisterMutation();
  const [loginUser, loginUserState] = useLoginMutation();

  const execute = async (payload: RegisterModel | LoginFormModel) => {
    try {
      let response;
        if(mode === 'register') {
          console.log('Registering user with payload:', payload);
          response = await registerUser(payload).unwrap();

          toast('Registration is successful!');
        } else if (mode === 'login') {
          console.log('Login user with payload:', payload);
          response = await loginUser(payload).unwrap();
        }

      if (response?.token) {
        localStorage.setItem('token', response.token);
        console.log('JWT stored:', response.token);
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