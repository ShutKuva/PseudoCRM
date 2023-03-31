import { ReactNode } from "react";
import styles from "./Container.module.css";

interface Props {
  children: ReactNode;
}

type ContainerProps = Props;

const Container = (props: ContainerProps) => {
  return <div className={styles.container}>{props.children}</div>;
};

export default Container;
