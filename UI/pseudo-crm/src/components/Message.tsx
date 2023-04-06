import { EmailTextMessage } from "../interfaces/EmailTextMessage";
import styles from "./Message.module.css";

interface Props {
  message: EmailTextMessage;
}

type MessageProps = Props;

const Message = (props: MessageProps) => {
  return (
    <div className={styles.message}>
      <h1>{props.message.subject ?? "(Without subject)"}</h1>
      <h4>From</h4>
      <div className={styles.people}>
        {props.message.from.map((v) => {
          return <h5 className={styles.human}>{v}</h5>;
        })}
      </div>
      <h4>To</h4>
      <div className={styles.people}>
        {props.message.to.map((v) => {
          return <h5 className={styles.human}>{v}</h5>;
        })}
      </div>
      <h4></h4>
    </div>
  );
};

export default Message;
