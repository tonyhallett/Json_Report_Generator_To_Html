import React from 'react';
import { JsonAssemblyCoverage } from '../jsonSummaryResult';
import { GroupFilter, GroupSorter } from './GroupSorter/groupSorter';
import { GroupedObject } from "./GroupSorter/GroupedObject";
import { GroupSlider } from './GroupSlider';
import { GroupingLevel } from './GroupingLevel';
import { JsonClassCoverageWithAssembly } from './JsonClassCoverageWithAssembly';
import { CoverageTableView, JsonClassCoverageColumn, PropertyRowCellCreator } from './CoverageTableView';
import Grid from '@material-ui/core/Grid';
import { sum } from '../sum';
import { ExpandCollapse } from './ExpandCollapse';
import { GroupNameCell } from './GroupNameCell';
import { ClassNameCell } from './ClassNameCell';
import { TableCell, TextField } from '@material-ui/core';
import { Coverage } from './Coverage';
import { ColumnSort } from './SortableHeaderCell';
import { GroupValueSummer } from './GroupSorter/GroupValueSummer';

interface CoverageTableProps{
    assemblies:JsonAssemblyCoverage[];
}
interface CoverageTableState{
    groupedObjects:GroupedObject<JsonClassCoverageWithAssembly>[],
    grouping:GroupingLevel,
    columns:JsonClassCoverageColumn[],
    filter:string
}

export const NoNamespace = "__No Namespace";
export class CoverageTable extends React.Component<CoverageTableProps,CoverageTableState> {
    initialSortProperty:keyof JsonClassCoverageWithAssembly = "name";
    initialSortDirectionAscending = true;
    classFilterSimple = true;

    firstRender:boolean = true
    groupSorter:GroupSorter<JsonClassCoverageWithAssembly>;
    propertyRowCellCreatorsLookup: Map<keyof JsonClassCoverageWithAssembly,PropertyRowCellCreator> = new Map();
    constructor(props:CoverageTableProps){
        super(props);
        this.toggleGroupExpansion = this.toggleGroupExpansion.bind(this);
        this.groupingChanged = this.groupingChanged.bind(this);
        this.expandOrCollapseAll = this.expandOrCollapseAll.bind(this);
        this.sort = this.sort.bind(this);
        this.filter = this.filter.bind(this);
        
        this.initializePropertyRowCellCreators();
        this.state = {
            groupedObjects:undefined,
            grouping:GroupingLevel.Assembly,
            columns:this.initializeColumns(),
            filter:""
        }
    }
    filterString = "";
    private groupFilter:GroupFilter<JsonClassCoverageWithAssembly> = (group => {
        if(this.filterString === ""){
            return false;
        }
        if(group.isTerminal()){
            const details = group.getHeader();
            const toFilter = this.classFilterSimple ? details.name : details.qualifiedName;
            return toFilter.toLowerCase().indexOf(this.filterString.toLowerCase())==-1;
        }
        return false;
    })
    private initializeColumns():JsonClassCoverageColumn[] {
        const jsonClassCoverageColumns:JsonClassCoverageColumn[] = [
            {
                property:"name",
                header:"Name",
                columnSort:ColumnSort.Not
            },
            {
                property:"coveredlines",
                header:"Covered",
                columnSort:ColumnSort.Not
            },
            {
                property:"uncoveredlines",
                header:"Uncovered",
                columnSort:ColumnSort.Not
            },
            {
                property:"coverablelines",
                header:"Coverable",
                columnSort:ColumnSort.Not
            },
            {
                property:"totallines",
                header:"Total",
                columnSort:ColumnSort.Not

            },
            {
                property:"coverage",
                header:"Line Coverage",
                columnSort:ColumnSort.Not,
                span:2
            },
            {
                property:"coveredbranches",
                header:"Covered Branches",
                columnSort:ColumnSort.Not,
            },
            {
                property:"totalbranches",
                header:"Total",
                columnSort:ColumnSort.Not
            },
            {
                property:"branchcoverage",
                header:"Branch coverage",
                columnSort:ColumnSort.Not,
                span:2
            }
        ]
        for(let i=0;i<jsonClassCoverageColumns.length;i++){
            const column = jsonClassCoverageColumns[i];
            if(column.property == this.initialSortProperty){
                column.columnSort = this.initialSortDirectionAscending ?ColumnSort.Ascending:ColumnSort.Descending;
            }
        }
        return jsonClassCoverageColumns;
    }
    private initializePropertyRowCellCreators(){
        this.propertyRowCellCreatorsLookup.set("name",(g)=> {
            return g.groupLevel > 0 ?
            <GroupNameCell toggleGroupExpansion={this.toggleGroupExpansion} isCollapsed={g.isCollapsed} id={g.id} name={g.name} level={g.groupLevel} /> :
            <ClassNameCell groupingLevel={this.state.grouping} assemblyName={g.assemblyName} name={g.name} qualifiedClassName={g.qualifiedName} />;
        });
        const simpleProperties:(keyof JsonClassCoverageWithAssembly)[] =["coveredlines","uncoveredlines","coverablelines","totallines","totalbranches","coveredbranches"];
        simpleProperties.forEach((property)=>{
            this.propertyRowCellCreatorsLookup.set(property,(g)=> {
                return <TableCell>{g[property]}</TableCell>
            });
        });
        
        const showPercentageBar = true;
        const showPercentageCoverage = true;
        
        this.propertyRowCellCreatorsLookup.set("coverage",(g)=> {
            return <Coverage showPercentageBar={showPercentageBar} showPercentageCoverage={showPercentageCoverage} coverage={g.coverage} denominator={g.coverablelines} numerator={g.coveredlines}/>
        });

        this.propertyRowCellCreatorsLookup.set("branchcoverage",(g)=> {
            return <Coverage showPercentageBar={showPercentageBar} showPercentageCoverage={showPercentageCoverage}  coverage={g.branchcoverage} denominator={g.totalbranches} numerator={g.coveredbranches}/>
        });
    }
    componentWillUnmount(){
    }

