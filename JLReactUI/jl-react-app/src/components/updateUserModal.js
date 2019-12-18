import React, { Component } from 'react';
import { Modal, Button, Row, Form, FormGroup } from 'react-bootstrap';
import { Link } from 'react-router-dom';

export class UpdateUserModal extends Component {

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
                        Functionality
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div className="container">
                        <Row>

                            <Form >
                                <FormGroup>
                                    <ul>
                                        <li>
                                            <Link to={{ pathname: `/update_user_inform/${this.props.user.id}`}} params={{ testvalue: "hello" }}  >
                                                Update main information
                                            </Link>
                                        </li>
                                        <li>
                                            <Link to={{ pathname: `/refresh_password/${this.props.user.id}` }} >
                                                Refresh password
                                            </Link>
                                        </li>
                                        <li>
                                            <Link to={{ pathname: `/uploadImage` }} >
                                                Update Image
                                            </Link>
                                        </li>                                            
                                    </ul>
                                </FormGroup>
                            </Form>
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