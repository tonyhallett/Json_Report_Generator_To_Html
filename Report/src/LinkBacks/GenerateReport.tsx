import React from 'react'
import PlayCircleFilledIcon from '@material-ui/icons/PlayCircleFilled';
import IconButton from '@material-ui/core/IconButton';
import { safeExecuteExternal } from '../executeExternal';
export function GenerateReport() {
  return <IconButton onClick={()=>{
    safeExecuteExternal(external => external.generateReport());
  }}>
    <PlayCircleFilledIcon/>
  </IconButton>
}
