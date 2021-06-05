import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import './custom.css'
import { NavMenu } from 'shared/nav-menu/NavMenu';
import { MainPage } from 'pages/main/MainPage';
import { AboutPage } from 'pages/about/AboutPage'; 
import { PlaylistBuilderPage } from 'pages/playlist-builder/PlaylistBuilderPage';
import { AuthCallbackPage } from 'pages/auth-callback/AuthCallbackPage';
import { PlaylistPage } from 'pages/playlist/PlaylistPage';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <div style={{backgroundColor: '#E0E5EC'}}>
        <NavMenu></NavMenu>
        <Switch>
        <Route exact path='/' component={MainPage} />
        <Route exact path='/about' component={AboutPage} />
        <Route path='/build' component={PlaylistBuilderPage} />
        <Route path='/authenticate' component={AuthCallbackPage} />
        <Route path='/playlist' component={PlaylistPage} />
      </Switch>
      </div>      
    );
  }
}
