import { ReactNode } from "react";
import styles from "./Option.module.css";

interface Props {
  icon: ReactNode;
}

type OptionProps = Props;

const Option = (props: OptionProps) => {
  return <div className={styles.option}>{props.icon}</div>;
};

export default Option;
