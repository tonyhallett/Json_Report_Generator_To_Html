import React from 'react';
import AssessmentIcon from '@material-ui/icons/Assessment';
import Tooltip from '@material-ui/core/Tooltip';
import ToggleButton from '@material-ui/lab/ToggleButton';
import { logExternal, safeExecuteExternal } from '../executeExternal';
import { ConditionalTooltip } from '../controls/ConditionalTooltip';

export interface ToggleAutoReportProps{
  enabled:boolean,
  showTooltip:boolean
}
export function ToggleAutoReport(props:ToggleAutoReportProps) {
  return <ConditionalTooltip showTooltip={props.showTooltip} title={`Auto ${props.enabled?"on":"off"}`}>
    <ToggleButton
    value=""
    selected={props.enabled}
    onChange={() => {
      safeExecuteExternal(external => external.ReportGenerationEnabled(!props.enabled))
    }}
  >
    <AssessmentIcon/>
  </ToggleButton>
  </ConditionalTooltip>
  
}
