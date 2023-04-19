import { OAuthConfig } from "../interfaces/OAuth";

export const MAIN_API = "https://localhost:7119/api";

export const oauthConfigs: OAuthConfig[] = [
  {
    name: "figma",
    authorizeEnd: "https://www.figma.com/oauth",
    clientId: "qqRv4rgmJXKZt1EpQ5SfZG",
    clientSecret: "boOPiwTvInp2MFo37SwoCeq2kFKttM",
    responseType: "code",
    scope: "",
    tokenEnd: "https://www.figma.com/api/oauth/token",
  },
  {
    name: "google",
    clientId:
      "214863787317-dlan0n9tlkkgvseaf3tqriumnhgmiajm.apps.googleusercontent.com",
    clientSecret: "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",
    authorizeEnd: "https://accounts.google.com/o/oauth2/v2/aut",
    tokenEnd: "www.googleapis.com/oauth2/v4/token",
    responseType: "code",
    scope: "",
  },
];
