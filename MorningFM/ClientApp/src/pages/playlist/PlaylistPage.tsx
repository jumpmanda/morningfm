import React, {useState} from 'react'; 
import { useLocation } from 'react-router';
import { SpotifyPlaylist } from 'shared/spotify-playlist-iframe/SpotifyPlaylist';
import { PlaylistLayout } from './PlaylistPageStyle';

export const PlaylistPage = () => {

    const location = useLocation(); 
    const id = location.search.substring(location.search.indexOf('id=') + 3); 
    let pid = id.split("&")[0];
    const [playlistId] = useState(pid);     

    return (<PlaylistLayout>
        <h3>Welcome to your new playlist!</h3>
        <p>Check out your spotify library to see the new playlist added.</p>
        <SpotifyPlaylist playlistId={playlistId} onLoad={()=>{console.log('im loaded!');}}/>
    </PlaylistLayout>); 
}; 