import React from 'react';

import CarouselCard, { CardCarouselData } from './CarouselCard';

export default {
    component: CarouselCard,
    title: 'CarouselCard',
};

const Template = args => <CarouselCard {...args} />;

function onChange(isSelected, index) {
    console.log('Card index: ' + index);
    console.log("Card selected: " + isSelected);
}

export const Default = Template.bind({});
Default.args = {
    title: "Test Header",
    subtitle: "Test subtitle",
    imageSrc: "https://reactstrap.github.io/assets/318x180.svg",
    altCaption: "alt caption here",
    maxWidth: "50%",
    index: 1,
    onChange: onChange
};