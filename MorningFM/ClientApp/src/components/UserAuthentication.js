import React, { Component } from 'react';
import { Container, Row, Col, Button, Form, FormGroup, Label, Input, Jumbotron, Alert, Nav, NavItem, NavLink } from 'reactstrap';
import classnames from 'classnames';

export default class UserAuthentication extends React.Component {
    static displayName = UserAuthentication.name;
    constructor(props) {
        super(props);
        this.state = {
            isError: false,
            errorMessage: '',
            submitAction: 'LOGIN',
            userEmail: '',
            userPassword: '',
            activeTab: '1'
        };

        this.onInputchange = this.onInputchange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    onInputchange(event) {
        this.setState({
            [event.target.name]: event.target.value
        });
    }

    handleSubmit(event) {
        event.preventDefault();
        this.authenticateUser(this.state.submitAction, this.state.userEmail, this.state.userPassword);
    }

    authenticateUser(action, email, password) {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ email: email, password: password, action: action == 'LOGIN' ? 0 : 1 })
        };
        fetch("api/authentication", requestOptions)
            .then(res => {
                res.json().then(json => {
                    console.log(json);
                    window.location = json.redirectUrl;
                }
                );
            },
                err => {
                    this.setState({ isError: true, errorMessage: err });
                    console.log(err);
                });
    }
    
    closeForm() {
        this.props.onFormClose(false);
    }

    toggleTab(tab) {
        this.setState({ activeTab: tab, submitAction: tab == '1' ? 'LOGIN': 'SIGNUP' });
    }

    render() {
        const showForm = this.props.showForm;
        const isError = this.state.isError;
        let form;
        let errorbanner;
        if (showForm) {
            form =
                <div className="mfm-login-signup-form">               
                    <Nav tabs>
                        <NavItem>
                            <NavLink className={classnames({ active: this.state.activeTab === '1' })} onClick={() => this.toggleTab('1')}>
                                Login
                        </NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink className={classnames({ active: this.state.activeTab === '2' })} onClick={()=> this.toggleTab('2')}>
                                Signup
                          </NavLink>
                        </NavItem>
                    </Nav>
                    <Form onSubmit={this.handleSubmit}>
                        <FormGroup>
                            <Label for="exampleEmail">Email</Label>
                            <Input type="email" name="userEmail" id="exampleEmail" placeholder="user123@email.com" onChange={this.onInputchange} />
                        </FormGroup>
                        <FormGroup>
                            <Label for="examplePassword">Password</Label>
                            <Input type="password" name="userPassword" id="examplePassword" placeholder="password123" onChange={this.onInputchange} />
                        </FormGroup>
                        <Button color="primary">Submit</Button>                   
                    </Form>
                    <Button color="link" onClick={() => this.closeForm()}>Cancel</Button>
                </div>;
        }
        if (isError) {
            errorbanner = <Alert color="warning">{this.state.errorMessage}</Alert>;
        }
        return (
            <Container>
                <div className="mfm-login-signup-form-container">
                    <div className="mfm-veil" style={{ display: this.state.showForm ? 'block': 'none'}}></div>
                    <Row>
                        {errorbanner}
                        {form}
                    </Row>
                </div>
            </Container>
        );
    }
}