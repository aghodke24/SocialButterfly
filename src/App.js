import { Routes,Route } from "react-router-dom";
import Profile from "./pages/profile/Profile";
import Register from "./pages/register/Register";
import MyFriends from "./pages/friends/Requests";
import Home from "./pages/home/Home";

function App(){

  return(

      <Routes>
      <Route path='/' element={<Profile></Profile>} />
      <Route path='/register' element={<Register></Register>} />
      <Route path='/requests' element={<MyFriends></MyFriends>} />
      <Route path='/home' element={<Home></Home>} />



      </Routes>
   
  )
}

export default App;
