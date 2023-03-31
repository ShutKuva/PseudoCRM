import { useForm } from "react-hook-form";
import Button from "../Button";
import Modal from "../Modal";
import styles from "./LoginForm.module.css";
import { LoginArgs } from "../../interfaces/Login";

interface Props {
  onClose: () => void;
  onSubmit: (data: LoginArgs) => void;
}

type LoginFormProps = Props;

const LoginForm = (props: LoginFormProps) => {
  const { register, handleSubmit, formState } = useForm<LoginArgs>({
    defaultValues: { login: "", password: "" },
  });

  const clickHandler = () => {
    handleSubmit((data) => {
      props.onSubmit(data);
    })();
  };

  return (
    <Modal onClose={props.onClose}>
      <div style={{ display: "flex", flexDirection: "column" }}>
        <input {...register("login")} type="text" placeholder="Login" />
        <input {...register("password")} type="text" placeholder="Password" />
        <button onClick={clickHandler}>Login</button>
      </div>
    </Modal>
  );
};

export default LoginForm;
