function MyLoop() {
  const arr = ["java", "php", "c#", "C/C++", "Python"];
  return (
    <>
    <div>Data in arr</div>
      {arr.map((item) => (
        <div>{item}</div>
      ))}
    </>
  );
}

export default MyLoop;
