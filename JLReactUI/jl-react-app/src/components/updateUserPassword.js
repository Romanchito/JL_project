import React, { Component } from 'react';
import { Button, FormGroup, FormControl, Form, ButtonToolbar } from "react-bootstrap";
import UserApi from './api-route-components/userApi';


export class RefreshPassword extends Component {
    constructor() {
        super();
        this.state = {
            values: { password: "", re_password: "" }
        };
    }

    submitForm = async e => {
        e.preventDefault();
        const id = this.props.match.params.id;        
        if (this.state.values.password === this.state.values.re_password) {
            console.log("password:"+ this.state.values.password);
            console.log("re_password:"+ this.state.values.re_password);
            await new UserApi().updateUserPassword(JSON.stringify(this.state.values.password), id);
            this.props.history.push('/user');
        }

        else {
            console.log(this.state.values.password + "!=" + this.state.values.re_password);
            setTimeout(
                () => this.setState({ message: "", isError: false }), 1800
            );
            this.setState({ message: "Password and password are different", isError: true, values: { password: "", re_password: "" } });
        }


    }


    handleInputChange = e =>
        this.setState({
            values: { ...this.state.values, [e.target.name]: e.target.value }
        });

    cancleRedirect = () => {
        this.props.history.push('/user');
    }

    render() {
        return (
            <div>
                <Form onSubmit={this.submitForm} className="login-form">
                    <FormGroup>
                        <Form.Label>Password</Form.Label>
                        <FormControl
                            type="text"
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
                        <Form.Label>Re-entry</Form.Label>
                        <FormControl
                            type="text"
                            name="re_password"
                            id="re_password"
                            title="re_password"
                            placeholder="re_password"
                            value={this.state.values.re_password}
                            onChange={this.handleInputChange}
                            required
                        />
                    </FormGroup>
                    <div>
                        <ButtonToolbar>
                            <Button type="submit" variant="success" >
                                Refresh
                        </Button>

                            <Button variant="danger" onClick={this.cancleRedirect}>
                                Cancle
                        </Button>
                        </ButtonToolbar>
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