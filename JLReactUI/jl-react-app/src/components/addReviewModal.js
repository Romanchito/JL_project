import React, { Component } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import ReviewsApi from './api-route-components/reviewsApi';

export class AddReviewModal extends Component {

    constructor(props) {
        super(props);
        this.state = {
            values: { name: "", text: "", filmId: +this.props.resid }
        }
    }
  
    handleSubmit = async (e) => {
        e.preventDefault();       
        console.log(this.state.values);
        await new ReviewsApi().addReview(JSON.stringify(this.state.values));
        this.props.onHide();           
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
                                    <Form.Control
                                        type="text"
                                        name="name"
                                        id="name"
                                        title="name"
                                        placeholder="Name"
                                        value={this.state.values.name}
                                        onChange={this.handleInputChange}
                                        required
                                    >
                                    </Form.Control>
                                </Form.Group>

                                <Form.Group>
                                    <Form.Control
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
                                    >
                                    </Form.Control>
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