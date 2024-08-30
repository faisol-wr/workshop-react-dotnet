import { Link } from "react-router-dom";

function Mymenu() {
  return (
    <>
      <div>
        <Link to="/">Home</Link>
        <Link to="/myLoop">My Loop</Link>
        <Link to="/myIfElse">My If Else</Link>
      </div>
    </>
  );
}

export default Mymenu;
