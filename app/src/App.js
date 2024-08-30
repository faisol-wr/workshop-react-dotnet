import { useState } from "react";

function App() {
  const [counter, setCounter] = useState(0);

  return (
    <>
      <div>Counter = {counter}</div>
      <button onClick={(e) => setCounter(counter + 1)}>Click Here</button>
    </>
  );
}

export default App;
