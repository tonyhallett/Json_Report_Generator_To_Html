import React from 'react';
import Tooltip from '@material-ui/core/Tooltip';

interface PercentageCoverageProps{
    coverage:number,
    numerator:number,
    denominator:number
}
export function PercentageCoverage(props: PercentageCoverageProps) {
    const percentage = props.coverage * 100;
    const percentageRounded = Math.round((percentage + Number.EPSILON) * 100) / 100;
    return <Tooltip title={`${props.numerator}/${props.denominator}`}><span>{`${percentageRounded} %`}</span></Tooltip>;
}
