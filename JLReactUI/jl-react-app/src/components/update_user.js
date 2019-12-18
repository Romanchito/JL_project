import React, { Component } from 'react';
import { Button, FormGroup, FormControl, Form, ButtonToolbar } from "react-bootstrap";
import UserApi from './api-route-components/userApi';

export class UpdateUser extends Component {

    constructor() {
        super();
        this.state = {
            values: { name: "", surname: "" },
            accountImage: null

        };
    }

    submitForm = async e => {
        e.preventDefault();
        console.log(this.state.val);
        const id = this.props.match.params.id;
        const data = await new UserApi().updateUser(JSON.stringify(this.state.values), id);

        if (!(data.hasOwnProperty("error"))) {
            this.props.history.push('/user');
        }
        else {
            setTimeout(
                () => this.setState({ message: "", isError: false }), 1800
            );
            this.setState({ message: data.error, isError: true, values: { name: "", surname: "" } });

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
                    <ButtonToolbar>
                        <div className="login-button">
                            <Button type="success">
                                Update
                        </Button>
                        </div>
                        <div className="login-button">
                            <Button type="danger" onClick={this.cancleRedirect}>
                                Cancle
                            </Button>
                        </div>
                    </ButtonToolbar>
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