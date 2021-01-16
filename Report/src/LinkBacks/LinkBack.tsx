import React from 'react';
import IconButton from '@material-ui/core/IconButton';
import { safeExecuteExternal } from '../executeExternal';
import { ConditionalTooltip } from '../controls/ConditionalTooltip';

//todo type the icon

export function LinkBack(props: { title: string; Icon: any; externalMethod: string; showTooltip:boolean}) {
  return <ConditionalTooltip title={props.title} showTooltip={props.showTooltip}>
    <IconButton onClick={() => {
      safeExecuteExternal(external => external[props.externalMethod]());
    }}>
      <props.Icon />
    </IconButton>
  </ConditionalTooltip>;
}



