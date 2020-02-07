import React, { Component } from 'react';
import LikeApi from './api-route-components/likesApi';
import { Button } from 'react-bootstrap';

export class Likes extends Component {
    constructor(props) {
        super(props);
        this.state = {
            values: { reviewId: 0, isLike: false },
            likesApi: new LikeApi()
        }
    }

    showLikes = (id, value) => {
        this.setState({
            values: {
                reviewId: id,
                isLike: value
            }
        },
            () => {
                this.state.likesApi.addLike(JSON.stringify(this.state.values))
                    .then(() => this.props.resetfunc());
            }
        )
    }

    render() {
        const { reviewid } = this.props;

        return (
            <div className="review-likes-dislikes-buttons-block">
                <table>
                    <tbody>
                        <tr>
                            <td id="likes">
                                <Button variant='success' onClick={this.showLikes.bind(this, reviewid, true)}>Like</Button>
                            </td>
                            <td id="likes">
                                <Button variant='danger' onClick={this.showLikes.bind(this, reviewid, false)}>Dislike</Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        )
    }
}