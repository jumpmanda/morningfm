import React from 'react'
import { render } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import { Context as ResponsiveContext } from 'react-responsive'
import { MainPage } from 'pages/main/MainPage'; 
import { useAuth0 } from '@auth0/auth0-react';
import Media from 'react-media'; 

// create a dummy user profile
const user = {
  email: 'johndoe@me.com',
  email_verified: true,
  sub: 'google-oauth2|2147627834623744883746',
};

jest.mock('@auth0/auth0-react'); 

test('loads MainPage', () => {
   //@ts-ignore
   useAuth0.mockReturnValue({
    isAuthenticated: true,    
    user,
    logout: jest.fn(),
    loginWithRedirect: jest.fn()
  }); 

  const { container: desktop, queryAllByRole } = render(
    <ResponsiveContext.Provider value={{ width: 769 }}>
      <MainPage/>
    </ResponsiveContext.Provider>
  ); 
  
    expect(queryAllByRole('heading')[0]).toHaveTextContent('MorningFM');  
  }); 

  test('loads MainPage isLoading', ()=>{
    //@ts-ignore
    useAuth0.mockReturnValue({
      isAuthenticated: false,
      isLoading: true,
      user,
      logout: jest.fn(),
      loginWithRedirect: jest.fn(),
      error: {message: 'whoopsies'}
    }); 

    const { container: desktop } = render(
      <ResponsiveContext.Provider value={{ width: 769 }}>
        <MainPage/>
      </ResponsiveContext.Provider>
    ); 
      expect(desktop).toHaveTextContent('Loading');  
  });

  // test('loads MainPage isError', ()=>{
  //   //@ts-ignore
  //   useAuth0.mockReturnValue({
  //     isAuthenticated: false,
  //     isLoading: false,
  //     isError: true,
  //     user,
  //     logout: jest.fn(),
  //     loginWithRedirect: jest.fn(),
  //     error: {message: 'whoopsies'}
  //   }); 

  //   const { container: desktop } = render(
  //     <ResponsiveContext.Provider value={{ width: 769 }}>
  //       <MainPage/>
  //     </ResponsiveContext.Provider>
  //   ); 
  //     expect(desktop).toHaveTextContent('Oops... whoopsies');  
  // }); 