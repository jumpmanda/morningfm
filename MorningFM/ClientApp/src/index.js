import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import { Auth0Provider } from "@auth0/auth0-react";
import config from "./auth_config.json";

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');
const base = window.location.origin; 

ReactDOM.render(
    
    <Auth0Provider
        domain={config.domain}
        clientId={config.clientId}
        redirectUri={base + config.redirectUriPath}>
        <BrowserRouter basename={baseUrl}>
            <App />
        </BrowserRouter>
    </Auth0Provider>,
  rootElement);

registerServiceWorker();