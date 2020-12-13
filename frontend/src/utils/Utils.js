export function capitalize(str) {
  const upperFirstLetter = str.charAt(0).toUpperCase();
  const restOfName = str.slice(1);
  return upperFirstLetter + restOfName;
}

export function roundUp(num, precision) {
  precision = Math.pow(10, precision);
  return (Math.ceil(num * precision) / precision).toFixed(2);
}
