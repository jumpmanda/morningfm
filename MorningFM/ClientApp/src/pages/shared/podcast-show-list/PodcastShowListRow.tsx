import React, { useState, useEffect } from 'react'; 
import { Input } from 'reactstrap';
import { Image, ListRow, MobileImage } from './PodcastShowList.style';
import {Default, Tablet, Mobile} from 'pages/shared/media-queries/MediaQueries'; 

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
        <ListRow key={props.index} onClick={toggleSelection} isSelected={isSelected}>         
          <Input addon type="checkbox" aria-label="Checkbox for following text input" checked={isSelected} onClick={toggleSelection}/>                      
          <Default>
            <Image src={props.showImageUrl} alt={props.showName + ' podcast show cover'}/>
            <h5 title={props.showName}>{props.showName}</h5>            
          </Default>
          <Mobile>
            <MobileImage src={props.showImageUrl} alt={props.showName + ' podcast show cover'}/>
            <h5 className="truncate" title={props.showName}>{props.showName}</h5>            
          </Mobile>
        </ListRow>
    );
};