import axios from "axios";
import { useState } from "react";
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Modal from 'react-bootstrap/Modal';
import {useNavigate } from "react-router-dom";
import { isExpired, decodeToken } from "react-jwt";

const Login = () => {

  const navigate = useNavigate();

  const [show, setShow] = useState(sessionStorage.getItem("isLoggedIn")!=='true');

  const handleClose = () =>{
    if(!email || !password){
      return false;
    }
    setShow(false);
  };


    const [email,setEmail] = useState("");
    const [password,setPassword] = useState("");


  
function loginUser(){

  const formData = new FormData();
  formData.append("email", email);
  formData.append("password", password); 


    axios.post("https://localhost:7151/api/Account/login",formData)
    .then((response)=>{
      console.log(response);
        sessionStorage.setItem("isLoggedIn", 'true');
        sessionStorage.setItem("tokens",response.data.token);
        sessionStorage.setItem("Id",response.data.id);
        sessionStorage.setItem("url",response.data.imagePath);
        const token = sessionStorage.getItem("tokens");    
        const decoded = decodeToken(token);
          const isMyTokenExpired = isExpired(token);
        console.log(decoded);
        console.log(isMyTokenExpired);



        const claimValue = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
       if(claimValue==='Admin'){
          navigate('/')
        }
        const claimName= decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']

        
        console.log(claimName);
        console.log(claimValue);

        sessionStorage.setItem('name',claimName);
        handleClose();
        window.location.reload();
   
    });

  }
return(

  <div>
<Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Login</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>Email</Form.Label>
              <Form.Control
                type="text"
                placeholder="email"
                value={email}
                onChange={(e) => {
                  setEmail(e.target.value)
              }}
                autoFocus
                style={{width:"75%"}}
              />
            </Form.Group>
            <Form.Group className="mb-1" controlId="exampleForm.ControlInput2">
              <Form.Label>Password</Form.Label>
              <Form.Control
                type="password"
                placeholder="password"
                value={password}
                onChange={(e) => {
                  setPassword(e.target.value)
              }}
              style={{width:"75%"}}

              />
            </Form.Group>
          </Form>
          <Button onClick={()=>navigate('/register')}>
             New User </Button>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button variant="primary" onClick={loginUser}>
            Signin
          </Button>
        </Modal.Footer>
      </Modal>
</div>
);
}

export default Login;
