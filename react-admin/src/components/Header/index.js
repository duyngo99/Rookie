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


  return (
    <div>
      <Navbar color="light" light expand="md" style={{ display: "flex", flexDirection: "column", flexWrap: "wrap" }}>
        <NavbarBrand style={{color:"black"}} >React admin side</NavbarBrand>
        <NavbarToggler onClick={toggle} />
        <Collapse isOpen={isOpen} navbar>
          <Nav className="mr-auto" navbar>
            <NavItem >
              <NavLink ><Button onClick={btnProduct} color="info">Product</Button></NavLink>
            </NavItem>
            <NavItem>
              <NavLink><Button onClick={btnCategory} color="info">Category</Button></NavLink>
            </NavItem>
            <NavItem>
              <NavLink><Button onClick={btnUser} color="info">User</Button></NavLink>
            </NavItem>

          </Nav>
        </Collapse>
      </Navbar>
    </div>
  )
}


