import React, { Component, useContext } from 'react';
import { Card, CardImg, CardBody, CardTitle, Container, Row, Col, Button } from 'reactstrap';

class PodcastShows {
    constructor(id, name, images) {
        this.id = id; 
        this.name = name;
        this.images = images;
    }
}

export class PlaylistCreate extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            sessionToken: '',
            isError: false,
            podcastShows: [],
            selectedShows: [],
            generatedPlaylist: '',
            isLoading: true,
            loadingMessage: 'Starting up.... just a sec...',
            isFrameLoaded: false
        };
        this.handleShowSelectionSubmit = this.handleShowSelectionSubmit.bind(this);
    }

    async getSessionFromUrl() {
        var index = this.props.location.search.indexOf('?token=');
        var sessionToken = this.props.location.search.substring(index + 7, index + 7 + 36);
        return sessionToken;
    }

    async componentDidMount() {
        var sessionToken = await this.getSessionFromUrl();
        this.setState({ sessionToken: sessionToken });
        sessionStorage.setItem('mfmSession', sessionToken);
        this.getUserShows();
    }

    getUserShows() {
        fetch("api/library/" + this.state.sessionToken + "/shows")
            .then(res => {
                res.json().then(json => {
                    var shows = json.map(item => { return new PodcastShows(item.show.id, item.show.name, item.show.images) });
                    this.setState({ podcastShows: shows, isLoading: false });
                }
                );
            },
                err => {
                    this.setState({ isError: true, errorMessage: err });
                    console.log(err);
                });
    }

    toggleShowCard(e, index) {
        var selectedShows = this.state.selectedShows;
        var existingIndex = selectedShows.indexOf(index);
        if (existingIndex == -1) {
            selectedShows.push(index);
        }
        else {
            selectedShows.splice(existingIndex, 1);
        }
        this.setState({ selectedShows: selectedShows });
    }

    handleShowSelectionSubmit() {
        this.setState({ isLoading: true });
        this.setState({ loadingMessage: 'Hacking, building, grabbing your playlist...' });

        var selectedShowIds = this.state.selectedShows.map((value) => { return this.state.podcastShows[value].id; });
        console.log(selectedShowIds);

        //todo make api call to build playlist and show results to user in ui
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ showIds: selectedShowIds })
        };

        fetch("api/library/" + this.state.sessionToken + "/recommended-playlist", requestOptions)
            .then(res => {
                res.json().then(json => {
                    console.log(json);
                    this.setState({ generatedPlaylist: json.playlistId, isLoading: false, loadingMessage: 'Grabbing preview window...' });
                }
                );
            },
                err => {
                    this.setState({ isError: true });
                    this.setState({ isLoading: false, loadingMessage: '' });
                    console.log(err);
                });

    }

    render() {        
        const shows = this.state.podcastShows;
        const items = []; 
        for (const [index, value] of shows.entries()) {
            items.push(<Col>
                <Card className={`mfm-show-card ${this.state.selectedShows.indexOf(index) == -1 ? '' : 'selected-card'}`} key={index} onClick={(e) => this.toggleShowCard(e, index)} id={"card-" + index}>
                    <CardImg top width="100%" src={value.images[0].url} alt="Show Image" />
                    <CardBody>
                        <CardTitle>{value.name}</CardTitle>
                    </CardBody>
                </Card>
            </Col>); 
        }

        const isLoading = this.state.isLoading;
        const needToBuild = this.state.generatedPlaylist == null || this.state.generatedPlaylist == "";
        let content;
        if (isLoading) {
            content = <div className="app-loading-content">
                <h3>{this.state.loadingMessage}</h3>
            </div>;
        }
        else if (needToBuild) {
            content = <Container>
                <Row><h1>Build your own morning radio show</h1></Row>
                <Row>    <h4>Podcasts</h4></Row>
                <Row>
                    <Col> <h5>Select shows to include: </h5>    </Col>
                    <Col><Button className="app-button-right" onClick={this.handleShowSelectionSubmit} color={this.state.selectedShows.length > 0 ? 'primary' : 'secondary'} disabled={this.state.selectedShows.length <= 0}>Generate</Button></Col>
                </Row>
                <Row>
                    {items }
                </Row>
            </Container>;
        }
        else {                   
            content = <div className="app-loading-content app-items-center">
                <h1>Playlist created!</h1>
                <h3 style={{ display: this.state.isFrameLoaded ? 'none' : 'block' }}>{this.state.loadingMessage}</h3>
                <iframe onLoad={() => { this.setState({isFrameLoaded: true}); }} src={"https://open.spotify.com/embed?uri=spotify:playlist:" + this.state.generatedPlaylist} width="300" height="380" frameBorder="0" allowtransparency="true"></iframe>
            </div>;          
        }
        return <div>
            {content}
        </div>;
    }
}