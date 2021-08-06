import React from 'react'; 
import { MainLayout, Button } from 'pages/main/MainPage.style';
import { Page } from 'shared/page/Page';
import radioImg from 'assets/radio.svg';
import { useAuth0 } from '@auth0/auth0-react';
import apiAuth from '../../auth/AuthUtils';

export const MainPage = () => {

    const {
        isLoading,
        isAuthenticated,
        error,
        loginWithRedirect        
    } = useAuth0();

    const { getAccessTokenSilently } = useAuth0();

    const redirect = async (isAuthenticated: boolean, authenticateCallback: Function) => {        
        if (!isAuthenticated) {
            authenticateCallback();         
        }  
        else{        
            const token = await getAccessTokenSilently();
            apiAuth(token);
        }  
    }

    if (isLoading) {       
        return <div>Loading...</div>;
    }
    if (error) {
        return <div>Oops... {error.message}</div>;
    }

    return (
        <Page>                      
            <MainLayout>
            <section>
                <img src={radioImg} alt={"radio"}/>    
            </section> 
            <section>  
                <h1>MorningFM</h1>
                <h1>Personalized radio to start your morning.</h1>
                <p>Connect with Spotify to queue up your daily podcast episodes, with recommended music in between.</p>
                <Button onClick={() => {redirect(isAuthenticated, loginWithRedirect);}} >Get Started</Button>
            </section>  
            </MainLayout>                                                                                                                                                                                                            
        </Page>                                           
    ); 
}; 