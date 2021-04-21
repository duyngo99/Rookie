import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import {
    Collapse,
    Navbar,
    NavbarToggler,
    NavbarBrand,
    Nav,
    NavItem,
    NavLink,
  } from 'reactstrap';
import { Badge } from 'reactstrap'

export default function Header(props) {
    const [isOpen, setIsOpen] = useState(false);

  const toggle = () => setIsOpen(!isOpen);
    return (
        <div>
        <Navbar color="light" light expand="md">
            <NavbarBrand ><Link to={"/"}>React Admin side</Link></NavbarBrand>
            <NavbarToggler onClick={toggle} />
        <Collapse isOpen={isOpen} navbar>
                <Nav className="mr-auto" navbar>
                    <NavItem>
                    <NavLink><Link to="/product">Product</Link></NavLink>
                    </NavItem>
                    <NavItem>
                    <NavLink><Link to="/category">Category</Link></NavLink>
                    </NavItem>
                </Nav>
        </Collapse>
      </Navbar>
        </div> 
    )
}


