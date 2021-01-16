import { IconButton } from '@material-ui/core';
import React from 'react';
import logo = require("./logo-transparent.png")

export function FineCodeCoverageIcon() {
    const dimensions = 45;
    return <IconButton><img width={dimensions} height={dimensions}  src={logo}/></IconButton>
}
