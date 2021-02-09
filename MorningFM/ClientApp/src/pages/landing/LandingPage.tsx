import React from 'react'; 
import { LandingLayout } from './LandingPage.style';
import landingGif from "assets/cool-loading2.gif";

export const LandingPage = () => {
    
    const landingHeader = "Loading...."; 
    
    return(
        <LandingLayout>       
            <img src={landingGif} alt={"Loading Gif"}/>
            <h3>{landingHeader}</h3>
        </LandingLayout>
    );
}; 