import React from 'react';
import RateReview from '@material-ui/icons/RateReview';
import { LinkBack } from './LinkBack';

export function RateAndReview(props:{showTooltip:boolean}) {
  return <LinkBack showTooltip={props.showTooltip} title="Rate and review" Icon={RateReview} externalMethod="RateAndReview" />
}
