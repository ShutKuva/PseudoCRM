import Container from "../../components/Container";
import styles from "./CrmPresenter.module.css";
import LeftSide from "./CrmPresenterAdditional/LeftSide";
import RightSide from "./CrmPresenterAdditional/RightSide";

interface Props {}

type CrmPresenterProps = Props;

const CrmPresenter = (props: CrmPresenterProps) => {
  return (
    <div className={styles.wrapper}>
      <LeftSide />
      <RightSide />
    </div>
  );
};

export default CrmPresenter;
