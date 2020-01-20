import React, { Component } from "react";
import { Button, FormGroup, FormControl, Form } from "react-bootstrap";
import { Link } from 'react-router-dom';
import JwtApi from './api-route-components/jwtApi';
import { MyFormControl } from './helpers/myFormControl';

export class Login extends Component {

    constructor() {
        super();
        this.state = {
            values: { username: "", password: "" },
            errors: {}
        };
    }

    submitForm = async e => {
        e.preventDefault();

        const data = await new JwtApi().getJwtToken(JSON.stringify(this.state.values));

        console.log(data);
        if (!(data.hasOwnProperty("errors"))) {
            localStorage.setItem('your-jwt', data.jwtHandler);
            this.props.history.push('/user');
        }

        else {

            console.log(data.errors);
            this.setState({ errors: data.errors });
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
                        <MyFormControl
                            errorslist={this.state.errors["Username"]}
                            formcontrol={<FormControl
                                type="text"
                                name="username"
                                id="username"
                                title="username"
                                placeholder="example@domain.com"
                                value={this.state.values.username}
                                onChange={this.handleInputChange}
                                required
                            />}
                        />
                    </FormGroup>
                    <FormGroup>
                        <Form.Label>Password</Form.Label>
                        <MyFormControl
                            errorslist={this.state.errors["Password"]}
                            formcontrol={<FormControl
                                type="password"
                                name="password"
                                id="password"
                                title="Password"
                                placeholder="password"
                                value={this.state.values.password}
                                onChange={this.handleInputChange}
                                required
                            />}
                        />
                    </FormGroup>

                    <FormGroup>
                        <MyFormControl
                            errorslist={this.state.errors["general"]}
                            formcontrol={null}
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
                </Form>
            </div>
        );
    }
}