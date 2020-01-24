import React, { Component } from 'react';
import { Button, ButtonToolbar } from 'react-bootstrap';
import { AddReviewModal } from './addReviewModal';
import { Link } from 'react-router-dom';
import ReviewsApi from './api-route-components/reviewsApi';
import Paginator from './helpers/customPaginator';

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

    addModalClose = () => {
        this.setState({ addModalShow: false });
        this.refreshList();
    };

    render = () => {
        const reviews = this.state.reviews;
        const valueId = this.props.id;      
        return (
            
            <div className="reviews_data_block">
                <Paginator currentPage={1} onPageChanged={this.refreshList} totalItemsCount={20} pageSize={3}/>
                <ButtonToolbar>
                    <div className="add_review_block">
                        <Button variant="primary" onClick={() => this.setState({ addModalShow: true })}>
                            Add review
                </Button>
                    </div>
                    <AddReviewModal
                        show={this.state.addModalShow}
                        onHide={this.addModalClose}
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