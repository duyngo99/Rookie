import React from 'react'

export default function Login(props) {
    return (
        <div>
            {props.userManager.signoutRedirect()}
        </div>
    )
}