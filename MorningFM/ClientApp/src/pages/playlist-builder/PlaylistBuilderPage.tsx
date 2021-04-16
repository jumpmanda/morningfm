import React, { useState, useEffect } from 'react'; 
import { useLocation } from 'react-router';
import { useAuth0 } from '@auth0/auth0-react';  
import { PodcastShows } from '../../models/PodcastShows'; 
import { PodcastShowList } from '../shared';
import { Page } from 'pages/shared/page/Page';
import {Default} from 'pages/shared/media-queries/MediaQueries'; 

export interface PodcastShowPayload {
    addedAt: string; 
    show: PodcastShows; 
}

export const PlaylistBuilderPage = () => {

    const location = useLocation(); 
    const sessionToken = location.search.substring(location.search.indexOf('?token=') + 7, location.search.indexOf('?token=') + 7 + 36); 
    const [podcastShows, setPodcastShows] = useState<PodcastShows[]>(); 
    const [token, setToken] = useState(sessionToken); 
    const { user } = useAuth0();

    //TODO: Left off with auth0 being wierd and not recognizing authentication until loading it up a second time.... strange.

    useEffect(()=>{
        getUserShows(); 
    }, []);

    const getUserShows = () => {
        fetch("api/library/" + token + "/shows")
            .then(res => {
                res.json().then(json => {
                   var shows: PodcastShows[] = json.map((item: PodcastShowPayload) => { return item.show; }); 
                   setPodcastShows(shows);
                }
                );
            },
                err => {
                    //this.setState({ isError: true, errorMessage: err });
                    console.log(err);
                });
    }

    return (<div>
        <h1>Playlist Builder</h1>
        <p>Select the shows you would like to have queue'ed up in your new playlist:</p>
        <p>These are shows that you've already followed.</p>
        <PodcastShowList shows={podcastShows ?? []} />
    </div>); 
}; 

