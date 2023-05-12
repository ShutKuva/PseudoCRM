import { useEffect, useState } from "react";
import styles from "./Chat.module.css";
import {
  HubConnection,
  HubConnectionBuilder,
  MessageHeaders,
} from "@microsoft/signalr";
import { MAIN_API, MAIN_API_WITHOUT_API } from "../../consts/url";
import { MessageDto } from "../../interfaces/MessageDto";
import axios from "axios";
import { useSelector } from "react-redux";
import { RootState } from "../../contexts/AuthContext";
import Message from "./MessageChat";

interface Props {}

type ChatProps = Props;

const Chat = (props: ChatProps) => {
  const auth = useSelector((state: RootState) => state.auth);
  const [message, setMessage] = useState<string>();
  const [messages, setMessages] = useState<MessageDto[]>([]);
  const [connection, setConnection] = useState<HubConnection>();

  useEffect(() => {
    axios
      .get<MessageDto[]>(`chat/messages`, {
        baseURL: MAIN_API,
        headers: { Authorization: `Bearer ${auth.token}` },
      })
      .then((response) => setMessages(response.data));

    const newConnection = new HubConnectionBuilder()
      .withUrl(`${MAIN_API_WITHOUT_API}/chat`, {
        headers: { Authorization: `Bearer ${auth.token}` },
      })
      .withAutomaticReconnect()
      .build();
    setConnection(newConnection);
  }, []);

  useEffect(() => {
    if (connection) {
      connection.start().then((result) => console.log("Connected..."));
      connection.on("NewMessage", (messages) => {
        setMessages(messages);
      });
    }
  }, [connection]);

  function clickHandler() {
    if (message?.trim()) {
      connection?.send("SendMessage", message);
    }
  }

  return (
    <div className={styles.chat}>
      <div className={styles.users}></div>
      <div className={styles["message-input"]}>
        <input
          type="text"
          value={message}
          onChange={(event) => setMessage(event.target.value)}
        />
        <button onClick={clickHandler}>Send</button>
      </div>
      <div className={styles.messages}>
        {messages.map((m, i) => {
          return <Message name={m.name} text={m.text} key={i} />;
        })}
      </div>
    </div>
  );
};

export default Chat;
