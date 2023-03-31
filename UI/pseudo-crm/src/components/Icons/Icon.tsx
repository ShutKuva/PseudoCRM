import { ReactNode } from "react";
import styles from "./Icon.module.css";

interface Props {
  children: ReactNode;
}

type IconProps = Props;

const Icon = (props: IconProps) => {
  return <div className={styles.icon}>{props.children}</div>;
};

export default Icon;
