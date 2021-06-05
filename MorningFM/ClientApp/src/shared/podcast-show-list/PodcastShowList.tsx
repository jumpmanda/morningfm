import React, { useState, useEffect } from 'react'; 
import { ListGroup, ListGroupItem } from 'reactstrap';
import { PodcastShowListRow } from './PodcastShowListRow'; 
import { PodcastShows } from 'models/PodcastShows'; 
import { ListLayout } from './PodcastShowList.style';

export interface PodcastShowListProps{
  shows: PodcastShows[]; 
  onChange?: (selectedShows: PodcastShows[]) => void; 
}

export interface SelectedShows {
  [key: number]: boolean; 
}

export const PodcastShowList = (props: PodcastShowListProps) => {

  const [selectedShows, setSelectedShow] = useState<SelectedShows>(); 

  useEffect(()=>{
    var selected: SelectedShows = {};   
    props.shows.forEach((shows,index)=>{
      selected[index] = false; 
    }); 
    setSelectedShow(selected);
  }, []); 

  const onChange = (index: number, isSelected: boolean) => {
    let result: { [key: number]: boolean} = {}; 
    if(isSelected){
      result = {...selectedShows, [index]: isSelected}; 
      setSelectedShow(result);  
    } else {              
      if(selectedShows){        
        result = {}; 
        Object.keys(selectedShows)
        .filter(key => parseInt(key) !== index).forEach(key => {
          result[parseInt(key)] = true; 
        });         
        setSelectedShow(result); 
      }       
    }

    if(props.onChange){
      let selectedShows = []; 
      for(let key in result){
        selectedShows.push(props.shows[key]); 
      }
      props.onChange(selectedShows); 
    }

  }; 

  return (<ListGroup>
    {props.shows.map((show, index)=>{
      return (
      show && 
      <ListLayout key={0} isSelected={( selectedShows && selectedShows[index] ) || false}>
          <ListGroupItem key={index}>
              <PodcastShowListRow index={index} showName={show.name} showImageUrl={show.images[0].url} onToggle={onChange}/>
          </ListGroupItem>
      </ListLayout>);
    })}
  </ListGroup>);  
}; 