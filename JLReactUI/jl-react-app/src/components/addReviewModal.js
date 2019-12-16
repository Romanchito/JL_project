import React, { Component } from 'react';
import { Modal, Button, Row, Col, Form } from 'react-bootstrap';

export class AddReviewModal extends Component {
   

    handleSubmit(e){
        e.preventDefault();
        alert(e.target.DName.value);
    }

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
                                <Col sm={6}>
                                    <Form onSubmit={this.handleSubmit}>
                                        <Form.Group controlId="DName">
                                            <Form.Control
                                                type="text"
                                                name="DName"
                                                required
                                                placeholder="Review Name"
                                                >                                                
                                            </Form.Control>
                                        </Form.Group>

                                        <Form.Group>
                                            <Button variant="primary" type="sumbit">
                                                Add
                                            </Button>
                                        </Form.Group>
                                    </Form>
                                </Col>
                            </Row>
                        </div>
                    
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="danger" onClick={this.props.onHide}>Close</Button>
                </Modal.Footer>
            </Modal>
        )
    }
}