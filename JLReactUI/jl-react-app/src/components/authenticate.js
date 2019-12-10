import React, { Component } from 'react';
import { withRouter, Redirect } from "react-router-dom";
import { getJwt } from './helpers/jwtHelper';

class AuthComponent extends Component {
    constructor(props) {
        super(props);
        this.state = {
            user: undefined
        };
    }

    componentDidMount() {
        this.getUser();
    }

    getUser() {
        if (!localStorage.getItem('your-jwt')) {
            this.setState({
                user: null
            });
            return;
        }


        fetch('https://localhost:44327/api/Users/user1', {
            headers: { Authorization: getJwt() }
        })
            .then(response => response.json())
            .then(data => {
                this.setState({ user: data });
            });


    }

    render() {
        const { user } = this.state;
        if (user === undefined) {
            return (
                <div>
                    Loading...
            </div>
            );
        }

        if (user === null) {
            return <Redirect to="/log" />
        }

        return this.props.children;
    }

}
export default withRouter(AuthComponent)
