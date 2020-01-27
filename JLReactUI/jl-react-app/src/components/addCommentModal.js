import React, { Component } from 'react';
import { Button, Form } from 'react-bootstrap';
import CommentApi from './api-route-components/commentApi';


export class AddCommentModal extends Component {

    constructor() {
        super();
        this.state = {
            values: { text: "", reviewId: 0 }
        }
    }

    handleSubmit = e => {
        console.log(this.state.values)
        e.preventDefault();
        this.setState(
            {
                values: { ...this.state.values, reviewId: +this.props.id }
            }, () => {
                new CommentApi().addComment(JSON.stringify(this.state.values)).then(() => this.props.resetfunc())
            }
        )
    }

    handleInputChange = e => {
        this.setState({
            values: { ...this.state.values, [e.target.name]: e.target.value }
        });
    }

    render() {

        return (
            <Form onSubmit={this.handleSubmit}>
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
        )
    }
}
