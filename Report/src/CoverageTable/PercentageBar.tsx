import React from 'react';





export function PercentageBar(props: { percentDecimal: number; percentColor: string; backgroundColor: string; height: any; width: any; }) {
    let transformAmount = (props.percentDecimal * 100) - 100;
    const transform = `translateX(${transformAmount}%)`;
    return <div style={{ display: "inline-block", position: "relative", "overflow": "hidden", backgroundColor: props.backgroundColor, height: props.height, width: props.width }}>
        <div style={{
            width: '100%',
            position: 'absolute',
            left: 0,
            bottom: 0,
            top: 0,
            transformOrigin: 'left',
            transform,
            backgroundColor: props.percentColor
        }}></div>
    </div>;
}
