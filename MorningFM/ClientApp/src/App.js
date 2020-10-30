import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { PlaylistCreate } from './components/PlaylistCreate';

import './custom.css'
import UserAuthentication from './components/UserAuthentication';

export default class App extends Component {
    static displayName = App.name;

      render () {
          return (
            <Layout>
                <Route exact path='/' component={Home} />
                  <Route path='/home' component={PlaylistCreate} />
                  <Route path='/account' component={UserAuthentication} />
            </Layout>    
        );
      }
}
