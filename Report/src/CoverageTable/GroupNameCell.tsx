import React from 'react';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import ExpandLessIcon from '@material-ui/icons/ExpandLess';
import { LevelNameCell } from "./LevelNameCell";
import { NoNamespace } from './CoverageTable';
import { Typography } from '@material-ui/core';


export function GroupNameCell(props: { name: string; level: number; id: string; toggleGroupExpansion: (id: string) => void; isCollapsed: boolean; }) {
    const ExpandIcon = props.isCollapsed ?  ExpandLessIcon : ExpandMoreIcon;
    let name:any = props.name;
    if(name == NoNamespace){
        name = <span style={{fontStyle:"italic"}}>No namespace</span>
    }
    return <LevelNameCell level={props.level} Icon={ExpandIcon} name={name} click={() => props.toggleGroupExpansion(props.id)}/>
}
