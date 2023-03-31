import { createPortal } from "react-dom";
import styles from "./Modal.module.css";
import { ReactNode } from "react";

interface Props {
  children: ReactNode;
  onClose: () => void;
}

type ModalProps = Props;

const ModalTemp = (props: ModalProps) => {
  return (
    <div className={styles["modal-back"]} onClick={props.onClose}>
      <div
        className={styles.modal}
        onClick={(event) => {
          event.preventDefault();
          event.stopPropagation();
        }}
      >
        {props.children}
      </div>
    </div>
  );
};

const modalContainer = document.getElementById("modal");

const Modal = (props: ModalProps) =>
  createPortal(ModalTemp(props), modalContainer!);

export default Modal;
