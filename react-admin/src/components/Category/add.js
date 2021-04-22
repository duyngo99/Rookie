import React, { useState } from 'react'
import { FormGroup, Label, Input, Button, } from 'reactstrap'
import { Link, useHistory } from 'react-router-dom';
import axios from 'axios'
function Add() {
    const history = useHistory()
    const [addCategory, setAddCategory] = useState({
        Name: '', Price: 0, Description: '', CategoryID: 0, RatingAVG: 0,
    })
    const add = () => {
        // e.preventDefault()
        // const formData = new FormData()
        // formData.append('ProductName',addProduct.productName)
        // formData.append('Price',parseFloat( addProduct.price))
        // formData.append('Description',addProduct.description)
        // formData.append('RatingAVG',parseFloat  (addProduct.ratingAVG))
        // formData.append('CategoryID',parseInt(addProduct.categoryID))
        addCategory.Price = parseFloat(addCategory.Price)
        addCategory.CategoryID = parseFloat(addCategory.CategoryID)
        console.log(addCategory)
        axios.post("https://localhost:5001/api/products", addCategory).then(history.push('/product'))
    }
    const onChange = e => {
        const { name, value } = e.target
        setAddCategory({ ...addCategory, [name]: value })
    }
    return (

        <div className="row">
            <div className="col-md-3"></div>
            <div className="col-md-6">
                <form autoComplete="off" noValidate onSubmit={add}>
                    <FormGroup >
                        <Label>Category Name</Label>
                        <Input name="Name" type="text" placeholder="Enter Product Name " onChange={onChange}></Input>
                    </FormGroup>
                    <Button type="submit" className="btn btn-primary">
                        Submit
                </Button>
                    <Link to={"/category"}><Button className="btn btn-danger">Cancel</Button></Link>
                </form>
            </div>
            <div className="col-md-3"></div>
        </div>

    )
}

export default Add
