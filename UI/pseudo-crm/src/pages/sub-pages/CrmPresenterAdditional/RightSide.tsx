import { Outlet } from "react-router-dom";
import styles from "./RightSide.module.css";

interface Props {}

type RightSideProps = Props;

const RightSide = (props: RightSideProps) => {
  return (
    <div className={styles["right-side"]}>
      <Outlet />
    </div>
  );
};

export default RightSide;
