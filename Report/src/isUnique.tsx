export function isUnique<T>(array: Array<T>, key: keyof T) {
  let unique = true;
  const uniques = [];
  for (let i = 0; i < array.length; i++) {
    const value = array[i][key];
    if (uniques.includes(value)) {
      unique = false;
      break;
    } else {
      uniques.push(value);
    }
  }
  return unique;
}
