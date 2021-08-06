import styled from '@emotion/styled';

export const MainLayout = styled.div`    
    display: flex;     
    align-items: center;
    padding: 10rem;   
    min-width: 420px; 
    height: 100%;   
    flex-wrap: wrap;  

    section {
        width: 50%; 
        flex-grow: 2;                        
        text-align: center;

        > img {                               
            min-height: 400px;
            min-width: 400px; 
            max-height: 600px; 
            max-width: 600px; 
        }
    }

    @media only screen and (max-width: 1110px) {
        height: auto;
        flex-direction: column;
        margin: 0; 
        padding: 0; 
        min-width: 0;
        section {
            > img{
                height: 200px;
                width: 200px;
                min-height: 100px;
                min-width: 100px; 
                max-height: 250px; 
                max-width: 250px; 
            }
        }  
    }

`; 

export const Button = styled.button`
    outline: none !important;     
    margin-top: 1rem;
    width: 200px;
    height: 60px;
    background-color: #E3E6EC;    
    box-shadow: 12px 12px 20px #D1D9E6, -12px -12px 20px #FFFFFF; 
    border-radius: 80px;
    cursor: pointer;    
    border: none;

    &:active{
        box-shadow: inset 10px 10px 30px #D1D9E6, inset -10px -10px 30px #FFFFFF; 
        background-color: #E6E9EF; 
    }
`; 