import { useState } from "react";
import MyComponent from "./Pages/MyComponent";

function App() {
  const [counter, setCounter] = useState(0);

  return (
    <>
      <div>Counter = {counter}</div>
      <button onClick={(e) => setCounter(counter + 1)}>Click Here</button>
      <MyComponent title="My title From App.js">
        <button>My Button From App.js</button>
      </MyComponent>
    </>
  );
}

export default App;
