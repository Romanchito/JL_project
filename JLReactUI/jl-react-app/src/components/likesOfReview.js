import React, { Component } from 'react';
import LikeApi from './api-route-components/likesApi';
import { Button } from 'react-bootstrap';

export class Likes extends Component {
    constructor(props) {
        super(props);
        this.state = {
            values: {
                reviewId: 0,
                isLike: false
            }
        }
    }  

    showLikes = (id, value) => {
        this.setState({
            values: {
                reviewId: id,
                isLike: value
            }
        }, 
            function(){
                new LikeApi().addLike(JSON.stringify(this.state.values));
            }
        )
        console.log("ID:" + this.state.values.reviewId);
        console.log("Type:" + this.state.values.isLike);           
    }

    render() {
        const idparam = this.props.reviewid;
       
        return (
            <div className="review-likes-dislikes-buttons-block">
                <table>
                    <tbody>
                        <tr>
                            <td id="likes">
                                <Button variant='success' onClick={() => this.showLikes(idparam, true)}>Like</Button>
                            </td>
                            <td>&nbsp;|&nbsp;</td>
                            <td id="likes">
                                <Button variant='danger' onClick={() => this.showLikes(idparam, false)}>Like</Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        )
    }
}