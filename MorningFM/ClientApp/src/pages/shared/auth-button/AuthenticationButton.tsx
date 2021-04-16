import React from "react";

import { useAuth0 } from "@auth0/auth0-react";

const LogoutButton = () => {
    const { logout } = useAuth0();
    return (
        <button
        className="btn btn-link"
        onClick={() =>
            logout({
            returnTo: window.location.origin,
            })
        }
        >
        Log Out
        </button>
    );
}; 

const LoginButton = () => {
    const { loginWithRedirect } = useAuth0();
    return (
        <button
        className="btn btn-link"
        onClick={() => loginWithRedirect()}
        >
        Log In
        </button>
    );
}; 

const AuthenticationButton = () => {
  const { isAuthenticated } = useAuth0();

  return isAuthenticated ? <LogoutButton /> : <LoginButton />;
};

export default AuthenticationButton;