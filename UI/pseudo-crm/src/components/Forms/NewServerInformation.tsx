import { useForm } from "react-hook-form";
import styles from "./NewServerInformation.module.css";
import { ServerInformation } from "../../interfaces/ServerInformation";

interface Props {
  returnValue: (data: ServerInformation) => void;
  isReturn: boolean;
}

type NewServerInformationProps = Props;

const NewServerInformation = (props: NewServerInformationProps) => {
  const { register, handleSubmit } = useForm<ServerInformation>();

  if (props.isReturn) {
    handleSubmit((data) => {
      props.returnValue(data);
    })();
  }

  return (
    <>
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
    </>
  );
};

export default NewServerInformation;
