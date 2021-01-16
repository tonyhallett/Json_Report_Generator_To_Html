import React from 'react'
import PlayCircleFilledIcon from '@material-ui/icons/PlayCircleFilled';
import IconButton, { IconButtonProps } from '@material-ui/core/IconButton';
import { Tooltip } from '@material-ui/core';
export function GenerateReport(props:{onClick:IconButtonProps['onClick'],tooltip?:string,disabled?:boolean}) {
  const button =  <IconButton disabled={props.disabled} onClick={props.onClick}>
    <PlayCircleFilledIcon/>
  </IconButton>

  if(props.tooltip){
    return <Tooltip title={props.tooltip}>
      {button}
    </Tooltip>
  }

  return button;
}

