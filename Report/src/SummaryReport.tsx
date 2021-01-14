import React from 'react'
import ReactDOMServer from 'react-dom/server';
import { getJsonSummary } from './jsonSummaryProvider'
import { Tabbable, TabsControl } from './controls/TabsControl';
import { CoverageTable } from './CoverageTable/CoverageTable';
import { SummaryTable } from './SummaryTable';
import { BuyMeACoffee } from './LinkBacks/BuyMeACoffee';
import { LogIssueOrSuggestion } from './LinkBacks/LogIssueOrSuggestion';
import { RateAndReview } from './LinkBacks/RateAndReview';
import { ToggleReport } from './LinkBacks/ToggleReportGeneration';

const jsonSummary = getJsonSummary();

export class SummaryReport extends React.Component<{},{}> {
    constructor(props) {
        super(props);
    }
                
    render() {
        var tabbables:Array<Tabbable> = [
            {title:"Coverage",element:<CoverageTable assemblies={jsonSummary.coverage.assemblies}/>},
            {title:"Summary",element:<SummaryTable summary={jsonSummary.summary} />},
            {title:"Risk Hotspots",element:"Risk hotspots"}];
        var output = <div>
            <BuyMeACoffee/><LogIssueOrSuggestion/><RateAndReview/><ToggleReport/>
            <TabsControl tabsLabel="Report Tabs" tabbables={tabbables}/>
            
        </div>
        // console.log(ReactDOMServer.renderToStaticMarkup(output));
        return output;
    }
}


