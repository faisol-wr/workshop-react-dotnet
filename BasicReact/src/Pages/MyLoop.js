import Mymenu from "../Components/Mymenu";

function MyLoop() {
  const arr = ["java", "php", "c#", "C/C++", "Python"];
  return (
    <>
      <Mymenu />
      <div>Data in arr</div>
      {arr.map((item) => (
        <div>{item}</div>
      ))}
    </>
  );
}

export default MyLoop;
