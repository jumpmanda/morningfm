import React from 'react';

import Carousel from './Carousel'; 
import { CardCarouselData } from './CarouselCard';

export default {
    component: Carousel,
    title: 'Carousel',
};

const Template = args => <Carousel {...args} />;

function onChange(selectedCards) {
    console.log(selectedCards);
}

export const Default = Template.bind({});
Default.args = {
    title: "Default",
    cardData: [new CardCarouselData("Test Header", "Test subtitle", "https://reactstrap.github.io/assets/318x180.svg", "alt caption here"),
        new CardCarouselData("Test Header", "Test subtitle", "https://reactstrap.github.io/assets/318x180.svg", "alt caption here"),
        new CardCarouselData("Test Header", "Test subtitle", "https://reactstrap.github.io/assets/318x180.svg", "alt caption here"),
        new CardCarouselData("Test Header", "Test subtitle", "https://reactstrap.github.io/assets/318x180.svg", "alt caption here"),
        new CardCarouselData("Test Header", "Test subtitle", "https://reactstrap.github.io/assets/318x180.svg", "alt caption here"),
        new CardCarouselData("Test Header", "Test subtitle", "https://reactstrap.github.io/assets/318x180.svg", "alt caption here"),
        new CardCarouselData("Test Header", "Test subtitle", "https://reactstrap.github.io/assets/318x180.svg", "alt caption here"),
        new CardCarouselData("Test Header", "Test subtitle", "https://reactstrap.github.io/assets/318x180.svg", "alt caption here"),
    ],
    onChange: onChange
};