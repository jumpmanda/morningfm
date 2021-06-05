import React from 'react';
import { Story, Meta } from '@storybook/react';
import { ProfileIcon, IProfileIconProps } from '../shared/profile-icon/ProfileIcon'; 
import { NavMenu } from 'shared/nav-menu/NavMenu'; 

export default {
    title: 'ProfileIcon',
    component: ProfileIcon,
  } as Meta;

let img = "https://s.gravatar.com/avatar/863ae112a61c0c0400df258a9af86352?s=480&r=pg&d=https%3A%2F%2Fcdn.auth0.com%2Favatars%2Fli.png";


const Template: Story<IProfileIconProps> = (args) => <ProfileIcon {...args} />;

export const Example = Template.bind({});

Example.args = {
    src: img
};

const NavMenuTemplate: Story<any> = (args) => <NavMenu {...args} />;

export const InNavMenu = NavMenuTemplate.bind({});

InNavMenu.args = {
};