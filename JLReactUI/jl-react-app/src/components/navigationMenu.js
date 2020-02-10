import React, { Component } from 'react';
import { Navbar, Nav, NavDropdown } from 'react-bootstrap';
import '../styles/user_account_styles.css';
import { withRouter } from 'react-router-dom';
import JwtApi from './api-route-components/jwtApi';

class NavigationMenu extends Component {

    constructor(props) {
        super(props);
        this.logout = this.logout.bind(this);
        this.state = {
            jwtApi: new JwtApi()
        }
    }

    logout = () => {
        this.state.jwtApi.removeJwtToken();
        this.props.history.push('/log');
    }

    render() {
        let jwt_decode = require('jwt-decode');
        let login;
        if (this.state.jwtApi.getLocalStorageToken()) {
            login = jwt_decode(this.state.jwtApi.getLocalStorageToken());
        }
        let isLoggedIn = false;

        if (this.state.jwtApi.getLocalStorageToken() !== null &&
            this.state.jwtApi.getLocalStorageToken() !== undefined) {
            isLoggedIn = true;
        }

        return (
            <Navbar bg="dark" variant="dark" expand="lg">
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="ml-auto">
                        <Nav.Link href="/">home</Nav.Link>
                        <Nav.Link href="/about">about</Nav.Link>

                        {isLoggedIn ? (
                            <NavDropdown title={login.email} id="basic-nav-dropdown" >
                                <NavDropdown.Item href="/user">account</NavDropdown.Item>
                                <NavDropdown.Divider />
                                <NavDropdown.Item onClick={() => this.logout()}>logout</NavDropdown.Item>
                            </NavDropdown>
                        ) : (
                                <Nav.Link href="/log">login</Nav.Link>
                            )}
                    </Nav>
                </Navbar.Collapse>
            </Navbar>
        )
    }
}
export default withRouter(NavigationMenu);