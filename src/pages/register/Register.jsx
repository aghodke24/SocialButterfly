import React, { useState } from 'react';
import { useNavigate,Link } from "react-router-dom";
import 'react-datepicker/dist/react-datepicker.css';
import axios from 'axios';


function Register(){

  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const role="User";
  const [dob, setDob] = useState("");
  const [imageFile, setImageFile] = useState(null);
  const [information, SetInformation] = useState("");


  
    const navigate = useNavigate();
    
    function registerUser(){

      const formattedDate = new Date(dob).toLocaleDateString('en-US');
            
          const formData = new FormData();
    formData.append("Name", name);
    formData.append("Email", email);
    formData.append("Password", password);
    formData.append("Dob", formattedDate);
    formData.append("Role", role);
    formData.append("ImageFile", imageFile);
    formData.append("Information", information);

  
    
       axios.post("https://localhost:7151/api/Account/register", formData, {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      });
      alert('Registration successful!');
      navigate('/')
    } 
  
  
    return (


      <section class="vh-100" style={{backgroundColor:"#eee"}}>
  <div class="container h-100">
    <div class="row d-flex justify-content-center align-items-center h-100">
      <div class="col-lg-12 col-xl-11">
        <div class="card text-black" style={{borderRadius:"25px"}}>
          <div class="card-body p-md-5">
            <div class="row justify-content-center">
              <div class="col-md-10 col-lg-6 col-xl-5 order-2 order-lg-1">

                <p class="text-center h1 fw-bold mb-5 mx-1 mx-md-4 mt-4">Sign up</p>

                <form class="mx-1 mx-md-4">

                  <div class="d-flex flex-row align-items-center mb-4">
                    <i class="fas fa-user fa-lg me-3 fa-fw"></i>
                    <div class="form-outline flex-fill mb-0">
                      <input type="text" id="form3Example1c" class="form-control" value={name}
                onChange={(e) => {
                  setName(e.target.value)
              }} placeholder='Your Name'/>
                    </div>
                  </div>

                  <div class="d-flex flex-row align-items-center mb-4">
                    <i class="fas fa-calendar fa-lg me-3 fa-fw"></i>
                    <div class="form-outline flex-fill mb-0">
                
                   <input type="date" id="form3Example2c" class="form-control" value={dob}
                onChange={(e) => {
                  setDob(e.target.value)
              }} placeholder='Your Dob'/> 
                    </div>
                  </div>

                  <div class="d-flex flex-row align-items-center mb-4">
                    <i class="fas fa-envelope fa-lg me-3 fa-fw"></i>
                    <div class="form-outline flex-fill mb-0">
                      <input type="email" id="form3Example3c" class="form-control" value={email}
                onChange={(e) => {
                  setEmail(e.target.value)
              }} placeholder='Your Email'/>
                    </div>
                  </div>

                  <div class="d-flex flex-row align-items-center mb-4">
                    <i class="fas fa-lock fa-lg me-3 fa-fw"></i>
                    <div class="form-outline flex-fill mb-0">
                      <input type="password" id="form3Example4c" class="form-control" value={password}
                onChange={(e) => {
                  setPassword(e.target.value)
              }} placeholder='Password'/>
                    </div>
                  </div>

                  <div class="d-flex flex-row align-items-center mb-4">
                    <i class="fas fa-file fa-lg me-3 fa-fw"></i>
                    <div class="form-outline flex-fill mb-0">
                      <input type="file" id="form3Example4cd" class="form-control"  onChange={(e) => {
                  setImageFile(e.target.files[0])
              }}/>
                    </div>
                  </div>

                  <div class="d-flex flex-row align-items-center mb-4">
                    <i class="fas fa-file fa-lg me-3 fa-fw"></i>
                    <div class="form-outline flex-fill mb-0">
                      <input type="text" id="form3Example5cd" class="form-control" value={information}  onChange={(e) => {
                  SetInformation(e.target.value)
              }} placeholder="Information"/>
                    </div>
                  </div>

                  <div class="form-check d-flex justify-content-center mb-5">
                    <input class="form-check-input me-2" type="checkbox" value="" id="form2Example3c" />
                    <label class="form-check-label" for="form2Example3">
                      I agree all statements in <a href="#!">Terms of service</a>
                    </label>
                  </div>

                  <div class="d-flex justify-content-center mx-4 mb-3 mb-lg-4">
                    <button type="button" class="btn btn-primary btn-lg" onClick={registerUser}>Register</button>
                  </div>
                
                  <p>
                       Already registered <Link to='/'>Login</Link>
                   </p>
    

                </form>

              </div>
              <div class="col-md-10 col-lg-6 col-xl-7 d-flex align-items-center order-1 order-lg-2">

                <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-registration/draw1.webp"
                  class="img-fluid" alt=''/>

              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</section>
       
      );
    }
   
export default Register;
