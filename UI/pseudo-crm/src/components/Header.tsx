import Container from "./Container";
import styles from "./Header.module.css";
import Login from "./Login";
import Logo from "./Logo";

interface Props {}

type HeaderProps = Props;

const Header = (props: HeaderProps) => {
  return (
    <header className={styles.header}>
      <Container>
        <div className={styles["vertical-wrapper"]}>
          <div className={styles.wrapper}>
            <Logo />
            <Login />
          </div>
        </div>
      </Container>
    </header>
  );
};

export default Header;
