import React, { useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink,
  Button,
} from 'reactstrap';
import { Badge } from 'reactstrap'

export default function Header(props) {
  const [isOpen, setIsOpen] = useState(false);
  const history = useHistory()
  const toggle = () => setIsOpen(!isOpen);
  const btnProduct = () => {
    history.push('/product/')
  }

  const btnCategory = () => {
    history.push('/category/')
  }

  const btnUser = () => {
    history.push('/user/')
  }
  const btnSignin = () => {
    history.push('/login/')
  }

  return (
    <div>
      <Navbar color="light" light expand="md" style={{ display: "flex", flexDirection: "column", flexWrap: "wrap" }}>
        <NavbarBrand >React admin side</NavbarBrand>
        <NavbarToggler onClick={toggle} />
        <Collapse isOpen={isOpen} navbar>
          <Nav className="mr-auto" navbar>
            <NavItem >
              <NavLink ><Button onClick={btnProduct} color="primary">Product</Button></NavLink>
            </NavItem>
            <NavItem>
              <NavLink><Button onClick={btnCategory} color="primary">Category</Button></NavLink>
            </NavItem>
            <NavItem>
              <NavLink><Button onClick={btnUser} color="primary">User</Button></NavLink>
            </NavItem>
            <NavItem>
              <NavLink><Button onClick={btnSignin} color="primary">Signin</Button></NavLink>
            </NavItem>
          </Nav>
        </Collapse>
      </Navbar>
    </div>
  )
}


