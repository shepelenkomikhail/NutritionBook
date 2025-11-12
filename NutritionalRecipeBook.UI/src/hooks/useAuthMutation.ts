import { useRegisterMutation } from '@api';
import type { RegisterModel, } from '@models';
import { toast } from '@utils/toast.tsx';

export function useAuthMutation() {
  const [registerUser, registerUserState] = useRegisterMutation();

  const execute = async (payload: RegisterModel) => {
    try {
        console.log('Registering user with payload:', payload);
        const response = await registerUser(payload).unwrap();

        if (response.token) {
          localStorage.setItem('token', response.token);
          console.log('JWT stored:', response.token);
        }
        toast('Registration is successful!');
    } catch (error) {
      console.error(`Failed to register user:`, error);
      toast(`Failed to register user`);
    }
  };

  const isLoading = registerUserState.isLoading || registerUserState.isLoading;
  const isError = registerUserState.isError || registerUserState.isError;

  return { execute, isLoading, isError };
}