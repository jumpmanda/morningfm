import React, { Component, useContext } from 'react';
import { Card, CardImg, CardBody, CardTitle, Container, Row, Col } from 'reactstrap';

class PodcastShows {
    constructor(id, name, images) {
        this.id = id; 
        this.name = name;
        this.images = images;
    }
}

export class UserHome extends React.Component{
    constructor(props){
        super(props);                 
        this.state = {
            sessionToken: '',
            isError: false,
            podcastShows: []
        };
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
        fetch("api/library/" + this.state.sessionToken + "/top-tracks")
            .then(res => {
                res.json().then(json => {                   
                    //var shows = json.map(item => { return new PodcastShows(item.show.id, item.show.name, item.show.images) });                  
                    var shows = json.map(item => { return new PodcastShows(item.id, item.name, item.album.images) });                  
                    this.setState({ podcastShows: shows }); 
                    }
                    );
                },
                err => {
                    this.setState({ isError: true, errorMessage: err });
                    console.log(err);
                });
        }

    render() {
        const shows = this.state.podcastShows;
        const items = []; 
        for (const [index, value] of shows.entries()) {
            items.push(<Col>
                <Card className="mfm-show-card" key={index}>
                    <CardImg top width="100%" src={value.images[0].url} alt="Show Image" />
                    <CardBody>
                        <CardTitle>{value.name}</CardTitle>
                    </CardBody>
                </Card>
            </Col>); 
        }
        return <div>
            <h1>Here are all the silly little songs you listen to</h1>      
            <Container>
                <Row>
                    {items}  
                </Row>                   
            </Container>           
        </div>;
    }
}