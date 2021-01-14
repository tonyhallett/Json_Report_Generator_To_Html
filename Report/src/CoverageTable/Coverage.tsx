import React from 'react';
import TableCell from '@material-ui/core/TableCell';
import { PercentageBar } from "./PercentageBar";
import { PercentageCoverage } from "./PercentageCoverage";

export interface CoverageProps{
  coverage:number,
  numerator:number,
  denominator:number,
  showPercentageCoverage:boolean,
  showPercentageBar:boolean
}

export function Coverage(props: CoverageProps) {
    const percentageBar = <TableCell>{props.showPercentageBar ? <PercentageBar backgroundColor={props.coverage===null?"gray":"red"} percentColor='green' height={10} width={100} percentDecimal={props.coverage} /> : null}</TableCell>;
    if (props.coverage == null) {
        return <>
            <TableCell align="right"></TableCell>
            {percentageBar}
        </>;
    }
    return <>
        <TableCell align="right">{props.showPercentageCoverage ? <PercentageCoverage coverage={props.coverage} numerator={props.numerator} denominator={props.denominator} /> : null}</TableCell>
        {percentageBar}
    </>;
}


