import React, { Component } from 'react';
import { Button, FormGroup, FormControl, Form, ButtonToolbar } from "react-bootstrap";
import UserApi from './api-route-components/userApi';
import { MyFormControl } from './helpers/myFormControl';


export class RefreshPassword extends Component {
    constructor() {
        super();
        this.state = {
            values: { password: "", confirmPassword: "" },
            errors: {}
        };
    }

    submitForm = async e => {
        e.preventDefault();
        const id = this.props.match.params.id;
        console.log(this.state.values.password)
        console.log(this.state.values.confirmPassword)
        const data = await new UserApi().updateUserPassword(JSON.stringify(this.state.values), id);
        if (!(data.hasOwnProperty("errors"))) {
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

    cancleRedirect = () => {
        this.props.history.push('/user');
    }

    render() {
        return (
            <div>
                <Form onSubmit={this.submitForm} className="login-form">
                    <FormGroup>
                        <Form.Label>Password</Form.Label>
                        <MyFormControl
                            errorslist={this.state.errors["Password"]}
                            formcontrol={<FormControl
                                type="text"
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
                        <Form.Label>ConfirmPassword</Form.Label>

                        <MyFormControl
                            errorslist={this.state.errors["ConfirmPassword"]}
                            formcontrol={<FormControl
                                type="text"
                                name="confirmPassword"
                                id="confirmPassword"
                                title="confirmPassword"
                                placeholder="confirmPassword"
                                value={this.state.values.confirmPassword}
                                onChange={this.handleInputChange}
                                required
                            />}
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