import React, { Component } from 'react';
import { Container, Row, Col, Button, Form, FormGroup, Label, Input, Jumbotron, Alert } from 'reactstrap';
import UserAuthForm from './UserAuthForm';

export class Home extends Component {
    static displayName = Home.name;   
      constructor(){
          super();         
    }  

    componentDidMount() {
        var userSession = sessionStorage.getItem('mfmSession');
        if (userSession != undefined && userSession != null) {
            window.location = '/home?token=' + userSession; 
        }
    }

  render () {
    return (
      <Container>
         <Jumbotron fluid className="app-home-jumbotron">
          <Container fluid>
            <h1 className="display-3">Personalized radio to start your morning.</h1>
            <p className="lead">Connect with Spotify to queue up your daily podcast episodes, with recommended music in between.</p>
          </Container>
            </Jumbotron>
            <UserAuthForm></UserAuthForm>
      </Container>
    );
  }
}
