import React, { Component } from 'react';
import UserApi from './api-route-components/userApi';
import ImageApi from './api-route-components/imageApi';
import ReviewsApi from './api-route-components/reviewsApi';
import '../styles/user_account_styles.css';
import { Button, ButtonToolbar } from "react-bootstrap";
import { UpdateUserModal } from './updateUserModal';

export default class UserAccount extends Component {

    constructor(prop) {
        super(prop);
        this.state = {
            user: {}, path: {}, reviews: [], updateModalShow: false,
            userApi: new UserApi(), imgApi: new ImageApi(), reviewApi: new ReviewsApi()
        };
    }

    componentDidMount() {
        this.getDataOfUser()
    }

    getDataOfUser() {
        let jwt_decode = require('jwt-decode');
        let login = jwt_decode(localStorage.getItem("your-jwt"));

        this.state.userApi.getUserByLogin(login.email).then(result => this.setState({ user: result }));

        this.state.imgApi.getAccountImage().then(data => {
            this.setState({ path: data });
        });

        this.state.reviewApi.getAllReviewsOfUser().then(data => {
            this.setState({ reviews: data });
        });
    }

    updateModalClose = () => this.setState({ updateModalShow: false });
    updateModalShow = () => this.setState({ updateModalShow: true });

    render() {
        const user = this.state.user;
        const path = this.state.path;
        const reviews = this.state.reviews;
        return (
            <div className="main-user-block">
                <div className="user-main-inform-block">
                    <div className="user-image-block">
                        <img src={path} id="account-img" alt={user.accountImage} />
                    </div>

                    <div className="user-inform-block">
                        <h2>Main information</h2>
                        <table>
                            <tbody>

                                <tr>
                                    <td>Login:</td>
                                    <td>{user.login}</td>
                                </tr>
                                <tr>
                                    <td>Name:</td>
                                    <td>{user.name}</td>
                                </tr>
                                <tr>
                                    <td>Surname:</td>
                                    <td>{user.surname}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>

                    <div id="updateButton">
                        <ButtonToolbar>
                            <div className="update_user_block">
                                <Button variant="primary" onClick={this.updateModalShow}>
                                    Update
                        </Button>
                            </div>
                            <UpdateUserModal
                                show={this.state.updateModalShow}
                                onHide={this.updateModalClose}
                                user={this.state.user}
                            />
                        </ButtonToolbar>
                    </div>
                </div>

                <div className="user-review-block">
                    {reviews.map((review) =>
                        <div key={review.id} className="review-main-block">
                            <p id="reviewHeader">{review.name}</p>
                            <div id="reviewDate">{review.date}</div>
                            <div id="textOfReview">
                                {review.text}
                            </div>
                            <div className="review-likes-dislikes-block">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td id="likes">{review.countOfLikes}</td>
                                            <td>&nbsp;|&nbsp;</td>
                                            <td id="dislikes">{review.countOfDislikes}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    )}
                </div>
            </div>
        );
    }
}