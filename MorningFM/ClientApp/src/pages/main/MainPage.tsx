import React from 'react'; 
import {Default, Mobile} from 'pages/shared/media-queries/MediaQueries'; 
import { MainLayout, MobileLayout, Button } from 'pages/main/MainPage.style';
import { Page } from 'pages/shared/page/Page';
import radioImg from 'assets/radio.svg';
import { useAuth0 } from '@auth0/auth0-react';


export const MainPage = () => {

    const {
        isLoading,
        isAuthenticated,
        error,
        user,
        loginWithRedirect,
    } = useAuth0();

    const redirect = (isAuthenticated: boolean, authenticateCallback: Function) => {        
        if (!isAuthenticated) {
            authenticateCallback();         
        }  
        else{
            window.location.href = "/test"; 
            console.log(user);             
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