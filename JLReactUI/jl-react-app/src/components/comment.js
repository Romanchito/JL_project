import React, { Component } from 'react';

export class Comment extends Component {

    constructor(props) {
        super(props);
        this.state = { comments: [] };
    }

    componentDidMount() {
        this.refreshList();
    }

    refreshList() {
        const id = this.props.id;
        fetch('https://localhost:44327/api/Comments/review/' + id)
            .then(response => response.json())
            .then(data => {
                this.setState({ comments: data });
            });
    }

    render() {

        const comments = this.state.comments;
        return (
            comments.map((comment) =>
            <div key={comment.id} className="comments_block">
                
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