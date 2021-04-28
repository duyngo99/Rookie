import React from 'react'

export default function Login(props) {
    return (
        <div>
            {   localStorage.removeItem("User"),
                props.userManager.signoutRedirect()
            }
        </div>
    )
}