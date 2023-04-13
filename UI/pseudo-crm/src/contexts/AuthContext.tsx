import { createContext } from "react";
import { Auth } from "../interfaces/Auth";
import { PayloadAction, configureStore, createSlice } from "@reduxjs/toolkit";
import { useDispatch } from "react-redux";

export const authState: Auth = {};

export const REFRESH_TOKEN = "refreshToken";
export const LOGIN = "login";
export const TOKEN = "token";

export const AuthSlice = createSlice({
  name: "auth",
  initialState: authState,
  reducers: {
    setTokens: (state, action: PayloadAction<Auth>) => {
      state.refreshToken = action.payload.refreshToken;
      state.login = action.payload.login;
      state.token = action.payload.token;
      localStorage.setItem(REFRESH_TOKEN, action.payload.refreshToken ?? "");
      localStorage.setItem(LOGIN, action.payload.login ?? "");
      localStorage.setItem(TOKEN, action.payload.token ?? "");
    },
  },
});

export const { setTokens } = AuthSlice.actions;
const reducer = AuthSlice.reducer;
export const AuthStore = configureStore({ reducer: { auth: reducer } });
export type AuthDispatch = typeof AuthStore.dispatch;
export type AuthDispatchFunc = () => AuthDispatch;
export const useAuthDispatch: AuthDispatchFunc = useDispatch;
export type RootState = ReturnType<typeof AuthStore.getState>;
export const AuthDispatch = AuthStore.dispatch;
