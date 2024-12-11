import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
// import App from './App';
import CreateUser from './CreateUser';
import Users from './Users';
import Login from './Login';
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Register from './Register';


const router = createBrowserRouter(
  [
    { path: "/", element: <Users /> },
    { path: "/create", element: <CreateUser /> },
    { path: "/login", element: <Login /> },
    { path: "/register", element: <Register /> }
  ]
)

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);
