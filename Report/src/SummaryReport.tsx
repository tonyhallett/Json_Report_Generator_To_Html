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
import { FineCodeCoverageIcon } from './FineCodeCoverageIcon';


interface InitialSettings{
    reportGenerationEnabled:boolean
}

interface SummaryReportState extends InitialSettings{
    runningReport:boolean,
    jsonSummaryResult:JsonSummaryResult
}
export class SummaryReport extends React.Component<{},SummaryReportState> {
    constructor(props) {
        super(props);
        this.state={
            reportGenerationEnabled:true,
            runningReport:false,
            jsonSummaryResult:null
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
        //can show constants as well 
        if(this.state.runningReport){
            return <LinearProgress />
        }
        const constants = <>
            <FineCodeCoverageIcon/>
                <BuyMeACoffee/><LogIssueOrSuggestion/><RateAndReview/>
                <ToggleAutoReport enabled={this.state.reportGenerationEnabled}/>
                <GenerateReport/>
        </>
        if(this.state.jsonSummaryResult === null){
            return constants;
        }
        var jsonSummaryResult = this.state.jsonSummaryResult;
        var tabbables:Array<Tabbable> = [
            {title:"Coverage",element:<CoverageTable assemblies={jsonSummaryResult.coverage.assemblies}/>},
            {title:"Summary",element:<SummaryTable summary={jsonSummaryResult.summary} />},
            {title:"Risk Hotspots",element:"Risk hotspots"}];
        var output = <div>
            {constants}
            <TabsControl tabsLabel="Report Tabs" tabbables={tabbables}/>
            
        </div>
        // console.log(ReactDOMServer.renderToStaticMarkup(output));
        return output;
    }
    //C# callable - note the prefix
    //did not work https://stackoverflow.com/questions/7322420/calling-javascript-object-method-using-webbrowser-document-invokescript
    __initialize(settings:InitialSettings){
        this.setState(settings);
    }
    __runningReport(){
        this.setState({runningReport:true});
    }
    __generateReport(jsonSummaryResult:JsonSummaryResult){
        alert("generate report !")
        //this to be the new 
        this.setState({runningReport:false,jsonSummaryResult:jsonSummaryResult});
    }
    __reportGenerationEnabled(enabled:boolean){
        this.setState({reportGenerationEnabled:enabled});
    }
}


