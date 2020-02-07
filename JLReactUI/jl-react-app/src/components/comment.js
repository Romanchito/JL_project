import React, { Component } from 'react';
import CommentApi from './api-route-components/commentApi';

export class Comment extends Component {

    constructor(props) {
        super(props);
        this.state = { comments: [], commentApi: new CommentApi() };
    }

    componentDidMount() {
        this.refreshList();
    }

    refreshList() {
        const id = this.props.id;
        this.state.commentApi.getAllCommentsOfReview(id)
            .then(data => {
                this.setState({ comments: data });
            });
    }

    render() {
        const { comments } = this.state;
        return (
            comments.map((comment) =>
                <div key={this.props.id} className="comments_block">

                    <div className="comments_inform_block">
                        <h2>{comment.text}</h2>
                        <div className="text_comments_block"><p>{comment.text}</p></div>
                        <div className="date_comment_block"><p>Date: {comment.date}</p></div>
                    </div>
                </div>
            )
        )
    }
}