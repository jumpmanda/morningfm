import { css } from '@emotion/react';
import styled from '@emotion/styled';

export const ProfileLayout = styled.div`
    display: flex;
    padding: 10px;
    height: 75px;   
    align-items: center;
    justify-content: center;
    // background-color: #ebf2fc;
    // border-radius: 25%;
    & > img {
        width: 25px;
        max-height: 100%;
        max-width: 100%;
        border-radius: 50%;
    }
    // & > svg {
    //     color: black; 
    //     transform: rotate(90deg);
    // }
`;