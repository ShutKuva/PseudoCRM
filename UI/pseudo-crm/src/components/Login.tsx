import { useState } from "react";
import Button from "./Button";
import styles from "./Login.module.css";
import { useSelector } from "react-redux";
import { RootState, setTokens, useAuthDispatch } from "../contexts/AuthContext";
import LoginForm from "./Forms/LoginForm";
import RegisterForm from "./Forms/RegisterForm";
import SetNewEmail from "./Forms/SetNewEmail";

interface Props {}

type LoginProps = Props;

const Login = (props: LoginProps) => {
  const [isLoginModalVisible, setIsLoginModalVisible] = useState(false);
  const [isRegisterModalVisible, setIsRegisterModalVisible] = useState(false);
  const [isSetNewEmailModalVisible, setIsSetNewEmailModalVisible] =
    useState(false);
  const auth = useSelector((state: RootState) => state.auth);
  const dispatch = useAuthDispatch();

  return (
    <>
      {isLoginModalVisible && (
        <LoginForm onClose={() => setIsLoginModalVisible(false)} />
      )}

      {isRegisterModalVisible && (
        <RegisterForm onClose={() => setIsRegisterModalVisible(false)} />
      )}

      {isSetNewEmailModalVisible && (
        <SetNewEmail onClose={() => setIsSetNewEmailModalVisible(false)} />
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
              name="Set new email"
              onClick={() => {
                setIsSetNewEmailModalVisible(true);
              }}
            />
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
