import React, { Component } from 'react';
import { Button, FormGroup, FormControl, Form, ButtonToolbar } from 'react-bootstrap';
import ImageApi from './api-route-components/imageApi';

export class UpdateUserImage extends Component {
    constructor(props) {
        super(props);
        this.state = {
            file: ""
        };
    }

    submitForm = async e => {
        e.preventDefault();
        const formData = new FormData();
        const fileField = document.querySelector('input[type="file"]');
        console.log(fileField);
        formData.append('file', fileField.files[0]);
        new ImageApi().uploadUserAccountImage(formData);
    }

    handleFileInput = e => {
        this.setState({
            file: e.target.value
        });

    }

    cancleRedirect = () => {
        this.props.history.push('/user');
    }
    render() {
        return (
            <div>
                <Form onSubmit={this.submitForm} className="login-form">
                    <FormGroup>
                        <Form.Label>Image</Form.Label>
                        <FormControl
                            type="file"
                            name="file"
                            id="file"
                            title="Image"
                            value={this.state.file}
                            onChange={this.handleFileInput}
                        />
                    </FormGroup>
                    <FormGroup>
                        <ButtonToolbar>
                            <div className="register_button">
                                <Button type="submit" variant="success" >
                                    Update
                        </Button>
                            </div>
                            <div className="login-button">
                                <Button variant="danger" onClick={this.cancleRedirect}>
                                    Cancle
                            </Button>
                            </div>
                        </ButtonToolbar>
                    </FormGroup>
                </Form>
            </div>
        );
    }
}