import React from 'react';
import AssessmentIcon from '@material-ui/icons/Assessment';
import Tooltip from '@material-ui/core/Tooltip';
import ToggleButton from '@material-ui/lab/ToggleButton';
import { logExternal, safeExecuteExternal } from '../executeExternal';

export interface ToggleAutoReportProps{
  enabled:boolean
}
export function ToggleAutoReport(props:ToggleAutoReportProps) {
  return   <Tooltip title="Toggle reporting">
      <ToggleButton
        value="yeah!"//todo what is this required for
        selected={props.enabled}
        onChange={() => {
          safeExecuteExternal(external => external.ReportGenerationEnabled(!props.enabled))
        }}
      >
        <AssessmentIcon/>
      
    </ToggleButton>
  </Tooltip>;
}
