import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import './custom.css'
import { MainPage } from 'pages/main/MainPage';
import { AboutPage } from 'pages/about/AboutPage'; 

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Switch>
        <Route exact path='/' component={MainPage} />
        <Route exact path='/about' component={AboutPage} />
      </Switch>
    );
  }
}
