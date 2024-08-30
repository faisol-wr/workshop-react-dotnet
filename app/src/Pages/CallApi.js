import axios from "axios";
import { useEffect, useState } from "react";

function CallApi() {
  const [list, setList] = useState([]);

  useEffect(() => {
    fechData();
  }, []);

  const fechData = async () => {
    try {
      await axios
        .get("https://fakestoreapi.com/products")
        .then((res) => {
          setList(res.data);
        })
        .catch((err) => {
          throw err.response.err;
        });
    } catch (e) {
      console.log(e.message);
    }
  };
  return (
    <>
      {list.map((item) => (
        <div>Title: {item.title}</div>
      ))}
    </>
  );
}

export default CallApi;
