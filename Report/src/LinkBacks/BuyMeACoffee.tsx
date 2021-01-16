import React from 'react';
import FreeBreakfastIcon from '@material-ui/icons/FreeBreakfast';
import { LinkBack } from './LinkBack';

export function BuyMeACoffee(props:{showTooltip:boolean}) {
  return <LinkBack showTooltip={props.showTooltip} title="Buy me a coffee" Icon={FreeBreakfastIcon} externalMethod="BuyMeACoffee" />
}


