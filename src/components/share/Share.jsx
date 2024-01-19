import "./share.css";
import React, { useState } from 'react';
import {PermMedia, Label,Room, EmojiEmotions} from "@mui/icons-material"
import axios from "axios";

export default function Share() {

  const [description, setDescription] = useState('');
  const userId = sessionStorage.getItem('Id')
  const [imageFile, setImageFile] = useState(null);

  const image = sessionStorage.getItem('url')

    function Share(){

          const formData = new FormData();
    formData.append("description", description);
    formData.append("UserId", userId);
    formData.append("ImageFile", imageFile);
  
    
       axios.post("https://localhost:7151/api/Post", formData, {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
        }).then(response => {
          // Reload the page
          window.location.reload();
      }).catch(error => {
          // Handle errors
          console.error(error);
      });
    }
  
  return (
    <div className="share">
      <div className="shareWrapper">
        <div className="shareTop">
          <img className="shareProfileImg" src={image} alt=""   />
          <input
            placeholder="Whats in your mind? "
            className="shareInput"
            type="text"
            value={description}
            onChange={(e)=>{
              setDescription(e.target.value);
            }}
          />
        </div>
        <hr className="shareHr"/>
        <div className="shareBottom">
            <div className="shareOptions">
                <div className="shareOption">
                    <PermMedia htmlColor="tomato" className="shareIcon"/>
                    <span className="shareOptionText"> <input type="file" id="form3Example4cd" class="form-control"  onChange={(e) => {
                  setImageFile(e.target.files[0])
              }}/>Photo or Video</span>
                </div>
                <div className="shareOption">
                    <Label htmlColor="blue" className="shareIcon"/>
                    <span className="shareOptionText">Tag</span>
                </div>
                <div className="shareOption">
                    <Room htmlColor="green" className="shareIcon"/>
                    <span className="shareOptionText">Location</span>
                </div>
                <div className="shareOption">
                    <EmojiEmotions htmlColor="goldenrod" className="shareIcon"/>
                    <span className="shareOptionText">Feelings</span>
                </div>
            </div>
            <button className="shareButton" type="button" onClick={Share}>Share</button>
        </div>
      </div>
    </div>
  );
}
