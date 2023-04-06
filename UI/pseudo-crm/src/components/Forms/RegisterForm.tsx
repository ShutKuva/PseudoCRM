import { useForm } from "react-hook-form";
import styles from "./RegisterForm.module.css";
import { RegisterArgs } from "../../interfaces/Register";
import Modal from "../Modal";
import Button from "../Button";
import { ButtonHTMLAttributes } from "react";

interface Props {
  onClose: () => void;
  onSubmit: (data: RegisterArgs) => void;
}

type RegisterFormProps = Props;

const RegisterForm = (props: RegisterFormProps) => {
  const { register, handleSubmit } = useForm<RegisterArgs>();

  const clickHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
    handleSubmit((data) => {
      props.onSubmit(data);
    })();
  };

  return (
    <Modal onClose={props.onClose}>
      <form onSubmit={handleSubmit(props.onSubmit)}>
        <div style={{ display: "flex", flexDirection: "column" }}>
          <input {...register("login")} type="text" placeholder="Login" />
          <input {...register("email")} type="email" placeholder="Email" />
          <input
            {...register("passwordHash")}
            type="password"
            placeholder="Password"
          />
          <button onClick={clickHandler}>Register</button>
        </div>
      </form>
    </Modal>
  );
};

export default RegisterForm;
