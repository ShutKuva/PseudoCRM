import { useState } from "react";
import Button from "./Button";
import styles from "./Login.module.css";
import { useSelector } from "react-redux";
import { Auth } from "../interfaces/Auth";
import { RootState, setTokens, useAuthDispatch } from "../contexts/AuthContext";
import { useForm } from "react-hook-form";
import LoginForm from "./Forms/LoginForm";
import { LoginArgs } from "../interfaces/Login";
import axios from "axios";
import { MAIN_API } from "../consts/url";
import RegisterForm from "./Forms/RegisterForm";
import { RegisterArgs } from "../interfaces/Register";

interface Props {}

type LoginProps = Props;

const Login = (props: LoginProps) => {
  const [isLoginModalVisible, setIsLoginModalVisible] = useState(false);
  const [isRegisterModalVisible, setIsRegisterModalVisible] = useState(false);
  const auth = useSelector((state: RootState) => state.auth);
  const dispatch = useAuthDispatch();
  const { register } = useForm();

  async function handleLogin(data: LoginArgs) {
    const credentials = await axios.get<Auth>(
      `JwtAuth/login?Name=${data.login}&PasswordHash=${data.password}`,
      { baseURL: MAIN_API }
    );
    dispatch(setTokens({ ...credentials.data, login: data.login }));
    setIsLoginModalVisible(false);
  }

  async function handleRegister(data: RegisterArgs) {
    const credentials = await axios.post<Auth>("JwtAuth/register", data, {
      baseURL: MAIN_API,
    });
    dispatch(setTokens({ ...credentials.data, login: data.login }));
    setIsRegisterModalVisible(false);
  }

  return (
    <>
      {isLoginModalVisible && (
        <LoginForm
          onClose={() => setIsLoginModalVisible(false)}
          onSubmit={handleLogin}
        />
      )}

      {isRegisterModalVisible && (
        <RegisterForm
          onClose={() => setIsRegisterModalVisible(false)}
          onSubmit={handleRegister}
        />
      )}

      <div className={styles.login}>
        {auth.login == undefined ? (
          <>
            <Button
              name="Login"
              onClick={() => {
                setIsLoginModalVisible(true);
              }}
            />
            <Button
              name="Register"
              onClick={() => {
                setIsRegisterModalVisible(true);
              }}
            />
          </>
        ) : (
          <>
            <h4>{auth.login}</h4>
            <Button
              name="Logout"
              onClick={() => {
                dispatch(setTokens({}));
              }}
            />
          </>
        )}
      </div>
    </>
  );
};

export default Login;
