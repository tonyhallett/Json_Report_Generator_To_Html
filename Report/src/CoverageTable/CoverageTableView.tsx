import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import { GroupedObject } from "./GroupSorter/GroupedObject";
import { JsonClassCoverageWithAssembly } from './JsonClassCoverageWithAssembly';
import { GroupingLevel } from './GroupingLevel';
import { ColumnSort, SortableHeaderCell } from './SortableHeaderCell';

export interface JsonClassCoverageColumn{
    property:keyof JsonClassCoverageWithAssembly,
    header:string,
    columnSort:ColumnSort,
    span?:number
}

interface CoverageTableViewProps{
     groupedObjects: GroupedObject<JsonClassCoverageWithAssembly>[]; 
     toggleGroupExpansion: (id: string) => void; 
     columns:JsonClassCoverageColumn[]
     sort:(key :keyof JsonClassCoverageWithAssembly)=>void,
     groupingLevel:GroupingLevel,
     propertyRowCellCreators:Map<keyof JsonClassCoverageWithAssembly,PropertyRowCellCreator>
}

export type PropertyRowCellCreator = (g:GroupedObject<JsonClassCoverageWithAssembly>) => any;//todo

export function CoverageTableView(props:CoverageTableViewProps) {
   
    return <Table size="small">
        <TableHead>
            <TableRow> 
                {
                    props.columns.map(c=>{
                        return <SortableHeaderCell colSpan={c.span} key={c.property} sort={props.sort} columnSort={c.columnSort} text={c.header} property={c.property} />
                    })
                }
            </TableRow>
        </TableHead>
        <TableBody>
            {props.groupedObjects.map(g => {
                return <TableRow key={g.id}>
                    {
                        props.columns.map(c=>{
                            const cell = props.propertyRowCellCreators.get(c.property)(g);
                            return React.cloneElement(cell,{key:c.property});
                        })
                    }
                </TableRow>;
            })}
        </TableBody>
    </Table>;
}





