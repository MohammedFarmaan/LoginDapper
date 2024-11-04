import React, { useState } from 'react'
import { api } from '../axiosConfig';

const Register = () => {
    const [username, setUsername] = useState('')
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')

   const handleSubmit = (e) =>{
        e.preventDefault();
        const userData = {username, email, password};
        console.log(userData);

        // fetch('http://localhost:5066/api/User', {
        //     method: 'POST',
        //     headers: {"Content-Type": "application/json"},
        //     body: JSON.stringify(formData)
        // }).then(() => {
        //     console.log("form added");
        // })

        api.post('/', userData)
        .then(response => {
            console.log("User added:", response.data);
            alert("User added successfully");
        })
           
        .catch(error => {
            console.error("There was an error adding the user!");
            alert(error.response.data);
            console.error( error.response.data);
        });
   }

  return (
    <div>
        <h1>Register</h1>
        <form onSubmit={handleSubmit}>
            <label htmlFor="username">Username: </label>
            <input type="text" name="username" id="username" required onChange={(e) => setUsername(e.target.value)}/>
            <br/>
            <label htmlFor="email">Email: </label>
            <input type="email" name="email" id="email" required onChange={(e) => setEmail(e.target.value)}/>
            <br/>
            <label htmlFor="password">Password: </label>
            <input type="password" name="password" id="password" required onChange={(e) => setPassword(e.target.value)}/>
            <br />
            <button type='submit'>
            Register
            </button>
        </form>

        <div className="">
            <p>If already registered <a href="/">Login</a></p>
        </div>
    </div>
  )
}

export default Register