import React, { Component } from 'react';
import ReviewsApi from './api-route-components/reviewsApi';
import CommentApi from './api-route-components/commentApi';
import { Likes } from './likesOfReview';
import { AddCommentModal } from './addCommentModal';


export class MainReviewInform extends Component {
    constructor(props) {
        super(props)
        this.state = {
            review: 0, comments: []
        }
    }

    componentDidMount() {
        this.getDataOfReview(this.props.match.params.id);
    }

    async getDataOfReview(id) {
        console.log("METHOD DONE");

        this.setState({ review: await new ReviewsApi().getReviewById(id) });
        this.getDataOfComments();
    }

    getDataOfComments() {
        const id = this.props.match.params.id;
        new CommentApi().getAllCommentsOfReview(id)
            .then(data => {
                this.setState({ comments: data });
            });

    }

    render = () => {
        return (
            <div className="main-data-review-block">
                <div className="main-review-block">
                    <h2>{this.state.review.name}</h2>
                    <h4>{this.state.review.userLogin}</h4>
                    <div className="review-text-block">
                        {this.state.review.text}
                        <div className="review-date-block">{this.state.review.date}</div>
                    </div>
                    <div className="review-likes-dislikes-block">
                        <table>
                            <tbody>
                                <tr>
                                    <td id="likes">{this.state.review.countOfLikes}</td>
                                    <td>&nbsp;|&nbsp;</td>
                                    <td id="dislikes">{this.state.review.countOdDislikes}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <Likes
                        reviewid={this.state.review.id}
                        resetfunc={() => this.getDataOfReview}
                    />
                </div>

                <div className="add-comment-block">
                    <AddCommentModal
                        id={this.props.match.params.id}
                    />
                </div>
                <div className="main-comments-block">
                    {this.state.comments.map((comment) =>
                        <div className="comment-block" key={comment.id}>
                            <div className="both-block"></div>
                            <div className="comment-account-image">
                                <img className="image"
                                    alt="user_image"
                                />
                            </div>
                            <div className="comment-text-block">
                                {comment.date}<br/>
                                {comment.text}
                            </div>
                            <div className="both-block"></div>
                        </div>
                    )}
                </div>
            </div>
        )
    }
}