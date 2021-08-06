import { useEffect, useState, useCallback } from 'react';
import { PodcastShows } from '../../models/PodcastShows'; 


export interface PodcastShowPayload {
    addedAt: string; 
    show: PodcastShows; 
}

export const usePodcastShows = (sessionToken: string, accessToken: string) => {
    const [podcastShows, setPodcastShows] = useState<PodcastShows[]>(); 
    const getUserShows = useCallback(async () => {
        if(sessionToken && accessToken && sessionToken !== "" && accessToken !== ""){
            fetch("api/library/" + sessionToken + "/shows", {
                headers: { Accept: "application/json", Authorization: `Bearer ${accessToken}` },
              })
                .then(res => {
                    res.json().then(json => {                  
                       var shows: PodcastShows[] = json.map((item: PodcastShowPayload) => { return item.show; }); 
                       setPodcastShows(shows);
                    }
                    );
                },
                    err => {                   
                        console.log(err);
                    });
        }        
    }, [sessionToken, accessToken]); 

    useEffect(()=>{
        getUserShows(); 
    }, [getUserShows]); 

    return { podcastShows };
}; 