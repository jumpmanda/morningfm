import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import './custom.css'
import { NavMenu } from 'pages/shared/nav-menu/NavMenu';
import { MainPage } from 'pages/main/MainPage';
import { AboutPage } from 'pages/about/AboutPage'; 
import { PlaylistBuilderPage } from 'pages/playlist-builder/PlaylistBuilderPage';
import ProtectedRoute from 'auth/ProtectedRoute';
import { AuthCallbackPage } from 'pages/auth-callback/AuthCallbackPage';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <div>
        <NavMenu></NavMenu>
        <Switch>
        <Route exact path='/' component={MainPage} />
        <Route exact path='/about' component={AboutPage} />
        <Route path='/playlist' component={PlaylistBuilderPage} />
        <Route path='/authenticate' component={AuthCallbackPage} />
      </Switch>
      </div>      
    );
  }
}
