import React from 'react';
import TableCell from '@material-ui/core/TableCell';
import IconButton from '@material-ui/core/IconButton';
import { Box } from '@material-ui/core';

// todo type name
export function LevelNameCell(props: { level: number; click: () => void; name: any; Icon: any; }) {
    return <TableCell component="th" scope="row">
        <Box component="span" pl={props.level}>
            <IconButton size="small" onClick={props.click}>
                <props.Icon />
            </IconButton>
        </Box>
        {props.name}
    </TableCell>;
}
