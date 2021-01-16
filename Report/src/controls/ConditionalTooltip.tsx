import React from 'react';
import Tooltip, { TooltipProps } from '@material-ui/core/Tooltip';
export type ConditionalTooltipProps = Pick<TooltipProps,"children"|"title"> & {showTooltip:boolean}

export function ConditionalTooltip(props: ConditionalTooltipProps) {
  if (props.showTooltip) {
    return <Tooltip title={props.title}>
      <div style={{display:"inline-block"}}>
        {props.children}
      </div>
    </Tooltip>;
  }
  return props.children;
}
