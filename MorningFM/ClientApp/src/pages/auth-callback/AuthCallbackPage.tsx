import React, { useEffect } from 'react';
import { useLocation, useHistory } from 'react-router'; 
import { useAuth0 } from '@auth0/auth0-react'; 

export const AuthCallbackPage = () => {
    const { user }  = useAuth0();  
    const location = useLocation();     
    const history = useHistory(); 

    useEffect(()=>{        
        var index = location.search.indexOf("code"); 
        var code = location.search.substring(index + 5); 
        redirect(code);         
    }, []);

    const redirect = async (code: string) => {
        const init: RequestInit = { mode: 'cors', headers: { 'Access-Control-Allow-Origin':'*' }  }; 

        await fetch("api/authentication?code=" + code, init)
        .then(res => res.json())
        .then(data => {        
           var url = data.redirectUri; 
           window.location = url; 
        });        
    }; 

    return (<div>
        hello from test!
        Name: {user?.name}
        Email: {user?.email}
    </div>);
}; 