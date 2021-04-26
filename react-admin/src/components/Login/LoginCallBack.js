import React from 'react'
import Oidc from 'oidc-client'
export default function LoginCallback() {
    // var callbackUrl="https://tdatadmin.z23.web.core.windows.net/signin-oidc#id_token=eyJhbGciOiJSUzI1NiIsImtpZCI6IkJGQ0M4MTkwMDIzQzcwNEMzNTA5RUNBQzhDNTYxMUE3IiwidHlwIjoiSldUIn0.eyJuYmYiOjE2MTkxOTI4MTAsImV4cCI6MTYxOTE5MzExMCwiaXNzIjoiaHR0cHM6Ly9hb2lzaGluLmF6dXJld2Vic2l0ZXMubmV0IiwiYXVkIjoicmVhY3QiLCJub25jZSI6Ik5vbmNlVmFsdWVhc2R3cXJpb2Fsa3NrYWxja2xzYWRsa3dxZGtsYXNsa2RrYXNsZGxzYSIsImlhdCI6MTYxOTE5MjgxMCwiYXRfaGFzaCI6IjU3RUdUTm9GQlREZlRsU040TU05M0EiLCJzX2hhc2giOiI4dXBLWm9OaWpPZVJPdVV4bm4xOElRIiwic2lkIjoiQTQ2NzVFQURDNzAyQTFGODlDREJEQjM2QTFGMjgyOTciLCJzdWIiOiJmODhjZDVmMi02OGVkLTQyMmQtYjY1ZS1kZjRhZWI4NjQ5OTgiLCJhdXRoX3RpbWUiOjE2MTkxOTI4MDksImlkcCI6ImxvY2FsIiwiYW1yIjpbInB3ZCJdfQ.L_hf784TV5rZfphpXjB8VMBzSMsTZh6GwyfA1BiGaMN-PproiPo-tzglCo5RmIuZkZx5ll9fzSsOrKkkPqZJLIx2rMnqYC3IJJSl8AVMzv681cB4TU8Sk5hR1AwjLX3DNNVDnhctfD7u2FmC19iwxDmnGJ_dDz3sg8qv7SVpXFs-7CYF44CzIIyQGqIpTE4-tgjpA9uPa2CCMMFKx0DOcxOVMYf4xNJlZYk3RBTzBo4jH5JkcIVg_MRmE7vCPyXat4loMsMs7t2owXYkG4s2tSCFqet_SosSqUFV7qadXSsxiXyap76zLoxixRBhQzDB0jrlLq9GbPpOoNXnlOJ31w&access_token=eyJhbGciOiJSUzI1NiIsImtpZCI6IkJGQ0M4MTkwMDIzQzcwNEMzNTA5RUNBQzhDNTYxMUE3IiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2MTkxOTI4MTAsImV4cCI6MTYxOTE5NjQxMCwiaXNzIjoiaHR0cHM6Ly9hb2lzaGluLmF6dXJld2Vic2l0ZXMubmV0IiwiYXVkIjoiaHR0cHM6Ly9hb2lzaGluLmF6dXJld2Vic2l0ZXMubmV0L3Jlc291cmNlcyIsImNsaWVudF9pZCI6InJlYWN0Iiwic3ViIjoiZjg4Y2Q1ZjItNjhlZC00MjJkLWI2NWUtZGY0YWViODY0OTk4IiwiYXV0aF90aW1lIjoxNjE5MTkyODA5LCJpZHAiOiJsb2NhbCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiZjg4Y2Q1ZjItNjhlZC00MjJkLWI2NWUtZGY0YWViODY0OTk4IiwibmFtZSI6InVzZXJAZ21haWwuY29tIiwiZW1haWwiOiJ1c2VyQGdtYWlsLmNvbSIsInJvbGUiOiJVc2VyIiwianRpIjoiRDMzRDU3REU0NjI5Rjk0NjE2QjIzRjE0QTRGMUY5OEUiLCJzaWQiOiJBNDY3NUVBREM3MDJBMUY4OUNEQkRCMzZBMUYyODI5NyIsImlhdCI6MTYxOTE5MjgxMCwic2NvcGUiOlsib3BlbmlkIiwicm9va2llLmFwaSJdLCJhbXIiOlsicHdkIl19.ojIYeoFNGhtskkYHsa3l5GmC2BuHUcEEhuZ3QUryrWB6uHRlB1HleVQiVfxvOz3ZY-5N2JCiBWN7OgZaeHHAhSYsPuD-Q18_5kaxZ7j0FUtnWmP4Q2A_CoA-UI275bbVkErQyYoh9sqTp6ZbPsFKbZ2EgFIidK53TAF7Vn0w9wdkaH8cXNPJNXMZa0gEhpYZ962Ag14I2AA42eeaupc0u1lICsr_FzSIcCrCqY8KksyCXohLbNBjDhKnYX3QkgRdh4cHSaYvWlYJSRiR6gY741MweFgx6_or5nRNe7O9vmhTTZpLIemDJw_Rvjw8l0tO3OD9o8BKlpKG8KQ9242stg&token_type=Bearer&expires_in=3600&scope=openid%20rookie.api&state=SessionValueMakeItABitLongerasdasdasfasdlkaskdqwkl&session_state=qv9ZAuzyuB_gHFjLEyLP_rHli9sSlztfzV_-2kujCuc.54845CC0160F99431E48719B53BCBC24";
    // var extractTokens=function(address) {
    //     var returnValue=address.split('#')[1];
    //     var value=returnValue.split('&');
    //     for(var i=0;i<value.length;i++)
    //     {
    //         var v=value[i];
    //         var kvPair=v.split('=');
    //         localStorage.setItem(kvPair[0],kvPair[1]);
    //     }
    //     window.location.href=process.env.REACT_APP_ADMIN_URL
    // }
    const userManager = new Oidc.UserManager({ userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }), });
    return (
        // extractTokens(window.location.href)
        userManager.signinCallback().then(window.location.href=process.env.REACT_APP_ADMIN_PRODUCT)

    )
}