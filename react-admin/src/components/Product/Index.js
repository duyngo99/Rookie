import React, { useEffect, useState } from 'react'
import { useHistory } from 'react-router-dom'
import axios from 'axios'

import { Button, Table } from 'reactstrap'


function Index() {

    const history = useHistory()
    const btnCreate = () => {
        history.push("/product/add")
    }
    const [productList, setProductList] = useState([])
    useEffect(() => {
        axios.get("https://localhost:5001/api/products").then(response => {
            setProductList(response.data)
            console.log("List")
            console.log(productList)
        }

        )
    },[])
    return (
        <div className="row">
        <div className="col-md-3"></div>
        <div className="col-md-6" >
            <Table>
                <thead>
                    <tr>
                        <th>ProductName</th>
                        <th>ID</th>
                        <th>Price</th>
                        <th>Des</th>
                        <th>Cate</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        productList.map(product=>
                            <tr>
                                <td>{product.productName}</td>
                                <td>{product.productID}</td>
                                <td>{product.price}</td>
                                <td>{product.description}</td>
                                <td>{product.categoryID}</td>
                            </tr>
                            )
                    }
                </tbody>
            </Table>
            <Button color="danger" onClick={btnCreate}>Create Product</Button>
        </div>
        <div className="col-md-3"></div>
        </div>
    );
}

export default Index
