import React, { useEffect } from 'react';
import { useAuth0 } from '@auth0/auth0-react'; 
import apiAuth from '../../auth/AuthUtils';

export const AuthCallbackPage = () => {
    const { getAccessTokenSilently }  = useAuth0();   

    useEffect(()=> {       
        let fetchToken = async () => {
            const token = await getAccessTokenSilently();
            apiAuth(token);
        };  
        fetchToken(); 
    }, [getAccessTokenSilently]);

    return (<div>
        Loading...
    </div>);
}; 