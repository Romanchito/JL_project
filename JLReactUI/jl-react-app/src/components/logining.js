import React, { Component } from "react";
import { Button, FormGroup, FormControl, Form } from "react-bootstrap";
import { Redirect,Link } from 'react-router-dom';
import JwtApi from './api-route-components/jwtApi';

export class Login extends Component {

    constructor() {
        super();
        this.state = {
            values: { username: "", password: "" },
            isSubmitting: false,
            isError: false
        };
    }

    submitForm = async e => {
        e.preventDefault();       
        
        const data = await new JwtApi().getJwtToken(JSON.stringify(this.state.values));
        if (!(data.hasOwnProperty("error"))) {
            localStorage.setItem('your-jwt', data);
            this.setState({ message: data.success });           
            return <Redirect push to="/user" />                      
        }
        else {
            setTimeout(
                () => this.setState({ message: "", isError: false }), 1800
            );
            this.setState({ message: data.error, isError: true, values: { username: "", password: "" } });

        }
    }

    handleInputChange = e =>
        this.setState({
            values: { ...this.state.values, [e.target.name]: e.target.value }
        });

    render() {
        return (
            <div>
                <Form onSubmit={this.submitForm} className="login-form">
                    <FormGroup>
                        <Form.Label>Email</Form.Label>
                        <FormControl
                            type="text"
                            name="username"
                            id="username"
                            title="Login"
                            placeholder="example@domain.com"
                            value={this.state.values.username}
                            onChange={this.handleInputChange}
                            required
                        />
                    </FormGroup>
                    <FormGroup>
                        <Form.Label>Password</Form.Label>
                        <FormControl
                            type="password"
                            name="password"
                            id="password"
                            title="Password"
                            placeholder="password"
                            value={this.state.values.password}
                            onChange={this.handleInputChange}
                            required
                        />
                    </FormGroup>
                    <div className="login-button">
                        <Button type="submit">
                            Sign in
                        </Button>
                    </div>

                    <FormGroup>
                        <div className="register-field">
                            <Link to={{ pathname: `/register` }} >
                                Create account
                           </Link>
                        </div>
                    </FormGroup>

                    <FormGroup>
                        <div id="errorBlock" className={`message ${this.state.isError && "error"}`}>
                            {this.state.isSubmitting ? "Submitting..." : this.state.message}
                        </div>
                    </FormGroup>
                </Form>
            </div>
        );
    }
}