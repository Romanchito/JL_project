import React, { Component } from 'react';
import { Button, FormGroup, FormControl, Form, ButtonToolbar } from "react-bootstrap";
import UserApi from './api-route-components/userApi';
import { MyFormControl } from './helpers/myFormControl';

export class UpdateUser extends Component {

    constructor(props) {
        super(props);
        this.state = {
            values: { name: "", surname: "" },
            errors: {},
            userApi: new UserApi()
        };
    }

    submitForm = async e => {
        e.preventDefault();
        const id = this.props.match.params.id;
        const data = await this.state.userApi.updateUser(JSON.stringify(this.state.values), id);

        if (!(data.hasOwnProperty("errors"))) {
            this.props.history.push('/user');
        }
        else {
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
                        <Form.Label>Name</Form.Label>
                        <MyFormControl
                            errorslist={this.state.errors["Name"]}
                            formcontrol={<FormControl
                                type="text"
                                name="name"
                                id="name"
                                title="Name"
                                placeholder="name"
                                value={this.state.values.name}
                                onChange={this.handleInputChange}
                                required
                            />}
                        />
                    </FormGroup>
                    <FormGroup>
                        <Form.Label>Surname</Form.Label>
                        <MyFormControl
                            errorslist={this.state.errors["Surname"]}
                            formcontrol={<FormControl
                                type="text"
                                name="surname"
                                id="surname"
                                title="Surname"
                                placeholder="surname"
                                value={this.state.values.surname}
                                onChange={this.handleInputChange}
                                required
                            />}
                        />
                    </FormGroup>
                    <ButtonToolbar>
                        <div className="login-button">
                            <Button type="submit" variant="success">
                                Update
                        </Button>
                        </div>
                        <div className="login-button">
                            <Button variant="danger" onClick={this.cancleRedirect}>
                                Cancle
                            </Button>
                        </div>
                    </ButtonToolbar>
                </Form>
            </div>
        );
    }
}