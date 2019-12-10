import React, { Component } from 'react';
import { Comment } from './comment';
import { Button, ButtonToolbar } from 'react-bootstrap';
import { AddReviewModel } from './addReviewModal';

export class Review extends Component {

    constructor(props) {
        super(props);
        this.state = { reviews: [], addModalShow: false };
    }

    componentDidMount() {
        this.refreshList();
    }

    refreshList() {
        const id = this.props.id;
        fetch('https://localhost:44327/api/Reviews/reviewsOfFilm/' + id)
            .then(response => response.json())
            .then(data => {
                this.setState({ reviews: data });
            });
    }

    render() {

        const reviews = this.state.reviews;
        let addModalClose = () => this.setState({ addModalShow: false });
        return (
            <div>

                <ButtonToolbar>
                    <div className="add_review_block">
                        <Button variant="primary" onClick={() => this.setState({ addModalShow: true })}>
                            Add review
                </Button>
                    </div>
                    <AddReviewModel
                        show={this.state.addModalShow}
                        onHide={addModalClose}
                    />
                </ButtonToolbar>
                {reviews.map((review) =>
                    <div key={review.id} className="reviews_block">
                        <div  className="review_inform_block">
                            <h2>{review.name}</h2>
                            <h3>{review.userLogin}</h3>
                            <div className="main_inform_review_block">
                                <div className="text_review_block"><p>{review.text}</p></div>
                                <p>Date: {review.date}</p>
                                <div className="review_like_block"><p>Likes: {review.likesCount}</p></div>
                            </div>
                            <Comment key={review.id} id={review.id} />
                        </div>

                    </div>
                )}

            </div>
        )
    }
}