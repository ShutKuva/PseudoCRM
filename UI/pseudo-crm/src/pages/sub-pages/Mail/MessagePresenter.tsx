import styles from "./MessagePresenter.module.css";

interface Props {}

type MessagePresenterProps = Props;

const MessagePresenter = (props: MessagePresenterProps) => {
  return <div className={styles.presenter}></div>;
};

export default MessagePresenter;
