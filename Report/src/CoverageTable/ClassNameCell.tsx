import React from 'react';
import LaunchIcon from '@material-ui/icons/Launch';
import { GroupingLevel } from './GroupingLevel';
import { safeExecuteExternal } from '../executeExternal';
import { LevelNameCell } from './LevelNameCell';

export function ClassNameCell(props: {groupingLevel:GroupingLevel, assemblyName: string; qualifiedClassName: string; name: string; }) {
    return <LevelNameCell level={props.groupingLevel+1} Icon={LaunchIcon} name={props.name} click={
        () => {
            console.log(`open externally ${props.assemblyName} ${props.qualifiedClassName}`);
            //todo - remove safe when only using in webbrowser control
            //will probably generate external typing as well

            // may be able to attach the file from custom IReportBuilder
            // file argument not used in OutputToolWindowControl
            safeExecuteExternal(external => external.OpenFile(props.assemblyName, props.qualifiedClassName));
        }

    } />
}

