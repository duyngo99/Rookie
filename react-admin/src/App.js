
import { useEffect, useState } from 'react';
import { BrowserRouter, Route, Switch, useHistory } from 'react-router-dom';
import './App.css';
import Header from './components/Header';

import Product from './components/Product/Index'
import AddProduct from './components/Product/Add'
import DeleteProduct from './components/Product/DeleteProduct'
import UpdateProduct from './components/Product/Update'

import Category from './components/Category/Index'
import AddCategory from './components/Category/Add'
import DeleteCategory from './components/Category/DeleteCategory'
import UpdateCategory from './components/Category/Update'

import axios from 'axios'

function App() {

  return (
    <div className="App">
      <BrowserRouter>
        <Header></Header>
        <Switch>
          <Route exact path ="/"><Product></Product></Route>
          <Route exact path="/product" ><Product ></Product></Route>
          <Route path="/product/add" ><AddProduct></AddProduct></Route>
          <Route path="/product/update" ><UpdateProduct></UpdateProduct></Route>
          <Route path="/product/delete"><DeleteProduct></DeleteProduct></Route>    


          <Route exact path="/category"><Category></Category></Route>   
          <Route path="/category/add"><AddCategory></AddCategory></Route>
          <Route path="/category/update"><UpdateCategory></UpdateCategory></Route>
          <Route path="/category/delete"><DeleteCategory></DeleteCategory></Route>
        </Switch>
      </BrowserRouter>
    </div>
  );
}

export default App;
