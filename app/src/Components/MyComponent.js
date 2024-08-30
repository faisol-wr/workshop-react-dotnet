function MyComponent(props) {
  return (
    <>
      <div>My Component</div>
      <div>{props.title}</div>
      <p>{props.children}</p>
    </>
  );
}

export default MyComponent;
