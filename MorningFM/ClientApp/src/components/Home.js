import React, { Component } from 'react';
import { Container, Row, Col, Button, Form, FormGroup, Label, Input, Jumbotron, Alert } from 'reactstrap';
import rightImg from '../assets/radio.svg';
import { Redirect } from 'react-router-dom'

export class Home extends Component {
    static displayName = Home.name;   
    constructor(props){
        super(props);        
          this.state = {
              isGettingStarted: false,
              showForm: false,
              redirect: false
        }
        this.renderAuthenticationRedirect = this.renderAuthenticationRedirect.bind(this);
        this.setRedirect = this.setRedirect.bind(this);
    }  

    componentDidMount() {
        var userSession = sessionStorage.getItem('mfmSession');
        if (userSession != undefined && userSession != null) {
            window.location = '/home?token=' + userSession; 
        }
    }

    setRedirect() {
        this.setState({redirect: true});
    }

    renderAuthenticationRedirect() {
        if (this.state.redirect) {
            return <Redirect to='/account' />;
        }
    }

    render() {
    return (
        <Container className="app-landing-page" fluid>
            <Row>
                <Container fluid>
                    <Row>
                        <Col sm="6" className="mfm-large-space">
                            <Container>
                                <Row>
                                    <h1 className="display-3">Personalized radio to start your morning.</h1>
                                    <p className="lead">Connect with Spotify to queue up your daily podcast episodes, with recommended music in between.</p>
                                </Row>
                                <Row style={{ marginTop: '1rem' }}>
                                    <div className={"mfm-button-neu " + (this.state.isGettingStarted ? "selected" : "")}
                                        onMouseDown={() => { this.setState({ isGettingStarted: true }); }}
                                        onMouseUp={() => { this.setState({ isGettingStarted: false }); }}
                                        onClick={this.setRedirect}>
                                        <h5>Getting Started</h5>
                                    </div>
                                </Row>
                            </Container>
                        </Col>
                        <Col className="mfm-small-space">
                            <img className="mfm-landing-right-img" src={rightImg} />
                        </Col>
                    </Row>
                </Container>
                {this.renderAuthenticationRedirect()}
            </Row>
      </Container>
    );
  }
}
