import React from 'react'
import ReactDOMServer from 'react-dom/server';
import { getJsonSummary } from './jsonSummaryProvider'
import { Tabbable, TabsControl } from './controls/TabsControl';
import { CoverageTable } from './CoverageTable/CoverageTable';
import { SummaryTable } from './SummaryTable';
import { BuyMeACoffee } from './LinkBacks/BuyMeACoffee';
import { LogIssueOrSuggestion } from './LinkBacks/LogIssueOrSuggestion';
import { RateAndReview } from './LinkBacks/RateAndReview';
import { ToggleAutoReport } from './LinkBacks/ToggleAutoReport';
import { JsonSummaryResult } from './jsonSummaryResult';
import { GenerateReport } from './LinkBacks/GenerateReport';
import LinearProgress from '@material-ui/core/LinearProgress';
import { ArrowLeftRounded } from '@material-ui/icons';

const jsonSummary = getJsonSummary();

interface InitialSettings{
    ReportGenerationEnabled:boolean
}

interface SummaryReportState extends InitialSettings{
    runningReport:boolean
}
export class SummaryReport extends React.Component<{},SummaryReportState> {
    constructor(props) {
        super(props);
        this.state={
            ReportGenerationEnabled:true,

            runningReport:false
        }
        this.addGlobalMethods();
    }

    //tod decorators would be better
    public addGlobalMethods(){
        const anyWindow = window as any;
        Object.getOwnPropertyNames(Object.getPrototypeOf(this)).forEach(p => {
            if(p.startsWith("__")){
                const windowFunctionName = p.substr(2);
                if(!anyWindow[windowFunctionName]){
                    var self = this;
                    anyWindow[windowFunctionName] = function(){
                        (self[p] as Function).apply(self,arguments);
                    }
                }
            }
        });
    }
    
    render() {
        if(this.state.runningReport){
            return <LinearProgress />
        }
        var tabbables:Array<Tabbable> = [
            {title:"Coverage",element:<CoverageTable assemblies={jsonSummary.coverage.assemblies}/>},
            {title:"Summary",element:<SummaryTable summary={jsonSummary.summary} />},
            {title:"Risk Hotspots",element:"Risk hotspots"}];
        var output = <div>
            <BuyMeACoffee/><LogIssueOrSuggestion/><RateAndReview/>
            <ToggleAutoReport enabled={this.state.ReportGenerationEnabled}/>
            <GenerateReport/>
            <TabsControl tabsLabel="Report Tabs" tabbables={tabbables}/>
            
        </div>
        // console.log(ReactDOMServer.renderToStaticMarkup(output));
        return output;
    }
    //C# callable - note the prefix
    //did not work https://stackoverflow.com/questions/7322420/calling-javascript-object-method-using-webbrowser-document-invokescript
    //care - C# property style
    __initialize(settings:InitialSettings){
        alert("in initialize");
        alert(settings.ReportGenerationEnabled);
        this.setState({
            ReportGenerationEnabled:settings.ReportGenerationEnabled
        });
    }
    __runningReport(){
        this.setState({runningReport:true});
    }
    __generateReport(jsonSummaryResult:JsonSummaryResult){
        //will set running report to false
    }
    __reportGenerationEnabled(enabled:boolean){
        this.setState({ReportGenerationEnabled:enabled});
    }
}




