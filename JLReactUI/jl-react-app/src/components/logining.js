

import React, { Component } from "react";
import { Button, FormGroup, FormControl, Form } from "react-bootstrap";


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
        console.log(this.state);
        this.setState({ isSubmitting: true });

        const res = await fetch('https://localhost:44327/api/Auth/jwtToken', {
            method: 'POST',
            body: JSON.stringify(this.state.values),
            headers: {
                'Content-type': 'application/json',
                'Accept': 'application/json'
            }
        });
        this.setState({ isSubmitting: false });
        const data = await res.json();
        localStorage.setItem('your-jwt', data);
        !data.hasOwnProperty("error")
            ? this.setState({ message: data.success })
            : this.setState({ message: data.error, isError: true });

        setTimeout(
            () =>
                this.setState({
                    isError: false,
                    message: "",
                    values: { username: "", password: "" }
                }),
            1600
        );
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
                    <Button type="submit">
                        Sing in
                     </Button>
                    <FormGroup>
                        <div className={`message ${this.state.isError && "error"}`}>
                            {this.state.isSubmitting ? "Submitting..." : this.state.message}
                        </div>
                    </FormGroup>
                </Form>
            </div>
        );
    }
}