var jsonSummary:JsonSummaryResult = {
	summary: {
			assemblies:2,
			classes:10,
			branchcoverage:0.5,
			coveredbranches:5,
			totalbranches:10,

			coveredlines:2,
			coverablelines:20,
			uncoveredlines:18,
			linecoverage:0.1,
			totallines:30,

			files:3,
			parser:"Cobertura",
			generatedon:new Date(),
	},
	coverage:null
}

// I made a typescript helper for this !
jest.mock('../src/jsonSummaryProvider',() => {
	return {
		getJsonSummary(){
			return jsonSummary;
	}
}});

import React from 'react'
import { findAllByRole, findByRole, getAllByRole, getByRole, getByText, getNodeText, prettyDOM, queryByRole, render, screen } from '@testing-library/react'
import '@testing-library/jest-dom/extend-expect'
import { SummaryReport } from '../src/SummaryReport'
import { JsonSummaryResult } from '../src/jsonSummaryResult'
import { getLineCoverageDisplay } from '../src/SummaryTable';


describe('should have 3 tabs', () => {
	let tabs:HTMLElement[];
  beforeEach(()=>{
		var {getAllByRole} = render(<SummaryReport/>);
		tabs = getAllByRole('tab');
	})
	
	test('should have 3 tabs', () => {
		expect(tabs.length).toBe(3);
	});

	function tabNameTest(tabIndex:number, expectedTabName:string){
		var tab = tabs[tabIndex];
		getByText(tab,expectedTabName);
	}

	describe('first tab', () => {
		test('should be the Coverage tab', () => {
			tabNameTest(0, "Coverage");
		});

		test('should be selected', () => {
			var firstTab = tabs[0];
			expect(firstTab).toHaveAttribute("aria-selected","true");
		});

		describe('tab panel', () => {
			describe('should contain coverage table', () => {
				let table:HTMLElement;
				beforeEach(()=> {
					var tabPanel = screen.getByRole('tabpanel',{hidden:false});
					table = getByRole(tabPanel, 'table');
				})
				describe('column headers', () => {

				})
				describe('default grouping', () => {
					//sorting
					//hiding
				});
				//filtering
			});
		});
	});

	describe('second tab', () => {
		test('should be the Summary tab', () => {
			tabNameTest(1, "Summary");
		});

		describe('tab panel', () => {
			describe('should contain summary table',  () => {
				let rows:HTMLElement[];
				beforeEach(() => {
					var secondTab = tabs[1];
					secondTab.click();
					var tabPanel = screen.getByRole('tabpanel',{hidden:false});
					rows = getAllByRole(tabPanel,'row');
				})
				var summary = jsonSummary.summary;

				// could make this strict with a row index
				function shouldHaveSummaryRowFor(heading:string,value:string){
					var row = rows.find(r=>{
						var cells = getAllByRole(r,'cell');
						var headingCell = cells[0];
						var valueCell = cells[1];
						return cells.length == 2 && getNodeText(headingCell) == heading && getNodeText(valueCell) == value;
					});
					expect(row).toBeDefined();
				}

				test('should have row for assemblies', () => {
					shouldHaveSummaryRowFor('Assemblies', summary.assemblies.toString());
				});

				test('should have row for classes', () => {
					shouldHaveSummaryRowFor('Classes', summary.classes.toString());
				});

				test('should have row for files', () => {
					shouldHaveSummaryRowFor('Files', summary.files.toString());
				});

				test('should have row for covered lines', () => {
					shouldHaveSummaryRowFor('Covered lines', summary.coveredlines.toString());
				});

				test('should have row for uncovered lines', () => {
					shouldHaveSummaryRowFor('Uncovered lines', summary.uncoveredlines.toString());
				});

				test('should have row for coverable lines', () => {
					shouldHaveSummaryRowFor('Coverable lines', summary.coverablelines.toString());
				});

				test('should have row for total lines', () => {
					shouldHaveSummaryRowFor('Total lines', summary.totallines.toString());
				});

				
				// todo consider nulls
				test('should have row for line lcoverage', () => {
					shouldHaveSummaryRowFor('Line coverage', getLineCoverageDisplay(summary));
				});

				// todo - there is no branch coverage being displayed

			});
			
		})
	});

	describe('third tab', () => {
		test('should be the Risk Hotspots tab', () => {
			tabNameTest(2, "Risk Hotspots");
		});
	});
	
});


