import React, { useState, useEffect } from 'react'; 
import { ListGroup, ListGroupItem } from 'reactstrap';
import { PodcastShowListRow } from './PodcastShowListRow'; 
import { PodcastShows } from '../../../models/PodcastShows'; 

export interface PodcastShowListProps{
  shows: PodcastShows[]; 
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
    var result: SelectedShows = {...selectedShows, [index]: isSelected}; 
    setSelectedShow(result);   
  }; 

  return (<ListGroup>
    {props.shows.map((show, index)=>{
      return (
      show && 
      <ListGroupItem key={index}>
          <PodcastShowListRow index={index} showName={show.name} showImageUrl={show.images[0].url} onToggle={onChange}/>
      </ListGroupItem>);
    })}
  </ListGroup>);  
}; 