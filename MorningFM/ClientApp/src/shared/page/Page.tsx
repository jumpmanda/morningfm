import React from 'react'; 
import { PageLayout } from './Page.style';

export interface PageProps{
}

export const Page: React.FunctionComponent<PageProps> = (props) => {
    return (<div>
        <PageLayout>        
        {props.children}      
        </PageLayout>
    </div>); 
}; 