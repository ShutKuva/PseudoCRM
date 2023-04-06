import { useLoaderData, useParams } from "react-router-dom";
import styles from "./MessagePresenter.module.css";
import { Protocol } from "../../../../interfaces/Protocol";
import { EmailTextMessage } from "../../../../interfaces/EmailTextMessage";
import Message from "../../../../components/Message";

interface Props {}

type MessagePresenterProps = Props;

const MessagePresenter = (props: MessagePresenterProps) => {
  const params = useLoaderData() as EmailTextMessage[] | Error;

  return (
    <div className={styles.presenter}>
      {params instanceof Error ? (
        <div className={styles.error}></div>
      ) : (
        params.map((v, i) => <Message message={v} key={i} />)
      )}
    </div>
  );
};

export default MessagePresenter;
