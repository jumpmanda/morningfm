import React from 'react'; 
import {Default, Tablet, Mobile} from 'pages/shared/media-queries/MediaQueries'; 
import { MainLayout, MainFooter, MobileLayout, Button } from 'pages/main/MainPage.style';
import { Page } from 'pages/shared/page/Page';
import radioImg from 'assets/radio.svg';
import { useAuth0 } from '@auth0/auth0-react';

export const redirect = (isAuthenticated: boolean, authenticateCallback: Function) => {
    var userSession = sessionStorage.getItem('mfmSession');
    // if (userSession != undefined && userSession != null) {
    //     window.location = '/home?token=' + userSession;
    //     return;
    // }
    if (!isAuthenticated) {
        authenticateCallback();         
    }    
}

export const MainPage = () => {

    const {
        isLoading,
        isAuthenticated,
        error,
        user,
        loginWithRedirect,
        logout,
    } = useAuth0();

    if (isLoading) {
        return <div>Loading...</div>;
    }
    if (error) {
        return <div>Oops... {error.message}</div>;
    }

    return (
        <Page>           
            <Default>
                <MainLayout>                                    
                    <section>
                        <h1>MorningFM</h1>
                        <h1>Personalized radio to start your morning.</h1>
                        <p>Connect with Spotify to queue up your daily podcast episodes, with recommended music in between.</p>
                        <Button onClick={() => {redirect(isAuthenticated, loginWithRedirect);}} >Get Started</Button>
                    </section>
                    <section>
                        <img src={radioImg} alt={"radio"}/>    
                    </section>                                      
                </MainLayout>                                
            </Default>   
            <Mobile>
                <MobileLayout>
                    <section>
                        <img src={radioImg} alt={"radio"}/>    
                    </section> 
                    <section>
                        <h1>MorningFM</h1>
                        <h1>Personalized radio to start your morning.</h1>
                        <p>Connect with Spotify to queue up your daily podcast episodes, with recommended music in between.</p>
                        <Button>Get Started</Button>
                    </section>                    
                </MobileLayout>                
            </Mobile>                                                                               
        </Page>                                           
    ); 
}; 