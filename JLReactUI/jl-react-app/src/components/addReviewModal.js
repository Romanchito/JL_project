import React, { Component } from 'react';
import { Modal, Button, Row, Form } from 'react-bootstrap';

export class AddReviewModal extends Component {

    constructor(props){
        super(props);
        this.state = {
            values: { name:"", text:"", filmId:""}
        }
    }

    handleSubmit = (e) => {
        e.preventDefault();  
        console.log(this.props.show);     
        console.log(this.props.resId);   
        
    }

    handleInputChange = e =>
    this.setState({
        values: { ...this.state.values, [e.target.name]: e.target.value }
    });

    render() {
        return (
            <Modal
                {...this.props}
                size="lg"
                aria-labelledby="contained-modal-title-vcenter"
                centered
            >
                <Modal.Header closeButton>
                    <Modal.Title id="contained-modal-title-vcenter">
                        Add Review
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>

                    <div className="container">
                        <Row>
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

                        </Row>
                    </div>
                </Modal.Body>               
            </Modal>
        )
    }
}