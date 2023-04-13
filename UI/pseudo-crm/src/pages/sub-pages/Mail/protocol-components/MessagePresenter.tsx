import { useLoaderData, useParams } from "react-router-dom";
import styles from "./MessagePresenter.module.css";
import { ServerError } from "../../../../interfaces/ServerError";
import { EmailTextMessage } from "../../../../interfaces/EmailTextMessage";
import Message from "../../../../components/Message";
import { useState } from "react";
import Modal from "../../../../components/Modal";
import NewServerInformation from "../../../../components/Forms/NewServerInformation";

interface Props {}

type MessagePresenterProps = Props;

const MessagePresenter = (props: MessagePresenterProps) => {
  const params = useLoaderData() as EmailTextMessage[] | ServerError;
  const [
    isNewServerInformationFormVisible,
    setIsNewServerInformationFormVisible,
  ] = useState(false);

  return (
    <div className={styles.presenter}>
      {isNewServerInformationFormVisible && (
        <Modal onClose={() => setIsNewServerInformationFormVisible(false)}>
          <NewServerInformation
            onSubmit={() => setIsNewServerInformationFormVisible(false)}
          />
        </Modal>
      )}
      {"message" in params ? (
        <div>
          <div className={styles.error}>{params.message}</div>
        </div>
      ) : (
        params.map((v, i) => <Message message={v} key={i} />)
      )}
      <button onClick={() => setIsNewServerInformationFormVisible(true)}>
        Add new server information
      </button>
    </div>
  );
};

export default MessagePresenter;
