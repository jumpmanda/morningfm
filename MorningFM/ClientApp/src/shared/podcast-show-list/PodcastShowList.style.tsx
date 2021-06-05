import { css } from '@emotion/react';
import styled from '@emotion/styled';

export interface ListRowProps{
    isSelected: boolean; 
}

export const ListRowSelected = (props: ListRowProps) => css({
    'background-color': props.isSelected ? '#e4e9f2': 'transparent',
    border: props.isSelected ? '1px solid #6498FF': 'none'
}); 

export const ListLayout = styled.div`
    ${ListRowSelected} 
`;

export const ListRow = styled.div`
    display: flex; 
    align-items: center;
    padding: 5px;
    & > * {        
        margin-right: 2rem;        
    }   

    @media only screen and (max-width: 768px) {
        > img {
            height: 50px; 
            width: 50px; 
        }

        > h5 {
            width: 250px;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }
    }

    @media only screen and (min-width: 767px) {
        > img {
            height: 75px; 
            width: 75px; 
        }
    }

`;

 