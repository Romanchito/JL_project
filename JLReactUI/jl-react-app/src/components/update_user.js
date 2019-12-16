import React, { Component } from 'react';
import { Button, FormGroup, FormControl, Form, ButtonToolbar } from "react-bootstrap";

import UserApi from './api-route-components/userApi';
import ImageApi from './api-route-components/imageApi';


export class UpdateUser extends Component {
    constructor() {
        super();
        this.state = {
            values: { name: "", surname: "", password: "" },
            accountImage: null
        };
    }

    submitForm = async e => {
        e.preventDefault();
        const id = this.props.match.params.id;
        const data = await new UserApi().updateUser(JSON.stringify(this.state.values), id);


        const uploadFile = new FormData();
        uploadFile.append('image' + id, this.state.accountImage, this.state.accountImage.name);
        await new ImageApi().uploadUserAccountImage(JSON.stringify(uploadFile)).then(res => {
            console.log(res);
        });

        if (!(data.hasOwnProperty("error"))) {
            this.props.history.push('/user');
        }
        else {
            setTimeout(
                () => this.setState({ message: "", isError: false }), 1800
            );
            this.setState({ message: data.error, isError: true, values: { name: "", surname: "", password: "" } });

        }
    }

    handleInputChange = e =>
        this.setState({
            values: { ...this.state.values, [e.target.name]: e.target.value }
        });

    imageHandler = e =>
        this.setState({
            accountImage: e.target.files[0]
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
                        <Form.Label>Account Image</Form.Label>
                        <FormControl type="file" onChange={(e) => this.setState({ accountImage: e.target.files[0] })} />
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