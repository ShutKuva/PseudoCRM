import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import {
  createBrowserRouter,
  createHashRouter,
  RouterProvider,
} from "react-router-dom";
import MainPage from "./pages/MainPage";
import CrmPresenter from "./pages/sub-pages/CrmPresenter";
import MailSubPage from "./pages/sub-pages/Mail/MailSubPage";
import MainSubPage from "./pages/sub-pages/Main/MainSubPage";
import MessagePresenterPlaceholder from "./pages/sub-pages/Mail/MessagePresenterPlaceholder";
import { Provider } from "react-redux";
import { AuthStore, TOKEN } from "./contexts/AuthContext";
import axios, { AxiosError } from "axios";
import MailPresenter from "./pages/sub-pages/Mail/MailPresenter";
import MessagePresenter from "./pages/sub-pages/Mail/protocol-components/MessagePresenter";
import { MAIN_API } from "./consts/url";
import { EmailTextMessage } from "./interfaces/EmailTextMessage";
import MessageSender from "./pages/sub-pages/Mail/protocol-components/MessageSender";

const router = createBrowserRouter([
  {
    path: "",
    element: <MainPage />,
    children: [
      { path: "", element: <MainSubPage /> },
      {
        path: "auth",
        children: [
          {
            path: ":service",
            loader: ({ request }) => {
              return request.url;
            },
          },
        ],
      },
      {
        path: "crm",
        element: <CrmPresenter />,
        children: [
          {
            path: "mail",
            element: <MailSubPage />,
            children: [
              {
                path: ":publicName",
                element: <MailPresenter />,
                children: [
                  {
                    path: "imap",
                    loader: async ({ params: { publicName } }) => {
                      try {
                        return await axios
                          .get<EmailTextMessage[]>(
                            `Email/${publicName}/imap/messages`,
                            {
                              baseURL: MAIN_API,
                              headers: {
                                Authorization: `Bearer ${localStorage.getItem(
                                  TOKEN
                                )}`,
                              },
                            }
                          )
                          .then((response) => response.data);
                      } catch (e) {
                        return { message: (e as AxiosError).cause?.message };
                      }
                    },
                    element: <MessagePresenter />,
                  },
                  {
                    path: "pop",
                    loader: async ({ params: { publicName } }) => {
                      try {
                        return await axios.get<EmailTextMessage[]>(
                          `Email/${publicName}/pop/messages/4`,
                          {
                            baseURL: MAIN_API,
                            headers: {
                              Authorization: `Bearer ${localStorage.getItem(
                                TOKEN
                              )}`,
                            },
                          }
                        );
                      } catch (e) {
                        return { message: (e as AxiosError).cause?.message };
                      }
                    },
                    element: <MessagePresenter />,
                  },
                  {
                    path: "smtp",
                    loader: async ({ params: { publicName } }) => {
                      return await axios
                        .get(`email/${publicName}/smtp/check`, {
                          baseURL: MAIN_API,
                          headers: {
                            Authorization: `Bearer ${localStorage.getItem(
                              TOKEN
                            )}`,
                          },
                        })
                        .then((response) => response.data);
                    },
                    element: <MessageSender />,
                  },
                ],
              },
              {
                path: "",
                element: <MessagePresenterPlaceholder />,
              },
            ],
          },
        ],
      },
    ],
  },
]);

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <React.StrictMode>
    <Provider store={AuthStore}>
      <RouterProvider router={router} />
    </Provider>
  </React.StrictMode>
);
