import React from 'react';
import { TableCellProps, TableCell, IconButton, Box, SortDirection, TableSortLabel } from '@material-ui/core';
import ArrowDropDownIcon from '@material-ui/icons/ArrowDropDown';
import ArrowDropUpIcon from '@material-ui/icons/ArrowDropUp';

import { JsonClassCoverageWithAssembly } from './JsonClassCoverageWithAssembly';
type SortableHeaderCellProps = TableCellProps & {
  text:string,
  property:keyof JsonClassCoverageWithAssembly,
  columnSort:ColumnSort,
  sort:(text:string)=>void
}


type MuiAscendingDescending = Exclude<SortDirection,false>;
export const SortableHeaderCell: React.FunctionComponent<SortableHeaderCellProps> = props => {
  const {text, columnSort,property,sort,...tableCellProps} = props;
  let ariaSortDirection:SortDirection=false;
  let muiSortDirection:MuiAscendingDescending = "asc";
  if(props.columnSort!=ColumnSort.Not){
    muiSortDirection = props.columnSort === ColumnSort.Ascending ? "asc":"desc";
    ariaSortDirection = muiSortDirection;
  }
  //they keep space for their icon if not sorting and must fade on hover
  //also hover on the text changes when not sorting
  //could rotate
  function getIcon(){
      const Icon = columnSort === ColumnSort.Descending ? ArrowDropUpIcon: ArrowDropDownIcon;
      if(columnSort!=ColumnSort.Not){
          // how to determine
          return <Icon style={{color:"deeppink"}}/>
      }
      return <Icon/>
  }
  return <TableCell sortDirection={ariaSortDirection} {...tableCellProps}>
        <TableSortLabel active={props.columnSort!=ColumnSort.Not} direction={muiSortDirection} onClick={()=>sort(property)}>
        {/* <IconButton size="small" onClick={()=>sort(property)}>
            {getIcon()}
        </IconButton> */}
        {props.text}
        </TableSortLabel>
  </TableCell>
}

export enum ColumnSort {Ascending, Descending,Not}