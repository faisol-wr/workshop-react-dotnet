import config from "./../config";

function CallConfig() {
  return (
    <>
      <div>IP = {config.ip}</div>
      <div>Token = {config.tokenName}</div>
    </>
  );
}

export default CallConfig;
