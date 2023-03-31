import { Outlet } from "react-router-dom";
import Tabs from "../../../components/Tabs";
import styles from "./MailSubPage.module.css";
import { useEffect, useState } from "react";
import { EmailPublicName } from "../../../interfaces/EmailPublicName";
import axios from "axios";
import { MAIN_API } from "../../../consts/url";
import { useSelector } from "react-redux";
import { RootState } from "../../../contexts/AuthContext";
import { TabItem } from "../../../interfaces/TabItem";

interface Props {}

type MailSubPageProps = Props;

const MailSubPage = (props: MailSubPageProps) => {
  const auth = useSelector((state: RootState) => state.auth);
  const [publicNames, setPublicNames] = useState<EmailPublicName[]>();

  useEffect(() => {
    axios
      .get<EmailPublicName[]>("Email/public-names", {
        baseURL: MAIN_API,
        headers: { Authorization: "Bearer " + auth.token },
      })
      .then((data) => setPublicNames(data.data));
  }, []);

  const tabItems =
    publicNames?.map<TabItem>((v) => {
      return {
        name: v.publicName,
      };
    }) ?? [];

  return (
    <>
      <Tabs items={tabItems}></Tabs>
      <Outlet></Outlet>
    </>
  );
};

export default MailSubPage;
