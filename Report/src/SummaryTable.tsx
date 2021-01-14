import React from 'react';
import { LeftTable } from './controls/LeftTable';
import { JsonCoverageSummary } from './jsonSummaryResult';

export function getLineCoverageDisplay(jsonCoverageSummary:JsonCoverageSummary){
  var coveredLinesDisplay = jsonCoverageSummary.coveredlines.toString();
  var coverableLinesDisplay = jsonCoverageSummary.coverablelines.toString()
  return `${jsonCoverageSummary.linecoverage.toString()}% (${coveredLinesDisplay} of ${coverableLinesDisplay})`
}

export function SummaryTable(props: { summary: JsonCoverageSummary; }) {
    var summary = props.summary;
    return <LeftTable keyValues={[
        ["Assemblies", summary.assemblies.toString()],
        ["Classes", summary.classes.toString()],
        ["Files", summary.files.toString()],
        ["Covered lines", summary.coveredlines.toString()],
        ["Uncovered lines", summary.uncoveredlines.toString()],
        ["Coverable lines", summary.coverablelines.toString()],
        ["Total lines", summary.totallines.toString()],
        ["Line coverage", getLineCoverageDisplay(summary)]
    ]} />;
}
