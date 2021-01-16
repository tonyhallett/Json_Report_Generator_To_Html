import { Dialog, AppBar, Toolbar, IconButton, Typography, Button, List, ListItem, ListItemText, Divider, Slide, makeStyles, createStyles, Theme, Table, TableCell, TableRow, Checkbox, TableBody } from '@material-ui/core';
import React from 'react';
import CloseIcon from '@material-ui/icons/Close';
import { Settings } from './SummaryReport';
import { Tabbable, TabsControl } from './controls/TabsControl';
import { safeExecuteExternal } from './executeExternal';
type SettingsScreenProps = {
  open:boolean
  handleClose:()=>void
} &  Omit<Settings,"reportGenerationEnabled">

/*
  interface InitialSettings{
    showExpandCollapseAll:boolean,
    showFilter:boolean,
    showTooltips:boolean,
    showGroupSlider:boolean
}
*/


const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    appBar: {
      position: 'relative',
      backgroundColor:'gray'
    },
    title: {
      marginLeft: theme.spacing(2),
      flex: 1,
    },
  }),
);

/*
export interface Tabbable{
    title:string
    element:any//todo
    disabled?:boolean
}
*/
function isUpperCase(char:string){
  return char === char.toUpperCase();
}
function capitalizeFirst(toCapitalize:string){
  const first = toCapitalize.substr(0,1);
  const rest = toCapitalize.substr(1);
  return first.toUpperCase() + rest;
}
function spaceCamel(camel:string){
  let result ="";
  for (let i = 0; i < camel.length; i++) {
    const char = camel.charAt(i);
    if(isUpperCase(char) && i!==0){
      result += " "
    }
    result += char;
  }
  return capitalizeFirst(result);
  
}
export function SettingsScreen(props:SettingsScreenProps) {
  //will need to change the aria on the TabsControl
  const showProps:Array<keyof SettingsScreenProps> = ["showExpandCollapseAll","showFilter","showTooltips"]
  const tabbables:Array<Tabbable> = [
    {
      title:"Show/Hide",
      element:<Table>
        <TableBody>
          {
            showProps.map(showProp => {
              return <TableRow key={showProp}>
                <TableCell><Checkbox checked={props[showProp] as boolean} onChange={evt=>safeExecuteExternal(external=>external[`Change${capitalizeFirst(showProp)}`](evt.target.checked))}/></TableCell>
                <TableCell>{spaceCamel(showProp.substr(4))}</TableCell>
              </TableRow>
            })
          }
        </TableBody>
      </Table>
    },{
      title:"Grouping",
      element:<div>Grouping to do</div>
    },
    {
      title:"Summary Columns",
      element:<div>Columns to do</div>
    }
  ]
  const classes = useStyles();
  return <Dialog fullScreen open={props.open} onClose={props.handleClose}>
      <AppBar className={classes.appBar}>
          <Toolbar>
              <IconButton edge="start" color="inherit" onClick={props.handleClose} aria-label="close">
                  <CloseIcon />
              </IconButton>
              <Typography variant="h6" className={classes.title}>
                  Settings
          </Typography>
              <Button autoFocus color="inherit" onClick={props.handleClose}>
                  Done
          </Button>
          </Toolbar>
      </AppBar>
      <div>
        
        <TabsControl tabsLabel="SettingsTabs" tabbables={tabbables}/>
      </div>
  </Dialog>;
}
