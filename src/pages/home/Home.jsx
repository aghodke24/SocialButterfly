import Topbar from "../../components/topbar/Topbar";
import Sidebar from "../../components/sidebar/Sidebar";
import Feeds from "../../components/feed/Feeds";
import Rightbar from "../../components/rightbar/Rightbar";
import "./home.css"


export default function Home() {
  return (
    <>
      <Topbar />
      <div className="homeContainer">
        <Sidebar />
        <Feeds/>
        <Rightbar/>
      </div>
    </>
  );
}
