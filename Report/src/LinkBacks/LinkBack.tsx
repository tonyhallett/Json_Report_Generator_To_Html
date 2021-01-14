import React from 'react';
import Tooltip from '@material-ui/core/Tooltip';
import IconButton from '@material-ui/core/IconButton';
import { safeExecuteExternal } from '../executeExternal';

//todo type the icon

export function LinkBack(props: { title: string; Icon: any; externalMethod: string; }) {
  return <Tooltip title={props.title}>
    <IconButton onClick={() => {
      safeExecuteExternal(external => external[props.externalMethod]());
    }}>
      <props.Icon />
    </IconButton>
  </Tooltip>;
}
