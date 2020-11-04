export function capitalize(str) {
  const upperFirstLetter = str.charAt(0).toUpperCase();
  const restOfName = str.slice(1);
  return upperFirstLetter + restOfName;
}
