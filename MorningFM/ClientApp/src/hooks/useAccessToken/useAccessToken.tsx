import { useState, useEffect } from 'react';
import { useAuth0 } from '@auth0/auth0-react';  

export const useAccessToken = () => {
    const [accessToken, setAccessToken] = useState<string>(""); 
    const { getAccessTokenSilently } = useAuth0();

    useEffect(()=>{
        let fetchAccessToken = async ()=>{
            const accessToken = await getAccessTokenSilently();       
            setAccessToken(accessToken); 
        }; 
        fetchAccessToken(); 
    }, [getAccessTokenSilently]); 

    return { accessToken };
}; 