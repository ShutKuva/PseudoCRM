import styles from "./Message.module.css";

interface Props {
  name: string;
  text: string;
}

type MessageProps = Props;

const Message = (props: MessageProps) => {
  return (
    <div className={styles.message}>
      <h5 className={styles["message-name"]}>{props.name}</h5>
      <p>{props.text}</p>
    </div>
  );
};

export default Message;
