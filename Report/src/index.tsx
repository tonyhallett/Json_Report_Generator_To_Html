import React from 'react'
import ReactDOM from 'react-dom'

import { SummaryReport } from './SummaryReport'

/* window.onerror = function(message:string){
  try{
    (window.external as any).LogError(message);
  }finally{}
} */


ReactDOM.render(
	<SummaryReport ref={summaryReport => (window as any).summaryReport = summaryReport}/>,
  document.getElementById('root'),
)