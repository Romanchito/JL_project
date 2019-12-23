import React, { Component } from 'react';
import ReviewsApi from './api-route-components/reviewsApi';
import CommentApi from './api-route-components/commentApi';
import { Likes } from './likesOfReview';

export class MainReviewInform extends Component {
    constructor(props) {
        super(props)
        this.state = { review:0,  comments:[]
        }
    }

    componentDidMount() {
        this.getDataOfReview();        
    }


   async getDataOfReview(){
        console.log(this.state);
        const id = this.props.match.params.id;             
        this.setState({review: await new ReviewsApi().getReviewById(id)});
        this.getDataOfComments();
    }

    getDataOfComments(){
        const id = this.props.match.params.id;
        new CommentApi().getAllCommentsOfReview(id)
            .then(data => {
                this.setState({ comments: data });
            });
            
    }

    render = () => {
        console.log("THIS COUNT " + this.state.review.countOfLikes)  
        console.log("THIS COUNT " + this.state.review.countOdDislikes)   
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
                        reviewid = {this.state.review.id}
                    />
                </div>

                <div className="main-comments-block">
                    {this.state.comments.map((comment) =>
                        <div className="comment-block" key={comment.id}>
                            <div className="comment-account-image"></div>
                            <div className="comment-text-block">
                                {comment.text}
                                <p className="comment-date-block">{comment.date}</p>
                            </div>
                        </div>
                    )}
                </div>
            </div>
        )
    }
}