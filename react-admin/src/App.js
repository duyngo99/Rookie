
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

require('dotenv').config()


function App() {

  return (
    <div className="App">
      <BrowserRouter>
        <Header></Header>
        <Switch>
          <Route exact path ="/"></Route>
          <Route exact path="/product" ><Product ></Product></Route>
          <Route path="/product/add" ><AddProduct></AddProduct></Route>
          <Route path="/product/update/:id" ><UpdateProduct></UpdateProduct></Route>
   
          <Route exact path="/category"><Category></Category></Route>   
          <Route path="/category/add"><AddCategory></AddCategory></Route>
          <Route path="/category/update/:id"><UpdateCategory></UpdateCategory></Route>
    
        </Switch>
      </BrowserRouter>
    </div>
  );
}

export default App;
