import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import CreateUser from './CreateUser';
import Login from './Login';
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Register from './Register';
import AuthProvider from './AuthContext';
import RoleProtectedRoute from './RoleProtectedRoute';
import Homepage from './Homepage';
import UserDetails from './UserDetails';
import CreateEvent from './CreateEvent';


const router = createBrowserRouter(
  [
    {
      path: "/", element:
        <RoleProtectedRoute roles={['Admin', 'User']}>
          <Homepage />
        </RoleProtectedRoute>
    },
    {
      path: "/create", element:
        <RoleProtectedRoute roles={['Admin']}>
          <CreateUser />
        </RoleProtectedRoute>
    },
    { path: "/login", element: <Login /> },
    { path: "/register", element: <Register /> },
    { path: "/user/:id", element: <UserDetails /> },
    {
      path: "/create-event", element:
        <RoleProtectedRoute roles={['User']}>
          <CreateEvent />
        </RoleProtectedRoute>
    },
  ]
)

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <AuthProvider>
      <RouterProvider router={router} />
    </AuthProvider>
  </React.StrictMode>
);
