import Mymenu from "../Components/Mymenu";

function MyIfElse() {
  const x = 10;

  return (
    <>
      <Mymenu />
      {x === 10 ? <div>X is Ten</div> : <div>No Data</div>}
    </>
  );
}

export default MyIfElse;
