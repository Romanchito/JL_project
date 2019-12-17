import React, { Component } from "react";
import { Button, FormGroup, FormControl, Form } from "react-bootstrap";
import '../styles/register_styles.css';
import UserApi from './api-route-components/userApi';
import JwtApi from './api-route-components/jwtApi';

export class Registration extends Component {

    constructor() {
        super();
        this.state = {
            values: { login: "", password: "", name: "", surname: "" },
            log_user: { username: "", password: "" },
            isError: false
        };
    }


    signIn = async (log_username, log_password) => {  
        this.setState({
            log_user: { username: log_username, password: log_password }
        });        
        const data = await new JwtApi().getJwtToken(JSON.stringify(this.state.log_user));
        localStorage.setItem('your-jwt', data);
        this.props.history.push('/user');
    }

    submitForm = async e => {
        e.preventDefault();
        const data = await new UserApi().addNewUser(JSON.stringify(this.state.values));
        if (data !== "This user already exists") {
            await this.signIn(this.state.values.login, this.state.values.password);
        }
        else {
            setTimeout(
                () => this.setState({ message: "", isError: false }), 1800
            );
            this.setState({ message: data, isError: true, values: { login: "", password: "", name: "", surname: "" } });

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
                            type="email"
                            name="login"
                            id="login"
                            title="Login"
                            placeholder="example@domain.com"
                            value={this.state.values.login}
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
                    <FormGroup>
                        <Form.Label>Name</Form.Label>
                        <FormControl
                            type="text"
                            name="name"
                            id="name"
                            title="Name"
                            placeholder="name"
                            value={this.state.values.name}
                            onChange={this.handleInputChange}
                            required
                        />
                    </FormGroup>
                    <FormGroup>
                        <Form.Label>Surname</Form.Label>
                        <FormControl
                            type="text"
                            name="surname"
                            id="surname"
                            title="Surname"
                            placeholder="surname"
                            value={this.state.values.surname}
                            onChange={this.handleInputChange}
                            required
                        />
                    </FormGroup>
                    <div className="register_button">
                        <Button type="submit" variant="success" >
                            Create account
                     </Button>
                    </div>
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