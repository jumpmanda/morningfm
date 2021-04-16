import React from 'react'; 
import { PageLayout, PageMobileLayout } from './Page.style';
import {Default, Tablet, Mobile} from 'pages/shared/media-queries/MediaQueries'; 

export interface PageProps{
}

export const Page: React.FunctionComponent<PageProps> = (props) => {
    return (<div>
        <PageLayout>        
        {props.children}      
        </PageLayout>
    </div>); 
}; 