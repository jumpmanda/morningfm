import React from 'react';
import { Story, Meta } from '@storybook/react';
import { PodcastShowList, PodcastShowListProps } from '../pages/shared/podcast-show-list/PodcastShowList';

export default {
  title: 'PodcastShowList',
  component: PodcastShowList,
} as Meta;

const Template: Story<PodcastShowListProps> = (args) => <PodcastShowList {...args} />;

export const Example = Template.bind({});

Example.args = { shows: [
  {
    id: '1heKeFb61FXJ8qBzAhl7xi',
    name: '.staging',
    images: [{ url: 'https://i.scdn.co/image/b2d2fa7f872231a8fcfa34b082f3b1623243209a'}]
  },
  {
    id: '1aCcf9CN3cunTBdkIzYTvo',
    name: 'Anything For Selena',
    images: [{ url: 'https://i.scdn.co/image/f093591dd9ebec99f58bd009b20a2c172aadc343'}]
  }
] } ;