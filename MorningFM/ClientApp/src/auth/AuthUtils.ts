export const apiAuth = async (accessToken: string) => {
    const init: RequestInit = { 
       mode: 'cors', 
       headers: { 'Access-Control-Allow-Origin':'*', Accept: "application/json", Authorization: `Bearer ${accessToken}` }  
      }; 

    await fetch("api/authentication", init)
    .then(res => res.json())
    .then(async data => {        
       var url = data.redirectUri; 
       window.location = url; 
    });
  }; 

  export default apiAuth; 