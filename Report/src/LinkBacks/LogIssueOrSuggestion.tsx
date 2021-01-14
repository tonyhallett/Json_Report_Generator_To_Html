import React from 'react';
import Github from '@material-ui/icons/Github';
import { LinkBack } from './LinkBack';

export function LogIssueOrSuggestion() {
  return <LinkBack title="Log issue or suggestion" Icon={Github} externalMethod="LogIssueOrSuggestion" />
}
