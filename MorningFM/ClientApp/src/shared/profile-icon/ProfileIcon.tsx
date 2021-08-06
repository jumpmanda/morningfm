import React from "react";
import {ProfileLayout} from './ProfileIcon.style';
import ChevronRight from '@material-ui/icons/ChevronRight';

export interface IProfileIconProps {
    src?: string
}

export const ProfileIcon = ({...props}: IProfileIconProps) => {
    return (<ProfileLayout>
        <img src={props.src} alt={"Profile"}/>
        {/* <ChevronRight/> */}
    </ProfileLayout>);
}; 