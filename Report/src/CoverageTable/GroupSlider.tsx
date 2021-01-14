import React from 'react';
import Slider, { ValueLabelProps } from '@material-ui/core/Slider';
import Typography from '@material-ui/core/Typography';
import { GroupingLevel } from "./GroupingLevel";
import Grid from '@material-ui/core/Grid';
import Box from '@material-ui/core/Box';
import { Tooltip } from '@material-ui/core';
import { isBreakStatement } from 'typescript';

function GroupLabelComponent(props:ValueLabelProps){
    let text="";
    switch(props.value){
        case GroupingLevel.AssemblyNamespace:
            text = "Namespace";
            break;
        case GroupingLevel.Assembly:
            text =  "Assembly";
            break;
        case GroupingLevel.None:
            text = "All";
            break;
    }
    return <Tooltip enterTouchDelay={0} placement="top" title={text}>
    {props.children}
  </Tooltip>
}
export function GroupSlider(props: { currentGrouping: GroupingLevel; groupingChanged: (evt: any, value: number) => void; }) {
    return (
    <Grid container alignItems="center">
        <Grid item>
            <Box mr={1}>
                <Typography component="span" id="grouping-slider">
                    {`Group`}
                </Typography>
            </Box>
        </Grid>
        <Grid item style={{width:100}}>
            <Box mr={2}>
                <Slider
                    getAriaValueText={(value, index) => "todo"} //todo
                    aria-labelledby="grouping-slider"
                    ValueLabelComponent={GroupLabelComponent}
                    step={1}
                    marks
                    min={0}
                    max={2}
                    value={props.currentGrouping}
                    onChange={props.groupingChanged} />
            </Box>
        </Grid>
    </Grid>)

}
