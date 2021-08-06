import React, { useState, useEffect } from 'react'; 
import { Input } from 'reactstrap';
import { ListRow } from './PodcastShowList.style';

export interface PodcastShowListRowProps {
    index: number;
    showName: string;
    showImageUrl: string;
    onToggle?: ((index: number, isSelected: boolean) => void);
}

export const PodcastShowListRow = (props: PodcastShowListRowProps) => {
    
    const [isSelected, setSelected] = useState<boolean>(false); 

    useEffect(()=>{

    }); 

    const toggleSelection = () => {
        setSelected(prev => !prev); 
        if(props.onToggle){
            props.onToggle(props.index, !isSelected);
        }
    };

    return (
        <ListRow key={props.index} onClick={toggleSelection} >         
          <Input addon type="checkbox" aria-label="Checkbox for following text input" checked={isSelected} onClick={toggleSelection}/> 
          <img src={props.showImageUrl} alt={props.showName + ' podcast show cover'}/>
          <h5 title={props.showName}>{props.showName}</h5>                  
        </ListRow>     
    );
};