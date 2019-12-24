import React, { Component } from 'react';
import { Button, ButtonToolbar } from 'react-bootstrap';
import { AddReviewModal } from './addReviewModal';
import { Link } from 'react-router-dom';
import ReviewsApi from './api-route-components/reviewsApi';

export class Review extends Component {

    constructor(props) {
        super(props);
        this.state = { reviews: [], addModalShow: false };
    }

    componentDidMount() {
        this.refreshList();
    }

    refreshList = () => {       
        new ReviewsApi().getAllReviewsOfFilm(this.props.id)
            .then(data => {
                this.setState({ reviews: data });
            });
    }

    render = () => {
        const reviews = this.state.reviews;
        const valueId = this.props.id;
        let addModalClose = () => {
            this.setState({ addModalShow: false });
            this.refreshList();
        };
        return (
            <div className="reviews_data_block">
                <ButtonToolbar>
                    <div className="add_review_block">
                        <Button variant="primary" onClick={() => this.setState({ addModalShow: true })}>
                            Add review
                </Button>
                    </div>
                    <AddReviewModal
                        show={this.state.addModalShow}
                        onHide={addModalClose}
                        resid={valueId}
                    />
                </ButtonToolbar>
                {reviews.map((review) =>
                    <div key={review.id} className="reviews_block">
                        <div className="review_inform_block">
                            <Link to={{ pathname: `/review/${review.id}` }}>
                                <h2>{review.name}</h2>
                            </Link>
                            <h3>{review.userLogin}</h3>
                            <div className="main_inform_review_block">
                                <div className="text_review_block"><p>{review.text}</p></div>
                                <div className="review_like_block"><p>{review.likesCount}</p></div>
                            </div>

                        </div>
                    </div>
                )}

            </div>
        )
    }
}