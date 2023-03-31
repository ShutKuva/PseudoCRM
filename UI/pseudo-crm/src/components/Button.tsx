import styles from "./Button.module.css";

interface Props {
  name: string;
  onClick: () => void;
}

type ButtonProps = Props;

const Button = (props: ButtonProps) => {
  return (
    <button className={styles.button} onClick={props.onClick}>
      {props.name}
    </button>
  );
};

export default Button;
