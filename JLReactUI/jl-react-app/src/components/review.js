import React, { Component } from 'react';
import { Comment } from './comment';

export class Review extends Component {

    constructor(props) {
        super(props);
        this.state = { reviews: [] };
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
        return (

            <div className="reviews_block">
                {reviews.map((review) =>
                    <div key={review.id} className="review_inform_block">
                        <h2>{review.name}</h2>
                        <h3>{review.userLogin}</h3>
                        <div className="main_inform_review_block">                            
                            <div className="text_review_block"><p>{review.text}</p></div>
                            <p>Date: {review.date}</p>
                            <div className="review_like_block"><p>Likes: {review.likesCount}</p></div>
                        </div>   

                        <Comment id={review.id}/>

                    </div>                   
                )}
            </div>
        )
    }
}