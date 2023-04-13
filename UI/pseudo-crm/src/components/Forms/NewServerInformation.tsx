import { useForm } from "react-hook-form";
import styles from "./NewServerInformation.module.css";
import { ServerInformation } from "../../interfaces/ServerInformation";
import { useParams } from "react-router-dom";
import axios from "axios";
import { MAIN_API } from "../../consts/url";
import { useSelector } from "react-redux";
import { RootState } from "../../contexts/AuthContext";

interface Props {
  onSubmit: () => void;
}

type NewServerInformationProps = Props;

const NewServerInformation = (props: NewServerInformationProps) => {
  const params = useParams();
  const auth = useSelector((state: RootState) => state.auth);
  const { register, handleSubmit } = useForm<ServerInformation>();

  function submitHandler() {
    handleSubmit((data) => {
      axios
        .post<ServerInformation>(`${params.publicName}/set-server-info`, data, {
          baseURL: MAIN_API,
          headers: { Authorization: `Bearer ${auth.token}` },
        })
        .then(() => props.onSubmit());
    });
  }

  return (
    <div style={{ display: "flex", flexDirection: "column" }}>
      <input {...register("server")} type="text" placeholder="Server" />
      <input {...register("port")} type="text" placeholder="Port" />
      <select {...register("serverProtocol")}>
        <option value="1">Pop</option>
        <option value="2">Imap</option>
        <option value="4">Smtp</option>
      </select>
      <select {...register("secureSocketOptions")}>
        <option value="0">None</option>
        <option value="1">Auto</option>
        <option value="2">SslOnConnect</option>
        <option value="3">StartTls</option>
        <option value="4">StartTlsWhenAvailable</option>
      </select>
      <button onClick={submitHandler}>Add</button>
    </div>
  );
};

export default NewServerInformation;
