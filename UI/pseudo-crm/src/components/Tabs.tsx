import { useState } from "react";
import { NavLink } from "react-router-dom";
import { TabItem } from "../interfaces/TabItem";
import styles from "./Tabs.module.css";

interface Props {
  items: TabItem[];
}

type TabsProps = Props;

const Tabs = (props: TabsProps) => {
  const [activeIndex, setActiveIndex] = useState<number>();

  return (
    <div className={styles["public-names"]}>
      {props.items.map((v, i) => (
        <NavLink
          to={v.name}
          className={({ isActive }) => {
            isActive && setActiveIndex(i);
            return (
              styles["public-name"] +
              " " +
              ((isActive || activeIndex == i) && styles.active)
            );
          }}
          key={i}
        >
          {v.name}
        </NavLink>
      ))}
    </div>
  );
};

export default Tabs;

/*{({ isActive }) => {
    isActive && setActiveIndex(i);
    return (
      styles["public-name"] +
      " " +
      (isActive || (activeIndex == i && styles.active))
    );
  }}*/
