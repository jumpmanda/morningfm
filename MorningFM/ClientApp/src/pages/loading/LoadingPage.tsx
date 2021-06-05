import React from 'react'; 
import { LoadingLayout } from 'pages/loading/LoadingPage.style';
import landingGif from "assets/cool-loading2.gif";

export const LoadingPage = () => {
    
    const landingHeader = "Loading...."; 
    
    return(
        <LoadingLayout>       
            <img src={landingGif} alt={"Loading Gif"}/>
            <h3>{landingHeader}</h3>
        </LoadingLayout>
    );
}; 