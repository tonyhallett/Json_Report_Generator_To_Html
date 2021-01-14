import React from 'react';
import AssessmentIcon from '@material-ui/icons/Assessment';
import Tooltip from '@material-ui/core/Tooltip';
import ToggleButton from '@material-ui/lab/ToggleButton';

export function ToggleReport() {
  // will useState - will look for a global variable 
  // still not sure how get vs -> if does then report and wait for the vs change ?
  return   <Tooltip title="Toggle reporting">
      <ToggleButton
        value="yeah!"//todo what is this required for
        selected
        onChange={() => {
          console.log("Toggling")
          const external = window.external as any;
          if (external.toggleReportGeneration) {
            external.toggleReportGeneration();
          }
          //setSelected(!selected);
        }}
      >
        <AssessmentIcon/>
      
    </ToggleButton>
  </Tooltip>;
}
