import React, { Component } from 'react';
import { Button, FormGroup, FormControl, Form, ButtonToolbar } from 'react-bootstrap';
import ImageApi from './api-route-components/imageApi';

export class UpdateUserImage extends Component {
    constructor(props) {
        super(props);
        this.state = {
            file: "",
            isFalseType: false,
            imageApi: new ImageApi()
        };
    }

    submitForm = async e => {
        e.preventDefault();

        const formData = new FormData();
        const fileField = document.querySelector('input[type="file"]');
        formData.append('file', fileField.files[0]);

        let typeCheck = RegExp("[|.|\\w|\\s|-]*\\.(?:jpg|gif|png)")

        

        if (fileField.files[0] !== undefined) {
            if (typeCheck.test(fileField.files[0].name)) {
                this.state.imageApi.uploadUserAccountImage(formData);
            }
            else { this.setState({ isFalseType: true }); }
        }
        else {
            this.state.imageApi.uploadUserAccountImage(formData);
        }

        this.props.history.push('/user');
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
                        {this.state.isFalseType ? <p id="errorBlock">Wrong file type</p> : ""}
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