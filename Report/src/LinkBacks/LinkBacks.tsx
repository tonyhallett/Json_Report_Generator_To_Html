import React from 'react';
import { BuyMeACoffee } from './BuyMeACoffee';
import { LogIssueOrSuggestion } from './LogIssueOrSuggestion';
import { RateAndReview } from './RateAndReview';

export function LinkBacks(props: { showTooltips: boolean; }) {
    return <>
        <BuyMeACoffee showTooltip={props.showTooltips} />
        <LogIssueOrSuggestion showTooltip={props.showTooltips} />
        <RateAndReview showTooltip={props.showTooltips} />
    </>;
}
