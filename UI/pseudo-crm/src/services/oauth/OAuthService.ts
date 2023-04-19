import axios from "axios";
import { oauthConfigs } from "../../consts/url";

const lengthOfState = 64;
const alphabet = "#ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

const countState = () => {
  let result: string[] = [];
  for (let i = 0; i < lengthOfState; i++) {
    result.push(alphabet[Math.floor(Math.random() * lengthOfState) - 1]);
  }
  return result.join("");
};

export const oauthAuthenticate = (
  service: string,
  save: (state: string) => void
) => {
  const config = oauthConfigs.find((x) => x.name === service);
  const state = countState();
  save(state);
  console.log(window.location.href);
  if (config) {
    axios.get(
      `${config.authorizeEnd}?client_id=${config.clientId}&redirect_uri=${window.location.href}auth/${service}&state=${state}&response_type=${config.responseType}&scope=${config.scope}`
    );
  }
};
