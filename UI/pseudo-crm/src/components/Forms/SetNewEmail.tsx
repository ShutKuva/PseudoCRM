import { useForm } from "react-hook-form";
import Modal from "../Modal";
import styles from "./SetNewEmail.module.css";
import { EmailCredentials } from "../../interfaces/EmailCredentials";
import { ServerProtocols } from "../../enums/ServerProtocols";
import { ServerInformation } from "../../interfaces/ServerInformation";
import { useState } from "react";
import axios from "axios";
import { RootState } from "../../contexts/AuthContext";
import { useSelector } from "react-redux";
import { MAIN_API } from "../../consts/url";

interface Props {
  onClose: () => void;
}

type SetNewEmailProps = Props;

const SetNewEmail = (props: SetNewEmailProps) => {
  const auth = useSelector((state: RootState) => state.auth);
  const { register, handleSubmit } = useForm<EmailCredentials>();

  function clickHandler(event: React.MouseEvent<HTMLButtonElement>) {
    event.preventDefault();
    handleSubmit((data) => {
      axios
        .post("Email/set-email", data, {
          baseURL: MAIN_API,
          headers: { Authorization: "Bearer " + auth.token },
        })
        .then(() => props.onClose());
    })();
  }

  return (
    <>
      <Modal onClose={props.onClose}>
        <form>
          <div style={{ display: "flex", flexDirection: "column" }}>
            <input
              {...register("publicName")}
              type="text"
              placeholder="Public name"
            />
            <input {...register("login")} type="text" placeholder="Login" />
            <input
              {...register("password")}
              type="password"
              placeholder="Password"
            />
            <button onClick={clickHandler}>Set new email</button>
          </div>
        </form>
      </Modal>
    </>
  );
};

export default SetNewEmail;
