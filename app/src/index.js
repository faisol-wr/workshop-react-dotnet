import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import reportWebVitals from "./reportWebVitals";
import App from "./App";
import MyLoop from "./Pages/MyLoop";
import MyIfElse from "./Pages/MyIfElse";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import CallApi from "./Pages/CallApi";

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
  },
  {
    path: "/myLoop",
    element: <MyLoop />,
  },
  {
    path: "/myIfElse",
    element: <MyIfElse />,
  },
  {
    path: "/callApi",
    element: <CallApi />,
  },
]);

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
