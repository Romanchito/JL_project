import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';
import { Navbar, Nav, Button } from 'react-bootstrap';

export class NavigationMenu extends Component {
    render() {
        let jwt_decode = require('jwt-decode');
        let login;
        if (localStorage.getItem("your-jwt")) {
            login = jwt_decode(localStorage.getItem("your-jwt"));
            console.log(login);
        }
        let isLoggedIn = false;

        if (localStorage.getItem("your-jwt") !== null &&
            localStorage.getItem("our-jwt") !== undefined) {

            isLoggedIn = true;

        }
        return (
            <Navbar expand="lg">
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav>
                        <NavLink className="d-inline p-2 bg-dark text-white" to="/">
                            Home
                    </NavLink >

                        <NavLink className="d-inline p-2 bg-dark text-white" to="/about">
                            About
                    </NavLink >
                        {isLoggedIn ? (

                            <NavLink className="d-inline p-2 bg-dark text-white" to="/user">
                                {login.email}
                            </NavLink>

                        ) : (
                                <NavLink className="d-inline p-2 bg-dark text-white" to="/log">
                                    <Button variant="success" to="log">Log in</Button>
                                </NavLink>
                            )}




                    </Nav>
                </Navbar.Collapse>
            </Navbar>
        )
    }
}