import MailIcon from "../../../components/Icons/MailIcon";
import Option from "../../../components/Option";
import styles from "./LeftSide.module.css";

interface Props {}

type LeftSideProps = Props;

const LeftSide = (props: LeftSideProps) => {
  return (
    <div className={styles["left-side"]}>
      <Option path="./mail" icon={<MailIcon />} />
    </div>
  );
};

export default LeftSide;
