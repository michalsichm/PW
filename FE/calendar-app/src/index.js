import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
// import App from './App';
import CreateUser from './CreateUser';
import Users from './Users';
import { createBrowserRouter, RouterProvider } from "react-router-dom";


const router = createBrowserRouter(
  [
    { path: "/", element: <Users /> },
    { path: "/create", element: <CreateUser /> }
  ]
)

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);
