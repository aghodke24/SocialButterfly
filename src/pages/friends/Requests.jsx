import React, { useState,useEffect } from 'react';
import './Requests.css';
import axios from 'axios';
import Topbar from '../../components/topbar/Topbar';
import { useNavigate } from 'react-router-dom';

const MyFriends = () => {

const id = sessionStorage.getItem('Id');
const [requests,setRequests] = useState([]);
const navigate = useNavigate()

useEffect(()=>{
    axios.get(`https://localhost:7151/api/Friends/${id}/requestuser`).then(response => {
        setRequests(response.data.value);
      })
      .catch(error => {
        console.log(error);
      });
  }, [id]);
    
  function Accept(id){
        axios.post(`https://localhost:7151/api/Friends/${id}`)
            .then(response => {
                console.log(response.data.value);
                window.location.reload();
                if(response===null){
                    navigate('/')
                }
            })
  }

  return (
    <center>
    <Topbar/>
    <div style={{marginTop:"25px"}} class="row">
    {

    requests.map((friend)=>(

<div id="fb" key={friend.id} class="col-6">
  <div id="fb-top">
    <p><b>Friend Requests</b><span>Find Friends &bull; Settings</span></p>
  </div>
  <img src={friend.imagePath} height="100" width="100" alt="I"/>
  <p id="info"><b>{friend.name}</b> <br/> <span>{friend.information}</span></p>
  <div id="button-block">
    <button id="confirm" onClick={() => Accept(friend.id)}>Confirm</button>
    <button id="delete">Delete Request</button>
  </div>
</div>

    ))}
</div>
</center>

  )
}

export default MyFriends;