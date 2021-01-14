import React, { useState } from 'react';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import Box from '@material-ui/core/Box'
export interface Tabbable{
    title:string
    element:any//todo
}

export function TabPanel(props) {
    const { children, value, index, ...other } = props;
  
    return (
      <div
        role="tabpanel"
        hidden={value !== index}
        id={`simple-tabpanel-${index}`}
        aria-labelledby={`simple-tab-${index}`}
        {...other}
      >
        {value === index && (
          <Box p={3}>
            {/* <Typography>{children}</Typography> */}
            {children}
          </Box>
        )}
      </div>
    );
}

export function tabId(index:number){
  return `simple-tab-${index}`;
}
//todo pass through additional props to Tabs
export function TabsControl(props: { tabbables: Array<Tabbable>; tabsLabel: string; }) {
    function a11yProps(index) {
        return {
            id: tabId(index),
            'aria-controls': `simple-tabpanel-${index}`,
        };
    }
    const [selectedTabIndex, setSelectedTabIndex] = useState(0);
    
    return <div>

        <Tabs value={selectedTabIndex} onChange={(evt, newIndex) => setSelectedTabIndex(newIndex)} aria-label={props.tabsLabel}>
            {props.tabbables.map((tabbable, index) => {
                return <Tab key={index} label={tabbable.title} {...a11yProps(index)} />;
            })}
        </Tabs>

        {props.tabbables.map((tabbable, index) => {
            return <TabPanel key={index} value={selectedTabIndex} index={index}>
                {tabbable.element}
            </TabPanel>;

        })}
    </div>;
}
