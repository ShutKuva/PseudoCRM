import styles from "./Footer.module.css";

interface Props {}

type FooterProps = Props;

const Footer = (props: FooterProps) => {
  return (
    <footer className={styles.footer}>
      <h3 className={styles["footer-text"]}>All rights reserved</h3>
    </footer>
  );
};

export default Footer;
