import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
//import registerServiceWorker from './registerServiceWorker';
import Auth0ProviderWithHistory from 'auth/Auth0ProviderWithHistory'; 

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');
const base = window.location.origin; 

ReactDOM.render(
  <BrowserRouter basename={baseUrl ?? ""}>
    <Auth0ProviderWithHistory>
      <App/>
    </Auth0ProviderWithHistory>
  </BrowserRouter>,
  rootElement);

//registerServiceWorker();

