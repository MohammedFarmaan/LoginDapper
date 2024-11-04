import Login from "./Pages/Login";
import Register from "./Pages/Register";
import {Route, Routes} from "react-router-dom";
function App() {
  return (
      <Routes>
      <Route path="/" element={<Login/>}/>
      <Route path="/register" element={<Register/>}/>
      </Routes>
  );
}

export default App;
