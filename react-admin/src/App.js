
import { useEffect, useState } from 'react';
import { BrowserRouter, Route, Switch, useHistory } from 'react-router-dom';
import './App.css';
import Header from './components/Header';

import Product from './components/Product/Index'
import AddProduct from './components/Product/Add'
import DeleteProduct from './components/Product/DeleteProduct'
import UpdateProduct from './components/Product/update'


import Category from './components/Category'
import axios from 'axios'

function App() {

  return (
    <div className="App">
      <BrowserRouter>
        <Header></Header>
        <Switch>
          <Route exact path ="/"><Product></Product></Route>
          <Route exact path="/product" ><Product ></Product></Route>
          <Route path="/product/add" component={AddProduct}></Route>
          <Route path="/product/update" component={UpdateProduct}></Route>
          <Route path="/product/delete" component={DeleteProduct}></Route>       
        </Switch>
      </BrowserRouter>
    </div>
  );
}

export default App;
