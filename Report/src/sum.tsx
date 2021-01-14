export function sum<T>(parts: Array<any>, key: string):number {
    return parts.reduce((prev, c) => {
        return prev + c[key];
    }, 0);
}
