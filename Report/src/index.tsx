import React from 'react'
import ReactDOM from 'react-dom'
import { Root } from './Root'
import 'ts-polyfill';

import { SummaryReport } from './SummaryReport'

/* window.onerror = function(message:string){
  try{
    (window.external as any).LogError(message);
  }finally{}
} */


ReactDOM.render(
	<Root/>,
  document.getElementById('root'),
)