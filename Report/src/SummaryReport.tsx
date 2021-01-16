import React from 'react'
import ReactDOMServer from 'react-dom/server';
import { Tabbable, TabsControl } from './controls/TabsControl';
import { CoverageTable } from './CoverageTable/CoverageTable';
import { SummaryTable } from './SummaryTable';
import { ToggleAutoReport } from './LinkBacks/ToggleAutoReport';
import { JsonSummaryResult } from './jsonSummaryResult';
import { GenerateReport } from './LinkBacks/GenerateReport';
import LinearProgress from '@material-ui/core/LinearProgress';
import { FineCodeCoverageIcon } from './FineCodeCoverageIcon';
import { logExternal, safeExecuteExternal } from './executeExternal';
import SettingsIcon from '@material-ui/icons/Settings';
import { ProjectsView } from './ProjectsView';
import { Project } from './Project';
import { EnabledProject } from './EnabledProject';
import { LinkBacks } from './LinkBacks/LinkBacks';
import { IconButton } from '@material-ui/core';
import { SettingsScreen } from './SettingsScreen';

interface InitialSettings{
    reportGenerationEnabled:boolean,
    projects:Array<Project>,
    showExpandCollapseAll:boolean,
    showFilter:boolean,
    showTooltips:boolean,
    showGroupSlider:boolean
}

export type Settings =  Omit<InitialSettings,"projects">

type SummaryReportState = Settings & {
    runningReport:boolean,
    projects:Array<EnabledProject>
    jsonSummaryResult:JsonSummaryResult,
    settingsOpen:boolean

    
}
export class SummaryReport extends React.Component<{},SummaryReportState> {
    constructor(props) {
        super(props);
        this.state={
            reportGenerationEnabled:true,
            runningReport:false,
            jsonSummaryResult:null,
            projects:[],
            showExpandCollapseAll:true,
            showFilter:true,
            showGroupSlider:true,
            showTooltips:true,
            settingsOpen:false
        }
        this.addGlobalMethods();
        this.projectEnabledChanged = this.projectEnabledChanged.bind(this);
        this.generateReport = this.generateReport.bind(this);
        this.generateReportForProject = this.generateReportForProject.bind(this);

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
                        let deserializedArguments = [];
                        if(arguments.length>0){
                            deserializedArguments = [JSON.parse(arguments[0])]
                        }
                        (self[p] as Function).apply(self,deserializedArguments);
                    }
                }
            }
        });
    }

    private generateReportForProjects(enabledProjects:Array<EnabledProject>){
        safeExecuteExternal(external => external.GenerateReport(JSON.stringify(enabledProjects)));
    }
    private generateReport(_:any){
        const enabledProjects = this.state.projects.filter(p=>p.enabled)
        this.generateReportForProjects(enabledProjects);
    }

    private generateReportForProject(project:EnabledProject){
        this.generateReportForProjects([project]);
    }
    private projectsEnabled():boolean{
        let enabled = false;
        for(let i=0;i<this.state.projects.length;i++){
            if(this.state.projects[i].enabled){
                enabled = true;
                break;
            }
        }
        return enabled;
    }
    render() {
        //can show constants as well 
        if(this.state.runningReport){
            return <LinearProgress />
        }
        //<FineCodeCoverageIcon/>
        const constants = <>
            <LinkBacks showTooltips={this.state.showTooltips}/>
            <ToggleAutoReport showTooltip={this.state.showTooltips} enabled={this.state.reportGenerationEnabled}/>
            <GenerateReport  disabled={!this.projectsEnabled()} onClick={this.generateReport} tooltip={this.state.showTooltips?"Run for selected projects":null}/>
            <IconButton size="small" onClick={()=>this.setState({settingsOpen:true})}>
                <SettingsIcon />
            </IconButton>
            <SettingsScreen 
                showExpandCollapseAll={this.state.showExpandCollapseAll}
                showFilter={this.state.showFilter}
                showGroupSlider={this.state.showGroupSlider}
                showTooltips={this.state.showTooltips}
                open={this.state.settingsOpen} handleClose={()=>this.setState({settingsOpen:false})}/>
        </>
        
        const jsonSummaryResult = this.state.jsonSummaryResult;
        const disableTabs = !jsonSummaryResult;
        const assemblies = jsonSummaryResult? jsonSummaryResult.coverage.assemblies:[]
        const summary = jsonSummaryResult? jsonSummaryResult.summary:null;
        
        var tabbables:Array<Tabbable> = [
            {title:"Coverage",disabled:disableTabs,element:<CoverageTable showGroupSlider={this.state.showGroupSlider} showTooltips={this.state.showTooltips} showFilter={this.state.showFilter} showExpandCollapseAll={this.state.showExpandCollapseAll} assemblies={assemblies}/>},
            {title:"Summary",disabled:disableTabs,element:<SummaryTable summary={summary} />},
            {title:"Risk Hotspots",disabled:disableTabs,element:"Risk hotspots"},
            {title:"Projects",disabled:false,element:<ProjectsView generateReportForProject={this.generateReportForProject} projects={this.state.projects} projectEnabledChanged={this.projectEnabledChanged}/>}
        ];
        var output = <div>
            {constants}
            <TabsControl tabsLabel="Report Tabs" tabbables={tabbables}/>
            
        </div>
        // console.log(ReactDOMServer.renderToStaticMarkup(output));
        return output;
    }
    private projectEnabledChanged(project:EnabledProject,enabled:boolean){
        this.setState({projects:this.state.projects.map(p=>{
            if(p === project){
                return {...p,enabled};
            }
            return p;
        })});
    }
    //C# callable - note the prefix
    
    __initialize(settings:InitialSettings){
        let {projects,...rest} = settings;
        const newState = {...rest,projects:this.getInitialEnabledProjects(projects)};
        this.setState(newState);
    }
    private getInitialEnabledProjects(projects:Array<Project>):Array<EnabledProject>{
        return projects.map(p=>({...p,enabled:true}));
    }
    __runningReport(){
        this.setState({runningReport:true});
    }
    __generateReport(jsonSummaryResult:JsonSummaryResult){
        this.setState({runningReport:false,jsonSummaryResult:jsonSummaryResult});
    }
    __reportGenerationEnabled(enabled:boolean){
        this.setState({reportGenerationEnabled:enabled});
    }

    __projectsAdded(projectsAdded:{projects:Array<Project>,newSolution:boolean}){
        const newSolution = projectsAdded.newSolution;
        const projects = projectsAdded.projects;
        if(newSolution){
            this.setState({projects:this.getInitialEnabledProjects(projects),jsonSummaryResult:null})
        }else{
            const currentEnabledProjects = this.state.projects;
            this.setState({projects:currentEnabledProjects.concat(this.getInitialEnabledProjects(projects))});
        }
    }
    __projectsRemoved(projectPaths:Array<string>){
        this.setState({projects:this.state.projects.filter(p=>{
            !projectPaths.includes(p.path);
        })});
    }

    __changeShowExpandCollapseAll(showExpandCollapseAll:boolean){
        this.setState({showExpandCollapseAll});
    }

    __changeShowFilter(showFilter:boolean){
        this.setState({showFilter})
    }

    __changeShowTooltips(showTooltips:boolean){
        this.setState({showTooltips})
    }
}

