import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableRow from '@material-ui/core/TableRow';

// todo type the tuple
export function LeftTable(props: { keyValues: Array<[any, any]>; }) {
    return <TableContainer>
        <Table>
            <TableBody>
                {props.keyValues.map((kv, i) => {
                    return <TableRow key={i}>
                        <TableCell>{kv[0]}</TableCell>
                        <TableCell>{kv[1]}</TableCell>
                    </TableRow>;
                })}
            </TableBody>
        </Table>

    </TableContainer>;
}
