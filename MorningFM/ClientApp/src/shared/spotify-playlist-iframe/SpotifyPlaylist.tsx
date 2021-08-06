import React from 'react'; 

export interface ISpotifyPlaylistProps {
    playlistId: string; 
    onLoad?: () => void; 
}

export const SpotifyPlaylist = ({playlistId, onLoad}: ISpotifyPlaylistProps) => {
    return (<div>
        <iframe key={playlistId} title={`mfm-spotify-playlist-iframe-${playlistId}`} onLoad={onLoad} 
            src={"https://open.spotify.com/embed?uri=spotify:playlist:" + playlistId} 
            width="300" 
            height="380" 
            frameBorder="0"></iframe>
    </div>); 
}; 