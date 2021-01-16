import React from 'react';
import { GenerateReport } from './LinkBacks/GenerateReport';
import { Checkbox, Table, TableBody, TableCell, TableHead, TableRow } from '@material-ui/core';
import { isUnique } from './isUnique';
import { EnabledProject } from './EnabledProject';

export interface ProjectsViewProps{
  projects:Array<EnabledProject>,
  //can use the Project
  projectEnabledChanged:(prject:EnabledProject,enabled:boolean)=>void
  generateReportForProject:(project:EnabledProject)=>void
}
export function ProjectsView(props: ProjectsViewProps) {
    const projects = props.projects;
    const uniqueNames = isUnique(projects, "name");
    return <Table>
        <TableHead>
            <TableCell>Generate report</TableCell>
            <TableCell>Enabled multi</TableCell>
            <TableCell>Project</TableCell>
        </TableHead>
        <TableBody>
            {projects.map(project => {
                return <TableRow>
                    <TableCell>
                        <GenerateReport onClick={() => props.generateReportForProject(project)} />
                    </TableCell>
                    <TableCell>
                        <Checkbox checked={project.enabled} onChange={(evt) => props.projectEnabledChanged(project, evt.target.checked)} />
                    </TableCell>
                    <TableCell>{uniqueNames ? project.name : project.path}</TableCell>
                </TableRow>;
            })}
        </TableBody>
    </Table>;
}
