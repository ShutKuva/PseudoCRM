import { Outlet } from "react-router-dom";
import Footer from "../components/Footer";
import Header from "../components/Header";
import styles from "./MainPage.module.css";

interface Props {}

type MainPageProps = Props;

const MainPage = (props: MainPageProps) => {
  return (
    <div className={styles.wrapper}>
      <Header></Header>
      <div className={styles["outlet-wrapper"]}>
        <Outlet />
      </div>
      <Footer></Footer>
    </div>
  );
};

export default MainPage;
