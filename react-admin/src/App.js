
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



import Oidc from 'oidc-client'
import Login from './components/Login/Login'
import LoginCallback from './components/Login/LoginCallBack'

require('dotenv').config()


function App() {
  const config = {
    userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }),
    authority: `${process.env.REACT_APP_LOCAL_API}`,
    client_id: "reactadmin",
    redirect_uri: `${process.env.REACT_APP_ADMIN}/signin-oidc`,
    post_logout_redirect_uri: `${process.env.REACT_APP_ADMIN}/signout-oidc`,
    response_type: "id_token token",
    scope: "openid profile rookieshop.api",
  }
  var userManager = new Oidc.UserManager(config)
  return (

    <div className="App">
      <BrowserRouter>
        <Header></Header>
        <Switch>
          <Route exact path="/"></Route>
          <Route exact path="/product" ><Product ></Product></Route>
          <Route path="/product/add" ><AddProduct></AddProduct></Route>
          <Route path="/product/update/:id" ><UpdateProduct></UpdateProduct></Route>

          <Route exact path="/category"><Category></Category></Route>
          <Route path="/category/add"><AddCategory></AddCategory></Route>
          <Route path="/category/update/:id"><UpdateCategory></UpdateCategory></Route>

          <Route exact path="/login"><Login userManager={userManager}></Login></Route>
          <Route exact path="/signin-oidc"><LoginCallback></LoginCallback></Route>

        </Switch>
      </BrowserRouter>
    </div>
  );
}

export default App;
