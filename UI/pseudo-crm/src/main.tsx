import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import MainPage from "./pages/MainPage";
import CrmPresenter from "./pages/sub-pages/CrmPresenter";
import MailSubPage from "./pages/sub-pages/Mail/MailSubPage";
import MainSubPage from "./pages/sub-pages/Main/MainSubPage";
import MessagePresenter from "./pages/sub-pages/Mail/MessagePresenter";
import MessagePresenterPlaceholder from "./pages/sub-pages/Mail/MessagePresenterPlaceholder";
import { Provider } from "react-redux";
import { AuthStore } from "./contexts/AuthContext";
import axios from "axios";

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
                element: <MessagePresenter />,
                // loader: {
                //   async ({ params }) {
                //     return await axios.get();
                //   },
                // },
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
