import { Auth } from "../interfaces/Auth";
import { PayloadAction, configureStore, createSlice } from "@reduxjs/toolkit";
import { useDispatch } from "react-redux";
import { OAuthState } from "../interfaces/OAuth";

export const authState: Auth = {};
export const oauthState: OAuthState = {};

export const REFRESH_TOKEN = "refreshToken";
export const LOGIN = "login";
export const TOKEN = "token";
export const ID = "id";

export const AuthSlice = createSlice({
  name: "auth",
  initialState: authState,
  reducers: {
    setTokens: (state, action: PayloadAction<Auth>) => {
      state.refreshToken = action.payload.refreshToken;
      state.login = action.payload.login;
      state.token = action.payload.token;
      state.id = action.payload.id;
      localStorage.setItem(REFRESH_TOKEN, action.payload.refreshToken ?? "");
      localStorage.setItem(LOGIN, action.payload.login ?? "");
      localStorage.setItem(ID, action.payload.id ?? "");
      localStorage.setItem(TOKEN, action.payload.token ?? "");
    },
  },
});

export const OAuthSlice = createSlice({
  name: "oauth",
  initialState: oauthState,
  reducers: {
    setState: (state, action: PayloadAction<OAuthState>) => {
      state.state = action.payload.state ?? state.state;
      state.code = action.payload.code ?? state.code;
    },
  },
});

export const { setTokens } = AuthSlice.actions;
const reducer = AuthSlice.reducer;
export const { setState } = OAuthSlice.actions;
const oreducer = OAuthSlice.reducer;
export const AuthStore = configureStore({
  reducer: { auth: reducer, oauth: oreducer },
});
export type AuthDispatch = typeof AuthStore.dispatch;
export type AuthDispatchFunc = () => AuthDispatch;
export const useAuthDispatch: AuthDispatchFunc = useDispatch;
export type RootState = ReturnType<typeof AuthStore.getState>;
export const AuthDispatch = AuthStore.dispatch;
