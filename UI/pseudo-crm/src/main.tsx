import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import MainPage from "./pages/MainPage";
import CrmPresenter from "./pages/sub-pages/CrmPresenter";

const router = createBrowserRouter([
  {
    path: "",
    element: <MainPage />,
    children: [
      {
        path: "",
        element: <CrmPresenter />,
      },
    ],
  },
]);

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);
