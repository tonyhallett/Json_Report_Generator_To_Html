import React from 'react'
import PlayCircleFilledIcon from '@material-ui/icons/PlayCircleFilled';
import IconButton, { IconButtonProps } from '@material-ui/core/IconButton';
import { ConditionalTooltip } from '../controls/ConditionalTooltip';
export function GenerateReport(props:{onClick:IconButtonProps['onClick'],tooltip?:string,disabled?:boolean}) {
  return <ConditionalTooltip title={props.tooltip} showTooltip={!!props.tooltip}>
    <IconButton disabled={props.disabled} onClick={props.onClick}>
      <PlayCircleFilledIcon/>
    </IconButton>
  </ConditionalTooltip>
}

