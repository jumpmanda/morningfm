﻿import React from 'react';
import { Button } from 'reactstrap';
import CarouselCard, { CardCarouselData } from './CarouselCard';

export default class Carousel extends React.Component {
    constructor(props){
        super(props);
        this.state = {
            elements: null,
            left: 0,
            currentElementIndex: 1,
            selectedCards: []
        };

        this.slide = this.slide.bind(this);
    }

    componentDidMount() {
        if (this.props.cardData == null || this.props.cardData == undefined) {
            return;
        }
        this.setState({ elements: this.getCardElements() });
    }

    getCardElements() {
        let cards = []; 
        for (let i = 0; i < this.props.cardData.length; i++) {
            cards.push(<CarouselCard
                imageSrc={this.props.cardData[i].imageSrc}
                title={this.props.cardData[i].title}
                subtitle={this.props.cardData[i].subtitle}
                altCaption={this.props.cardData[i].altCaption}
                index={i}
                onChange={(isSelected, index) => { this.updateSelectedCards(index); }}
            />); 
        }
        return cards; 
    }

    updateSelectedCards(index) {
        var selectedCards = this.state.selectedCards;
        var existingIndex = selectedCards.indexOf(index);
        if (existingIndex == -1) {
            selectedCards.push(index);
        }
        else {
            selectedCards.splice(existingIndex, 1);
        }
        this.setState({ selectedCards: selectedCards }, () => {
            if (typeof this.props.onChange === 'function') {
                this.props.onChange(this.state.selectedCards);
            }
        });
    }

    slide(delta) {
        this.setState({ left: this.state.left + delta });
    }

    getElementCenterPosition(isPrevious) {
        let centerPoint = null;         

        let elementRef = this[`element-${this.state.currentElementIndex}`];
       
        let boundingBox = elementRef.getBoundingClientRect();        

        this.slide(isPrevious ? boundingBox.width : -boundingBox.width);
        
        return centerPoint; 
    }

    createItems(elements) {
        let totalElementCount = 0;
        const items = [<div ref={input => { this["element-0"] = input; } } id="mfm-carousel-start" className="mfm-carousel-item item-0"></div>];

        if (this.state.elements != null && this.state.elements != undefined) {
            for (const [index, value] of this.state.elements.entries()) {
                items.push(<div ref={input => { this[`element-${index + 1}`] = input; }} className={`mfm-carousel-item item-${index + 1}`}>{value}</div>);
                totalElementCount++;
            }
            items.push(<div id="mfm-carousel-end" ref={input => { this[`element-${totalElementCount + 1}`] = input; }} className={`mfm-carousel-item item-${totalElementCount + 1}`}></ div>);
        }

        return items; 
    }

    isIndexInBounds(currentIndex, isPrevious) {       
        if (this.state.elements != null) {
            let MAX = this.state.elements.length; 
            return ((isPrevious && currentIndex - 1 >= 1) || (!isPrevious && currentIndex + 1 < MAX + 1)); 
        }
        return false; 
    }

    render() {
        const items = this.createItems(this.state.elements);             
        return <div className="mfm-carousel-container">
            <h1>{ this.props.title }</h1>
            <Button id="mfm-carousel-previous-btn" onClick={() => {
                if (!this.isIndexInBounds(this.state.currentElementIndex, true)) {
                    return;
                }
                this.setState({ currentElementIndex: this.state.currentElementIndex - 1 }, () => { this.getElementCenterPosition(true); });                
            }}>Previous</Button>
            <Button id="mfm-carousel-next-btn" onClick={() => {
                if (!this.isIndexInBounds(this.state.currentElementIndex, false)) {
                    return;
                }
                this.setState({ currentElementIndex: this.state.currentElementIndex + 1 }, () => { this.getElementCenterPosition(false); });
            }}>Next</Button>
            <div className="mfm-carousel-element-strip" style={{left: this.state.left + "px"}}>               
                {items}                
            </div>
        </div>;
    }
}
