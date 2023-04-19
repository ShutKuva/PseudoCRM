import { useForm } from "react-hook-form";
import Button from "../Button";
import Modal from "../Modal";
import styles from "./LoginForm.module.css";
import { LoginArgs } from "../../interfaces/Login";
import { MAIN_API } from "../../consts/url";
import { Link } from "react-router-dom";
import axios from "axios";
import { oauthAuthenticate } from "../../services/oauth/OAuthService";
import { Auth } from "../../interfaces/Auth";
import { useDispatch } from "react-redux";
import {
  setState,
  setTokens,
  useAuthDispatch,
} from "../../contexts/AuthContext";

interface Props {
  onClose: () => void;
}

type LoginFormProps = Props;

const LoginForm = (props: LoginFormProps) => {
  const { register, handleSubmit, formState } = useForm<LoginArgs>({
    defaultValues: { login: "", password: "" },
  });

  const dispatch = useAuthDispatch();

  const clickHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
    handleSubmit(async (data) => {
      const credentials = await axios.get<Auth>(
        `JwtAuth/login?Name=${data.login}&PasswordHash=${data.password}`,
        { baseURL: MAIN_API }
      );
      dispatch(setTokens({ ...credentials.data, login: data.login }));
      props.onClose();
    })();
  };

  const authHandler = () => {
    oauthAuthenticate("figma", (state) => dispatch(setState({ state })));
  };

  return (
    <Modal onClose={props.onClose}>
      <div style={{ display: "flex", flexDirection: "column" }}>
        <input {...register("login")} type="text" placeholder="Login" />
        <input
          {...register("password")}
          type="password"
          placeholder="Password"
        />
        <button onClick={authHandler}>Login with Figma</button>
        <button onClick={clickHandler}>Login</button>
      </div>
    </Modal>
  );
};

export default LoginForm;
