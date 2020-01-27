import React, { Component } from 'react';
import { Navbar, Nav, NavDropdown } from 'react-bootstrap';
import '../styles/user_account_styles.css';
import { withRouter } from 'react-router-dom';

class NavigationMenu extends Component {
   
    constructor(props) {
        super(props);
        this.logout = this.logout.bind(this);
    }

    logout = () => {
        localStorage.removeItem("your-jwt");
        this.props.history.push('/log');
    }

    render() {
        let jwt_decode = require('jwt-decode');
        let login;
        if (localStorage.getItem("your-jwt")) {
            login = jwt_decode(localStorage.getItem("your-jwt"));            
        }
        let isLoggedIn = false;

        if (localStorage.getItem("your-jwt") !== null &&
            localStorage.getItem("your-jwt") !== undefined) {

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
                                <NavDropdown.Item onClick={() => this.logout() }>logout</NavDropdown.Item>
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