import React, { useState, useEffect } from 'react'; 
import { useHistory, useLocation } from 'react-router';
import { PodcastShows } from '../../models/PodcastShows'; 
import { PodcastShowList } from 'shared/podcast-show-list/PodcastShowList';
import { PlaylistBuilderLayout } from './PlaylistBuilderPageStyle';
import { Button, Alert } from 'reactstrap';
import { AddCircleOutline } from '@material-ui/icons'; 
import apiAuth from '../../auth/AuthUtils';
import { usePodcastShows } from '../../hooks/usePodcastShows/usePodcastShows';
import { useAccessToken } from '../../hooks/useAccessToken';

export const PlaylistBuilderPage = () => {

    const location = useLocation(); 
    const history = useHistory(); 
    const sessionToken = location.search.substring(location.search.indexOf('?token=') + 7, location.search.indexOf('?token=') + 7 + 36);   
    const [selectedShows, setSelectedShows] = useState<PodcastShows[]>([]); 
    const { accessToken } = useAccessToken(); 
    const [isWarningDisplayed, setIsWarningDisplayed] = useState(false); 
    const [isSelectionValid, setIsSelectionValid] = useState(false); 
    const { podcastShows } = usePodcastShows(sessionToken, accessToken); 

    useEffect(()=>{
        let fetchSession = async () => {
            if((!sessionToken || sessionToken === "") && accessToken !== ""){               
                apiAuth(accessToken);
            }  
        };       
        fetchSession(); 
    }, [sessionToken, accessToken]); 
   
    const createPlaylist = async (shows: PodcastShows[]) => {       

        let showIds: string[] = shows.map((show)=>{ return show.id; }); 

        let requestOptions = {
            method: 'POST',             
            headers: { 'Content-Type': 'application/json', Accept: "application/json", Authorization: `Bearer ${accessToken}` },
            body: JSON.stringify({ showIds: showIds })
        }; 
        
        fetch("api/library/" + sessionToken + "/recommended-playlist", requestOptions)
            .then(res => {
                res.json().then(json => {
                   console.log(json); 
                   let id = json.playlistId; 
                   history.push(`playlist?token=${sessionToken}&id=${id}`); 
                }
                );
            },
                err => {
                    //this.setState({ isError: true, errorMessage: err });
                    console.log(err);
                });
    }; 

    const onSelectedShowsChange = (shows: PodcastShows[]) => {
        setSelectedShows(shows); 

        if(!shows || shows.length === 0){
            setIsWarningDisplayed(true); 
            setIsSelectionValid(false); 
        }
        else{
            setIsWarningDisplayed(false); 
            setIsSelectionValid(true);
        }       
    }; 

    return (
        <PlaylistBuilderLayout>
        <h1>Playlist Builder</h1>
        <p>Select the shows you would like to have queue'ed up in your new playlist:</p>
        <p>These are shows that you've already followed.</p>
        {isWarningDisplayed && <Alert color="warning">Please select podcast shows.</Alert>}
        <PodcastShowList shows={podcastShows ?? []} onChange={onSelectedShowsChange} />
        <Button color="primary" key={selectedShows.length} onClick={()=>{ createPlaylist(selectedShows);}} disabled={!isSelectionValid}>
            <AddCircleOutline/>
            Create
        </Button>
    </PlaylistBuilderLayout>); 
}; 

