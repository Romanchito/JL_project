import React, {Component} from 'react';
import {NavLink} from 'react-router-dom';
import {Navbar, Nav, Button} from 'react-bootstrap';

export class NavigationMenu extends Component{
    render(){
        return(
            <Navbar expand="lg">
            <Navbar.Toggle aria-controls = "basic-navbar-nav"/>
            <Navbar.Collapse id ="basic-navbar-nav">
                <Nav>
                    <NavLink  className = "d-inline p-2 bg-dark text-white" to="/">
                        Home
                    </NavLink >

                    <NavLink  className = "d-inline p-2 bg-dark text-white" to="/about">
                        About
                    </NavLink >

                    <NavLink  className = "d-inline p-2 bg-dark text-white" to="/log">
                        <Button variant="success" to="log">Log in</Button>
                    </NavLink >
                </Nav>
            </Navbar.Collapse>
        </Navbar>
        )
    }
}