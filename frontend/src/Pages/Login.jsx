import React, { useState } from 'react'
import { api } from '../axiosConfig';

const Login = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleSubmit = (e) =>{
        e.preventDefault();
        const userData = {email, password};
        // console.log(userData);

        api.post('/login', userData)
        .then(response => {
            alert("Login successful")
            console.log("user logged in successfully", response)})
        .catch(error => {console.log(error);
            alert(error.response.data);
        }
 
    )
    }

  return (
    <div> <h1>Login</h1>
    <form onSubmit={handleSubmit}>
        <label htmlFor="email">Email: </label>
        <input type="email" name="email" id="email" required onChange={(e) => setEmail(e.target.value)}/>
        <br/>
        <label htmlFor="password">Password: </label>
        <input type="password" name="password" id="password" required onChange={(e) => setPassword(e.target.value)}/>
        <br />
        <button type='submit'>
        Login
        </button>
    </form>

    <div className="">
        <p>If not registered <a href="/register">Register</a></p>
    </div></div>
  )
}
export default Login