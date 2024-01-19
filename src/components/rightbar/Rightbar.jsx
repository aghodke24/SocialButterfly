import "./rightbar.css";
import { Users } from "../../dummyData";
import Online from "../online/Online";
import { useState,useEffect } from "react";
import axios from "axios";

export default function Rightbar({ profile }) {
  const HomeRightbar = () => {
    
      

    return (
      <>
        <div className="birthdayContainer">
          <img className="birthdayImg" src="assets/gift.png" alt="" />
          <span className="birthdayText">
            <b>Pola Foster</b> and <b>3 other friends</b> have a birhday today.
          </span>
        </div>
        <img className="rightbarAd" src="assets/ad.png" alt="" />
        <h4 className="rightbarTitle">Online Friends</h4>
        <ul className="rightbarFriendList">
          {Users.map((u) => (
            <Online key={u.id} user={u} />
          ))}
        </ul>
      </>
    );
  };

  const ProfileRightbar = () => {

    const [friends,setFriends] = useState([]);
    const id = sessionStorage.getItem('Id')

    useEffect(()=>{
      axios.get(`https://localhost:7151/api/Friends/${id}/myfriends`).then(response => {
        setFriends(response.data.value);
        })
        .catch(error => {
          console.log(error);
        });
    }, [id]);

    return (
      <>
        <h4 className="rightbarTitle">User information</h4>
        <div className="rightbarInfo">
          <div className="rightbarInfoItem">
            <span className="rightbarInfoKey">City:</span>
            <span className="rightbarInfoValue">New York</span>
          </div>
          <div className="rightbarInfoItem">
            <span className="rightbarInfoKey">From:</span>
            <span className="rightbarInfoValue">Madrid</span>
          </div>
          <div className="rightbarInfoItem">
            <span className="rightbarInfoKey">Relationship:</span>
            <span className="rightbarInfoValue">Single</span>
          </div>
        </div>
        <h4 className="rightbarTitle">User friends</h4>
        <div className="rightbarFollowings">
          
        {
  friends.map((f)=>(
<div className="rightbarFollowing" key={f.id}>
            <img
              src={f.imagePath}
              alt=""
              className="rightbarFollowingImg"
            />
            <span className="rightbarFollowingName">{f.name}</span>
          </div>
  ))
}
        </div>



      </>
    );
  };
  return (
    <div className="rightbar">
      <div className="rightbarWrapper">
        {profile ? <ProfileRightbar /> : <HomeRightbar />}
      </div>
    </div>
  );
}
