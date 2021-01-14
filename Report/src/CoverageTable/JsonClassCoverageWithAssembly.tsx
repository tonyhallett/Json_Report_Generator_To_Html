import { JsonClassCoverage } from '../jsonSummaryResult';

export type JsonClassCoverageWithAssembly = JsonClassCoverage & { assemblyName: string; namespace: string; qualifiedName:string; id: string; };
