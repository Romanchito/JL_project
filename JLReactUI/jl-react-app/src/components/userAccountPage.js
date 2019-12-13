import React, { Component } from 'react';
import UserApi from './api-route-components/userApi';
import ImageApi from './api-route-components/imageApi';
import '../styles/user_account_styles.css';
import { Button} from "react-bootstrap";

export default class UserAccount extends Component {

    constructor(prop) {
        super(prop);
        this.state = { user: {}, path: {} };
    }

    componentDidMount() {
        this.getDataOfUser()
    }

    getDataOfUser() {
        let jwt_decode = require('jwt-decode');
        let login = jwt_decode(localStorage.getItem("your-jwt"));
        console.log(login.email);
        new UserApi().getUserByLogin(login.email).then(result => this.setState({ user: result }));
        new ImageApi().getUserImage().then(data => {
            this.setState({ path: data });
        });
    }

    render() {
        const user = this.state.user;
        const path = this.state.path;

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
                            <Button>
                                Update
                            </Button>
                    </div>
                </div>

                <div className="user-review-block">

                </div>
            </div>
        );
    }
}