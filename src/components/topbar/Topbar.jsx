import "./topbar.css";
import { Search, Person, Chat } from "@mui/icons-material";
import NotificationsActiveIcon from '@mui/icons-material/NotificationsActive';
import { useState,useEffect } from "react";
import axios from "axios";
import { Link, useNavigate } from "react-router-dom";
import { common } from "@mui/material/colors";

export default function Topbar() {

  const navigate = useNavigate();
  const logout = () =>{
    sessionStorage.removeItem("tokens");
    sessionStorage.removeItem("isLoggedIn");
    sessionStorage.removeItem("Id");
    window.location.reload();
    navigate('/')
  }

  const [friendRequestsCount, setFriendRequestsCount] = useState(0);
  const id = sessionStorage.getItem("Id");
  const image = sessionStorage.getItem('url');

  useEffect(() => {
    const fetchFriendRequestsCount = async () => {
      try {
        const response = await axios.get(`https://localhost:7151/api/Friends/${id}/requests`);
        console.log(response.data.value);
        setFriendRequestsCount(response.data.value);
      } catch (error) {
        console.log(error);
      }
    };
    fetchFriendRequestsCount();
  }, [id]);

  return (
    <div className="topbarContainer">
      <div className="topbarLeft">
       <Link to='/'><span className="logo" >SocialButterfly</span></Link> 
      </div>
      <div className="topbarCenter">
        <div className="searchbar">
          <Search className="searchIcon" />
          <input
            placeholder="Search for friend, post or video"
            className="searchInput"
          />
        </div>
      </div>
      <div className="topbarRight">
        <div className="topbarLinks">
          <Link to="/home" style={{color:"white"}}><span className="topbarLink">Homepage</span></Link>
          <span className="topbarLink">Timeline</span>
          <button className="btn btn-primary" onClick={logout}>Logout</button>
        </div>
        <div className="topbarIcons">
          <div className="topbarIconItem">
            <Person />
            <span className="topbarIconBadge"></span>
          </div>
          <div className="topbarIconItem">
            <Chat />
            <span className="topbarIconBadge">
              
            </span>
          </div>
          <div className="topbarIconItem">
          <Link to="/requests"><NotificationsActiveIcon sx={{ color: common['white'] }} /></Link>
            {friendRequestsCount > 0 && (
            <span className="topbarIconBadge">{friendRequestsCount}</span>
            )}
          </div>
        </div>
        <Link to="/"><img src={image} alt="" className="topbarImg"/></Link>
      </div>
    </div>
  );
}
