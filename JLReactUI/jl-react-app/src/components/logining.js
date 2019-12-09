import React, { Component } from "react";
import { Button, FormGroup, FormControl, Form } from "react-bootstrap";


export class Login extends Component {

    constructor() {
        super();
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    

    handleSubmit(event) {
        const data = new FormData(event.target);
        event.preventDefault();
        fetch('https://localhost:44327​/api​/Auth​/jwtToken', {
            method: 'POST',
            body: data,
          });
    }

    render() {
        return (
            <div>
                <Form onSubmit={this.handleSubmit} className="login-form">
                    <FormGroup controlId="login">
                        <Form.Label>Email</Form.Label>
                        <FormControl
                           type="text"
                           name="username"
                           required
                           placeholder="Login"
                        />
                    </FormGroup>
                    <FormGroup controlId="password">
                        <Form.Label>Password</Form.Label>
                        <FormControl
                           type="password"
                           name="password"
                           required
                           placeholder="Password"
                        />
                    </FormGroup>
                    <Button type="submit">
                        Login
                     </Button>
                </Form>
            </div>
        );
    }
}