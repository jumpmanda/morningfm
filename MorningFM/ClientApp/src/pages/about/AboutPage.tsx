import React from 'react'; 
import { Page } from 'shared/page/Page';
import { AboutLayout } from './AboutPage.style';

export const AboutPage = () => {
    return (<Page>        
        <AboutLayout>
            <h1>About Us</h1>
            <p>This silly little app is built by internet citizen @jumpmanda_. </p>
        </AboutLayout>        
    </Page>); 
};