import { useForm } from "react-hook-form";
import { RegisterArgs } from "../../interfaces/Register";
import Modal from "../Modal";
import { Auth } from "../../interfaces/Auth";
import { setTokens, useAuthDispatch } from "../../contexts/AuthContext";
import axios from "axios";
import { MAIN_API } from "../../consts/url";

interface Props {
  onClose: () => void;
}

type RegisterFormProps = Props;

const RegisterForm = (props: RegisterFormProps) => {
  const { register, handleSubmit } = useForm<RegisterArgs>();

  const dispatch = useAuthDispatch();

  const clickHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
    handleSubmit(async (data) => {
      const credentials = await axios.post<Auth>("JwtAuth/register", data, {
        baseURL: MAIN_API,
      });
      dispatch(setTokens({ ...credentials.data, login: data.login }));
      props.onClose();
    })();
  };

  return (
    <Modal onClose={props.onClose}>
      <form>
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
