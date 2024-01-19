import "./profile.css";
import Topbar from "../../components/topbar/Topbar";
import Sidebar from "../../components/sidebar/Sidebar";
import Feed from "../../components/feed/Feed";
import Rightbar from "../../components/rightbar/Rightbar";
import Login from "../login/Login";


export default function Profile() {
  const image=sessionStorage.getItem("url");
  const name=sessionStorage.getItem("name");

  return (
    <>

<Login/>
      <Topbar />
      <div className="profile">
        <Sidebar />
        <div className="profileRight">
          <div className="profileRightTop">
            <div className="profileCover">
              <img
                className="profileCoverImg"
                src="assets/post/3.jpeg"
                alt=""
              />
              <img
                className="profileUserImg"
                src={image}
                alt=""
              />
            </div>
            <div className="profileInfo">
                <h4 className="profileInfoName">{name}</h4>
                <span className="profileInfoDesc">Hello my friends!</span>
            </div>
          </div>
          <div className="profileRightBottom">
            <Feed />
            
            <Rightbar profile/>
          </div>
        </div>
      </div>
    </>
  );
}
