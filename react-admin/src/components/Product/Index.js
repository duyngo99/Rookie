import React, { useEffect, useState } from 'react'
import { useHistory } from 'react-router-dom'
import axios from 'axios'

import { Button, Table } from 'reactstrap'


function Index() {
    const [categoryList, setCategoryList] = useState([])
    useEffect(() => {
        axios.get(process.env.REACT_APP_LOCAL_CATEGORY).then(response => {
            setCategoryList(response.data)
        })
    },[])
    const [productList, setProductList] = useState([])
    const history = useHistory()
    const btnCreate = () => {
        history.push("/product/add")
    }

    const btnDelete = (id) => {
        // axios.delete(process.env.REACT_APP_LOCAL_PRODUCT + `/` + id).then(setProductList(productList.filter(x => x.productID != id)))
        axios.delete(process.env.REACT_APP_LOCAL_PRODUCT + '/' + id).then(setProductList(productList.filter(x => x.productID != id)))
    }

    const btnUpdate = (id) => {
        history.push('/product/update/' + id)
    }

    useEffect(() => {
        axios.get(process.env.REACT_APP_LOCAL_PRODUCT).then(response => {
            setProductList(response.data)
        }
        )
    },[])
    
    return (
        <div className="row">
            <div className="col-md-2"></div>
            <div className="col-md-8" >
                <Table>
                    <thead>
                        <tr>
                            <th>ProductID</th>
                            <th>ProductName</th>
                            <th>Price</th>
                            <th>Description</th>
                            <th>Category</th>
                            <th>Image</th>
                            <th>Function</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            productList.map(product =>
                                <tr>
                                    <td>{product.productID}</td>    
                                    <td>{product.productName}</td>
                                    <td>{product.price}</td>    
                                    <td>{product.description}</td>
                                    <td>{categoryList.find(x=>x.categoryID==product.categoryID) != undefined?categoryList.find(x=>x.categoryID==product.categoryID).name:''}</td>
                                    <td><img src={process.env.REACT_APP_LOCAL_IMAGE + product.image} width="100px"></img></td>
                                    <Button onClick={() => btnDelete(product.productID)} color="danger">Delete</Button>
                                    <Button onClick={() => btnUpdate(product.productID)} color="success">Update</Button>                               
                                </tr>
                            )
                        }
                    </tbody>
                </Table>
                <Button color="danger" onClick={btnCreate}>Create Product</Button>

            </div>
            <div className="col-md-2"></div>
        </div>
    );
}

export default Index
