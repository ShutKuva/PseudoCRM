import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import MainPage from "./pages/MainPage";
import CrmPresenter from "./pages/sub-pages/CrmPresenter";
import MailSubPage from "./pages/sub-pages/Mail/MailSubPage";
import MainSubPage from "./pages/sub-pages/Main/MainSubPage";
import MessagePresenterPlaceholder from "./pages/sub-pages/Mail/MessagePresenterPlaceholder";
import { Provider } from "react-redux";
import { AuthStore, TOKEN } from "./contexts/AuthContext";
import axios from "axios";
import MailPresenter from "./pages/sub-pages/Mail/MailPresenter";
import { Protocol } from "./interfaces/Protocol";
import MessagePresenter from "./pages/sub-pages/Mail/protocol-components/MessagePresenter";
import { MAIN_API } from "./consts/url";
import { EmailTextMessage } from "./interfaces/EmailTextMessage";

const router = createBrowserRouter([
  {
    path: "",
    element: <MainPage />,
    children: [
      { path: "", element: <MainSubPage /> },
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
                        return await axios.get<EmailTextMessage[]>(
                          `Email/${publicName}/imap/messages`,
                          {
                            baseURL: MAIN_API,
                            headers: {
                              Authorization: `Bearer ${localStorage.getItem(
                                TOKEN
                              )}`,
                            },
                          }
                        );
                      } catch {}
                    },
                    element: <MessagePresenter />,
                  },
                  {
                    path: "pop",
                    loader: async ({ params: { publicName } }) => {
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
                    },
                    element: <MessagePresenter />,
                  },
                  {
                    path: "smtp",
                    loader: async ({ params: { publicName } }) => {
                      return { name: "smtp" };
                    },
                    element: <MessagePresenter />,
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