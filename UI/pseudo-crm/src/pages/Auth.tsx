import { useEffect } from "react";
import styles from "./Auth.module.css";
import { useLoaderData, useNavigate, useParams } from "react-router-dom";
import axios from "axios";
import { OAuthTokenRequest } from "../interfaces/OAuth";

interface Props {}

type AuthProps = Props;

interface Query {
  name: string;
  value: string;
}

const getQueries = (url: string) => {
  const urlArr = url.split("?");
  if (urlArr.length == 1) {
    return [];
  }

  const rawQueries = urlArr[1].split("&");
  const result: Query[] = [];
  for (const query of rawQueries) {
    const temp = query.split("=");
    result.push({ name: temp[0], value: temp[1] });
  }
  return result;
};

const Auth = (props: AuthProps) => {
  const params = useLoaderData() as string;
  const routeParams = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    //axios.post<OAuthTokenRequest>();
    navigate(-1);
  }, [params]);

  return <div>Тут відбувається магія...</div>;
};

export default Auth;
