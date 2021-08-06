import React, { useState } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import './NavMenu.css';
import AuthenticationButton from '../auth-button/AuthenticationButton';
import { ProfileIcon } from 'shared/profile-icon/ProfileIcon'; 
import { useAuth0 } from '@auth0/auth0-react'; 

export const NavMenu = () => {
  const [collapsed, setCollapsed] = useState(true); 
  const { user } = useAuth0(); 

    return (
      <header className="mfm-header-navbar">
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow" light>
          <Container>            
            <NavbarToggler onClick={() => setCollapsed(!collapsed)} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!collapsed} navbar>
              <ul className="navbar-nav flex-grow" style={{ 'align-items': 'center' }}>
                <NavItem>
                  <NavLink href={"/"}>Home</NavLink>                  
                </NavItem>
                <NavItem>
                <NavLink  href={"/build"}>Build</NavLink>
                </NavItem>
                <NavItem>
                <NavLink  href={"/about"}>About</NavLink>
                </NavItem>
                <NavItem>
                  <AuthenticationButton/>
                </NavItem>
                {user?.picture && 
                <NavItem>
                <ProfileIcon src={user?.picture}/>
              </NavItem>}                
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
}
