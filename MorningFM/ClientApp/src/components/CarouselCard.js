import React from 'react';
import { Card, CardImg, CardBody, CardSubtitle, CardTitle, CardImgOverlay } from 'reactstrap';
import checkImg from '../assets/check-mark.png';

export class CardCarouselData {
    constructor(title, subtitle, imageSrc, altCaption) {
        this.imageSrc = imageSrc;
        this.altCaption = altCaption;
        this.title = title;
        this.subtitle = subtitle;
    }
}

export default class CarouselCard extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            isSelected: false
        }
        this.toggleSelect = this.toggleSelect.bind(this);
    }

    toggleSelect() {
        this.setState({ isSelected: !this.state.isSelected }, () => {
            if (typeof this.props.onChange === 'function') {
                this.props.onChange(this.state.isSelected, this.props.index, );
            }
        });       
    }

    render() {
        return (<Card className="mfm-carousel-card " outline color={this.state.isSelected ? "primary": ""}
            style={{ maxWidth: this.props.maxWidth }}
            onClick={this.toggleSelect}>
            <CardImg top width="100%" src={this.props.imageSrc} alt={this.props.altCaption} />
            <CardBody>
                <CardTitle>{this.props.title}</CardTitle>
                <CardSubtitle>{this.props.subtitle}</CardSubtitle>
            </CardBody>
            <CardImgOverlay className="d-flex justify-content-end flex-column">
                <div className={"mfm-carousel-oval " + (this.state.isSelected ? "selected": "")}>
                    <img src={checkImg} style={{ height: "15px", width: "20px" }} />
                </div>
            </CardImgOverlay>
        </Card>
        );
    }
}