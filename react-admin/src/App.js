
import { useEffect, useState } from 'react';
import { BrowserRouter, Route, Switch, useHistory } from 'react-router-dom';
import './App.css';
import Header from './components/Header';

import Product from './components/Product/Index'
import AddProduct from './components/Product/Add'

import UpdateProduct from './components/Product/Update'

import Category from './components/Category/Index'
import AddCategory from './components/Category/Add'

import UpdateCategory from './components/Category/Update'


import Oidc, { UserManager } from 'oidc-client'

import Login from './components/Login/Login'
import LoginCallback from './components/Login/LoginCallback'

import Logout from './components/Logout/Logout'
import LogoutCallBack from './components/Logout/LogoutCallBack'

import axios from 'axios';
import User from './components/User/Index';

require('dotenv').config()


function App() {
  const config = {
    userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }),
    authority: `${process.env.REACT_APP_LOCAL_API}`,
    client_id: "react_admin",
    redirect_uri: `${process.env.REACT_APP_HOME}/signin-oidc`,
    post_logout_redirect_uri: `${process.env.REACT_APP_HOME}/signout-oidc`,
    response_type: "id_token token",
    scope: "openid profile rookieshop.api",
  }
  var userManager = new Oidc.UserManager(config)
  userManager.getUser().then(user => {
    if (user) {
      localStorage.setItem("User", user.profile.role)
      axios.defaults.headers.common["Authorization"] = "Bearer " + user.access_token
    }
  })
  var user = localStorage.getItem("User")
  console.log(user)
  if (user != "Admin") {
    return (
      <BrowserRouter>
        <Switch>
          <Route exact path="/"><Login userManager={userManager}></Login></Route>
          <Route exact path="/signin-oidc" component={LoginCallback}></Route>
        </Switch>
      </BrowserRouter>
    );
  }
  return (
    <div className="App">
      <BrowserRouter>
        <Header></Header>
        <Switch>
          <Route exact path="/" ><Product ></Product></Route>
          <Route exact path="/product" ><Product ></Product></Route>
          <Route path="/product/add" ><AddProduct></AddProduct></Route>
          <Route path="/product/update/:id" ><UpdateProduct></UpdateProduct></Route>

          <Route exact path="/category"><Category></Category></Route>
          <Route path="/category/add"><AddCategory></AddCategory></Route>
          <Route path="/category/update/:id"><UpdateCategory></UpdateCategory></Route>

          <Route exact path="/user"><User></User></Route>
          <Route exact path="/signin-oidc" component={LoginCallback}></Route>
          <Route exact path="/logout" ><Logout userManager={userManager}></Logout></Route>
          <Route exact path="/signout-oidc" component={LogoutCallBack}></Route>
        </Switch>
      </BrowserRouter>
    </div>
  );
}

export default App;
