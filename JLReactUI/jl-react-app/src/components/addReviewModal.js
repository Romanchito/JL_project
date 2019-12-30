import React, { Component } from 'react';
import { Modal, Button, Form, FormControl } from 'react-bootstrap';
import ReviewsApi from './api-route-components/reviewsApi';
import { MyFormControl } from './helpers/myFormControl';

export class AddReviewModal extends Component {

    constructor(props) {
        super(props);
        this.state = {
            values: { name: "", text: "", filmId: +this.props.resid },
            errors: {}
        }
    }

    handleSubmit = async (e) => {
        e.preventDefault();
        console.log(this.state.values);
        const data = await new ReviewsApi().addReview(JSON.stringify(this.state.values));
        if (!(data.hasOwnProperty("errors"))) {
            this.props.onHide();
        }
        else {
            console.log(data.errors);
            this.setState({ errors: data.errors });
        }        
    }


    handleInputChange = e => {

        if (e.target.name !== "filmId") {
            this.setState({
                values: { ...this.state.values, [e.target.name]: e.target.value }
            });
        }
        else {
            this.setState({
                values: { ...this.state.values, [e.target.name]: e.target.defaultValue }
            });
        }


    }
    render = () => {
        return (
            <Modal
                {...this.props}
                size="lg"
                aria-labelledby="contained-modal-title-vcenter"
                centered>

                <Modal.Body>
                    <div className="container">

                        <Form onSubmit={this.handleSubmit}>
                            <Form.Group>
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
                            </Form.Group>

                            <Form.Group>
                                <MyFormControl
                                    errorslist={this.state.errors["Text"]}
                                    formcontrol={<FormControl
                                        as="textarea"
                                        aria-label="With textarea"
                                        type="text"
                                        name="text"
                                        id="text"
                                        title="text"
                                        placeholder="Text"
                                        value={this.state.values.text}
                                        onChange={this.handleInputChange}
                                        required
                                    />}
                                />
                            </Form.Group>
                            <Form.Group>
                                <Button variant="primary" type="sumbit">
                                    Add
                                    </Button>
                            </Form.Group>
                        </Form>
                    </div>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="danger" onClick={this.props.onHide}>Close</Button>
                </Modal.Footer>
            </Modal>
        )
    }
}