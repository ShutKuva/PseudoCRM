import { ReactNode } from "react";
import { Link } from "react-router-dom";
import styles from "./Option.module.css";

interface Props {
  icon: ReactNode;
  path: string;
}

type OptionProps = Props;

const Option = (props: OptionProps) => {
  return (
    <Link className={styles.option} to={props.path}>
      {props.icon}
    </Link>
  );
};

export default Option;
