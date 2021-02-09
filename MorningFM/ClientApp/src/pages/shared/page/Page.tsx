import React from 'react'; 
import { PageLayout, PageMobileLayout } from './Page.style';
import { NavMenu } from 'pages/shared/nav-menu/NavMenu';
import {Default, Tablet, Mobile} from 'pages/shared/media-queries/MediaQueries'; 

export interface PageProps{
}

export const Page: React.FunctionComponent<PageProps> = (props) => {
    return (<div>
        <PageLayout>
        <NavMenu></NavMenu>
        {props.children}      
        </PageLayout>
    </div>); 
}; 