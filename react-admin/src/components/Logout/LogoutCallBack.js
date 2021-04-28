import React from 'react'
import Oidc from 'oidc-client'
export default function LoginCallBack() {
    var userManager = new Oidc.UserManager({ userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }), })

    return (
        <div>
            {
                userManager.signoutRedirect().then(res => {

                    userManager.getUser().then(window.location.href = process.env.REACT_APP)
                })
            }
        </div>
    )
}
