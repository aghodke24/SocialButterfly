import { useState, useEffect } from "react";
import axios from "axios";
import Modal from "react-bootstrap/Modal";
import { ModalBody } from "react-bootstrap";
import { Media } from "reactstrap";
import {Image} from "react-bootstrap";

const CommentsModal = ({ show, postId, onClose }) => {
  const [comments, setComments] = useState([]);
  const [newComment, setNewComment] = useState("");
  
  console.log(postId);

  useEffect(() => {
    axios
    .get(`https://localhost:7151/api/Comment/${postId}`)
    .then((response) => {
      console.log(response);
    setComments(response.data);
    });
    }, [postId]);
    
    

    const handleSubmit = (e) => {
    e.preventDefault();
    axios
    .post(`https://localhost:7151/api/Comment`, {
    text: newComment,
    postId: postId,
    userId: sessionStorage.getItem("Id"),
    })
    .then((response) => {
    setComments((existingComments) => [...existingComments, response.data]);
    setNewComment("");
    });
    };
    
    return (
    <Modal show={show} onHide={onClose}>
    <ModalBody>
    <span className="close" onClick={onClose}>
    ×
    </span>
    <h2>Comments</h2>
    {comments.length > 0 ? (
    comments.map((comment) => (
      <Media key={comment.id} className="d-flex align-items-start">
  <Image
    width={64}
    height={64}
    className="mr-3"
    src={comment.user.imagePath}
    roundedCircle
  />
  <div className="media-body">
  <small>{new Date(comment.createdAt).toLocaleDateString()}</small>
    <h5>{comment.user.name}</h5>
    <p>{comment.text}</p>
  </div>
</Media>
    ))
    ) : (
    <p>No comments yet.</p>
    )}
    <form>
    <label htmlFor="newComment">Add a comment:</label>
    <textarea
    id="newComment"
    value={newComment}
    onChange={(e) => setNewComment(e.target.value)}
    />
    <button type="button" onClick={handleSubmit}>Submit</button>
    </form>
    </ModalBody>
    </Modal>
    );
    };
    
    export default CommentsModal;