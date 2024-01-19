import "./post.css";
import { MoreVert } from "@mui/icons-material";
import { useState } from "react";
import axios from "axios";
import CommentModal from "../../pages/comments/Comments";
import { useEffect } from "react";

export default function Post() {
  const [like,setLike] = useState(1)
  const [isLiked,setIsLiked] = useState(false)
  const [showCommentModal, setShowCommentModal] = useState(false);
  const id=sessionStorage.getItem('Id')

  const [data,setData] = useState([])

  useEffect(()=>{
    axios.get(`https://localhost:7151/api/Post/${id}`).then((response)=>{
      console.log(response);
      setData((existData)=>{
            return response.data;
           
            });
        });
    },[id]);

  const likeHandler =()=>{
    setLike(isLiked ? like-1 : like+1)
    setIsLiked(!isLiked)
  }


  const handleCommentButtonClick = () => {
    setShowCommentModal(true);
  };

  const handleCloseCommentModal = () => {
    setShowCommentModal(false);
  };

    // Added function to handle modal visibility

  return (
    <>
{
  data.map((data)=>(
    <div className="post" key={data.id}>

<div className="postWrapper" >
        <div className="postTop">
          <div className="postTopLeft">
            <img
              className="postProfileImg"
              src={data.user.imagePath}
              alt=""
            />
            <span className="postUsername">
              {data.user.name}
            </span>
            <span className="postDate">{data.createdAt}</span>
          </div>
          <div className="postTopRight">
            <MoreVert />
          </div>
        </div>
        <div className="postCenter">
          <span className="postText">{data.descriprion}</span>
          <img className="postImg" src={data.imagePath} alt="" />
        </div>
        <div className="postBottom">
          <div className="postBottomLeft">
            <img className="likeIcon" src="assets/like.png" onClick={likeHandler} alt="" />
            <img className="likeIcon" src="assets/heart.png" onClick={likeHandler} alt="" />
            <span className="postLikeCounter">{like} people like it</span>
          </div>
          <div className="postBottomRight">
          <button
                  className="btn btn-dark"
                  onClick={handleCommentButtonClick}
                >
                  comments
                </button>
              </div>
            </div>
          </div>
          <CommentModal
            show={showCommentModal}
            postId={data.id} // Pass the post id to CommentModal
            onClose={handleCloseCommentModal}
          />
        </div>
      ))}
    </>
  );
}



