import { Link } from "react-router-dom";
import styles from "./MainSubPage.module.css";
import { useSelector } from "react-redux";
import { RootState } from "../../../contexts/AuthContext";
import Chat from "../../../components/Chat/Chat";

interface Props {}

type MainSubPageProps = Props;

const MainSubPage = (props: MainSubPageProps) => {
  const auth = useSelector((state: RootState) => state.auth);

  return (
    <div className={styles.main}>
      {auth.login && (
        <>
          <Link to="crm">To crm</Link>
          <Chat></Chat>
        </>
      )}
    </div>
  );
};

export default MainSubPage;
