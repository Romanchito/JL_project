import React, { Component } from 'react';
import ImageApi from './api-route-components/imageApi';

export class UserImage extends Component {

    constructor(props) {
        super(props)
        this.state = {
            path: "",
            imgApi: new ImageApi()
        }
    }

    componentDidMount() {
        this.showImage();
    }

    showImage() {
        this.state.imgApi.getUserImage(this.props.id).then(data => {
            this.setState({ path: data });
        });
    }

    render() {
        return (
            <img className="image" alt="user_image" src={this.state.path} />
        )
    }
} 