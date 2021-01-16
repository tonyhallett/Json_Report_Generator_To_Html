import React from 'react';
import Tooltip, { TooltipProps } from '@material-ui/core/Tooltip';
export type ConditionalTooltipProps = Pick<TooltipProps,"children"|"title"> & {showTooltip:boolean}

export function ConditionalTooltip(props: ConditionalTooltipProps) {
  if (props.showTooltip) {
    return <Tooltip title={props.title}>
      {props.children}
    </Tooltip>;
  }
  return props.children;
}
