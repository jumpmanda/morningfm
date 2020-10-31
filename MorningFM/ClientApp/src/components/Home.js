import React, { Component } from 'react';
import { Container, Row, Col, Button, Form, FormGroup, Label, Input, Jumbotron, Alert } from 'reactstrap';
import rightImg from '../assets/radio.svg';
import { useAuth0 } from '@auth0/auth0-react';

function redirect(isAuthenticated, authenticateCallback) {
    var userSession = sessionStorage.getItem('mfmSession');
    if (userSession != undefined && userSession != null) {
        window.location = '/home?token=' + userSession;
        return;
    }
    if (!isAuthenticated) {
        authenticateCallback();         
    }    
}

function Home() {
   
    const {
        isLoading,
        isAuthenticated,
        error,
        user,
        loginWithRedirect,
        logout,
    } = useAuth0();

    let isButtonClicked = false;

    if (isLoading) {
        return <div>Loading...</div>;
    }
    if (error) {
        return <div>Oops... {error.message}</div>;
    }
   
    return <Container className="app-landing-page" fluid>
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
                                <div className={"mfm-button-neu " + (isButtonClicked ? "selected" : "")}
                                    onMouseDown={() => { isButtonClicked = true; }}
                                    onMouseUp={() => { isButtonClicked = false; }}
                                    onClick={() => {redirect(isAuthenticated, loginWithRedirect);}}>
                                    <h5>Get Started</h5>
                                </div>                                    
                            </Row>
                        </Container>
                    </Col>
                    <Col className="mfm-small-space">
                        <img className="mfm-landing-right-img" src={rightImg} />
                    </Col>
                </Row>
            </Container>                
        </Row>
    </Container>;
    
}

export default Home;
