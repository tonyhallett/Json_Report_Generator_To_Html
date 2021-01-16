import React from 'react';
import Github from '@material-ui/icons/Github';
import { LinkBack } from './LinkBack';

export function LogIssueOrSuggestion(props:{showTooltip:boolean}) {
  return <LinkBack showTooltip={props.showTooltip} title="Log issue or suggestion" Icon={Github} externalMethod="LogIssueOrSuggestion" />
}
