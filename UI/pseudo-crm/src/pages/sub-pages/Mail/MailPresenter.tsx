import { Link, Outlet } from "react-router-dom";
import styles from "./MailPresenter.module.css";

interface Props {}

type MailPresenterProps = Props;

const MailPresenter = (props: MailPresenterProps) => {
  return (
    <div className={styles.presenter}>
      <div className={styles.protocols}>
        <Link to="imap" className={styles.protocol}>
          <h4>IMAP</h4>
        </Link>
        <Link to="pop" className={styles.protocol}>
          <h4>POP</h4>
        </Link>
        <Link to="smtp" className={styles.protocol}>
          <h4>SMTP</h4>
        </Link>
      </div>
      <Outlet />
    </div>
  );
};

export default MailPresenter;
