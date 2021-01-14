import React from 'react';
import ExpandLessIcon from '@material-ui/icons/ExpandLess';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import IconButton from '@material-ui/core/IconButton';
import Typography from '@material-ui/core/Typography';
import Grid from '@material-ui/core/Grid';

//todo spread props on container
export function ExpandCollapse(props: { expandOrCollapse: (expand: boolean) => void; header: string; }) {
    return <Grid container alignContent="center" alignItems="center">
        <Grid item>
            <Typography component="span">{props.header}</Typography>
        </Grid>
        <Grid item>
            <IconButton size="small" onClick={() => props.expandOrCollapse(false)}>
                <ExpandLessIcon />
            </IconButton>
            <IconButton size="small" onClick={() => props.expandOrCollapse(true)}>
                <ExpandMoreIcon />
            </IconButton>
        </Grid>
    </Grid>;
}