    private expandOrCollapseAll(expandAll:boolean){
        this.setState({groupedObjects: this.groupSorter.expandCollapseAll(expandAll)});
    }
    private groupingChanged(evt:any,newGroupingLevel:GroupingLevel){
        //bizarrely getting change from slider when no change
        if(newGroupingLevel!=this.state.grouping){
            this.setState({
                grouping:newGroupingLevel,
                groupedObjects:this.groupSorter.regroup(c => {
                    return this.performGrouping(c,newGroupingLevel);
                })
            })
        }
    }
    
    private sort(key:keyof JsonClassCoverageWithAssembly){
        let newSortAscending = true;
        const newColumns = this.state.columns.map(c=>{
            if(c.property == key){
                let newSort = ColumnSort.Ascending;
                if(c.columnSort === ColumnSort.Ascending){
                    newSort = ColumnSort.Descending;
                    newSortAscending = false;
                }
                return {...c,columnSort:newSort};
            }else{
                return {...c,columnSort:ColumnSort.Not};
            }
        })
        const newGroupObjects = this.groupSorter.sort(key,newSortAscending);
        this.setState({
            columns:newColumns,
            groupedObjects:newGroupObjects
        })
    }
    private performGrouping(c:JsonClassCoverageWithAssembly,groupingLevel:GroupingLevel):Array<string>{
        switch(groupingLevel){
            case GroupingLevel.Assembly:
                return [c.assemblyName];
            case GroupingLevel.AssemblyNamespace:
                return [c.namespace,c.assemblyName];
            case GroupingLevel.None:
                return ["All"];

        }
        
    }
    private filter(event: React.ChangeEvent<HTMLInputElement>){
        this.filterString = event.target.value;
        const groupedObjects = this.groupSorter.refilter();
        this.setState({filter:this.filterString,groupedObjects})
    }
    private toggleGroupExpansion(groupId:string){
        this.setState({groupedObjects:this.groupSorter.toggleGroupExpansion(groupId)});
    }
    private simplifyClassName(className:string):{namespace:string,simplifiedClassName:string}{
        const lastIndex = className.lastIndexOf(".");
        if(lastIndex==-1){
            return {
                namespace:"",
                simplifiedClassName:className
            }
        }
        const namespace = className.substring(0,lastIndex);
        const simplifiedClassName = className.substring(lastIndex+1);
        return {namespace,simplifiedClassName}
    }
    private getFirstGrouping():GroupedObject<JsonClassCoverageWithAssembly>[]{
        const classes:JsonClassCoverageWithAssembly[] = [];
        this.props.assemblies.forEach(a => {
            a.classesinassembly.forEach(c=>{
                let {namespace,simplifiedClassName} = this.simplifyClassName(c.name);
                if(namespace == ""){
                    namespace = NoNamespace;
                }
                const assemblyName = a.name;
                const id = `${assemblyName}.${c.name}`
                classes.push(Object.assign({assemblyName:a.name,namespace,id},c,{name:simplifiedClassName,qualifiedName:c.name}));
            })
        })

        //change to merger?
        const groupSummer:GroupValueSummer<JsonClassCoverageWithAssembly> = (parts) => {
            const header:JsonClassCoverageWithAssembly = {

            } as JsonClassCoverageWithAssembly;
            
            const summabledProperties:(keyof JsonClassCoverageWithAssembly)[] = [
                "coveredlines",
                "uncoveredlines",
                "coverablelines",
                "totallines",
                "coveredbranches",
                "totalbranches"
            ]
            summabledProperties.forEach(p=>{
                header[p as any] = sum(parts,p);
            })
            
            if(header.coverablelines == 0){
                header.coverage = null;
            }else{
                header.coverage = header.coveredlines / header.coverablelines;
            }
            if(header.totalbranches == 0){
                header.branchcoverage = null;
            }else{
                header.branchcoverage = header.coveredbranches / header.totalbranches;
            }
            return header;
        };
        this.groupSorter = new GroupSorter<JsonClassCoverageWithAssembly>(classes,groupSummer,"name","id", this.groupFilter);
        this.groupSorter.group(c => {
            return this.performGrouping(c, this.state.grouping);
        
        });
        return this.groupSorter.sort(this.initialSortProperty,true);
    }

    render() {
        let groupedObjects:GroupedObject<JsonClassCoverageWithAssembly>[] = [];
        if(this.firstRender){
            groupedObjects = this.getFirstGrouping();
            this.firstRender = false;
        }else{
            groupedObjects = this.state.groupedObjects;
        }
        
        return <div>
                <Grid container alignItems="center">
                    <Grid item>
                        <GroupSlider currentGrouping={this.state.grouping} groupingChanged={this.groupingChanged}/> 
                    </Grid>
                    <Grid item>
                        <ExpandCollapse header="Expand / collapse all" expandOrCollapse={this.expandOrCollapseAll}/>
                    </Grid>
                    <Grid item>
                        <TextField variant="outlined" label="Filter" value={this.state.filter} onChange={this.filter}/>
                    </Grid>
                </Grid>
                
                
                <CoverageTableView propertyRowCellCreators={this.propertyRowCellCreatorsLookup} groupingLevel={this.state.grouping} columns={this.state.columns} sort={this.sort} groupedObjects={groupedObjects} toggleGroupExpansion={this.toggleGroupExpansion}/>

            </div>
    }
}




