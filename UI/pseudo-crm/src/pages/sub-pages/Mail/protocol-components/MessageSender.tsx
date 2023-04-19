import { createRef, useState } from "react";
import styles from "./MessageSender.module.css";
import { useForm } from "react-hook-form";
import { EmailTextMessage } from "../../../../interfaces/EmailTextMessage";
import axios from "axios";
import { useLoaderData, useNavigate, useParams } from "react-router-dom";
import { MAIN_API } from "../../../../consts/url";
import { RootState } from "../../../../contexts/AuthContext";
import { useSelector } from "react-redux";
import Modal from "../../../../components/Modal";
import NewServerInformation from "../../../../components/Forms/NewServerInformation";

interface Props {}

type MessageSenderProps = Props;

const MessageSender = (props: MessageSenderProps) => {
  const [
    isNewServerInformationFormVisible,
    setIsNewServerInformationFormVisible,
  ] = useState(false);

  const loaderParams = useLoaderData();
  const params = useParams();
  const navigate = useNavigate();
  const auth = useSelector((state: RootState) => state.auth);

  const { register, handleSubmit } = useForm<EmailTextMessage>();

  const [toEmails, setToEmails] = useState<string[]>([]);
  const [fromEmails, setFromEmails] = useState<string[]>([]);

  const toRef = createRef<HTMLInputElement>();
  const fromRef = createRef<HTMLInputElement>();

  function newReceiverHandler(who: "to" | "from") {
    return (event: React.MouseEvent<HTMLButtonElement>) => {
      event.preventDefault();
      switch (who) {
        case "from":
          if (fromRef.current && fromRef.current.value) {
            setFromEmails([...fromEmails, fromRef.current.value]);
            fromRef.current.value = "";
          }
          break;
        case "to":
          if (toRef.current && toRef.current.value) {
            setToEmails([...toEmails, toRef.current.value]);
            toRef.current.value = "";
          }
          break;
      }
    };
  }

  function submitHandler(event: React.MouseEvent<HTMLButtonElement>) {
    event.preventDefault();
    handleSubmit((data) => {
      data.from = fromEmails;
      data.to = toEmails;
      axios
        .post<EmailTextMessage>(`email/${params.publicName}/smtp/send`, data, {
          baseURL: MAIN_API,
          headers: { Authorization: `Bearer ${auth.token}` },
        })
        .then(() => {
          navigate("");
        });
    })();
  }

  return (
    <div>
      {isNewServerInformationFormVisible && (
        <Modal onClose={() => setIsNewServerInformationFormVisible(false)}>
          <NewServerInformation
            onSubmit={() => setIsNewServerInformationFormVisible(false)}
          />
        </Modal>
      )}
      {(loaderParams as boolean) ? (
        <form
          style={{
            display: "flex",
            flexDirection: "column",
            width: "50%",
            margin: "0 auto",
          }}
        >
          <input type="text" placeholder="Subject" {...register("subject")} />
          <input type="text" ref={toRef} />
          <button onClick={newReceiverHandler("to")}>Add new receiver</button>
          <input type="text" ref={fromRef} />
          <button onClick={newReceiverHandler("from")}>Add new sender</button>
          <input type="text" placeholder="Message" {...register("text")} />
          <button onClick={submitHandler}>Send</button>
        </form>
      ) : (
        <div>
          <button onClick={() => setIsNewServerInformationFormVisible(true)}>
            Add new server information
          </button>
        </div>
      )}
    </div>
  );
};

export default MessageSender;
