import styles from "./MessagePresenterPlaceholder.module.css";

interface Props {}

type MessagePresenterPlaceholderProps = Props;

const MessagePresenterPlaceholder = (
  props: MessagePresenterPlaceholderProps
) => {
  return <div className={styles.presenter}></div>;
};

export default MessagePresenterPlaceholder;
