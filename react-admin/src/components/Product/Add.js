import React, { useState } from 'react'
import { FormGroup, Label, Input, Button, } from 'reactstrap'
import { Link, useHistory } from 'react-router-dom';
import axios from 'axios'
function Add() {
    const history = useHistory()
    const [addProduct, setAddProduct] = useState({
        Name: '', Price: 0, Description: '', CategoryID: 0, RatingAVG: 0, Image: null
    })
    const add = (e) => {
        e.preventDefault()
        const formData = new FormData()
        formData.append('Name', addProduct.Name)
        formData.append('Price', parseFloat(addProduct.Price))
        formData.append('Description', addProduct.Description)
        formData.append('RatingAVG', parseFloat(addProduct.RatingAVG))
        formData.append('CategoryID', parseInt(addProduct.CategoryID))
        formData.append('Image', addProduct.Image)
        // addProduct.Price = parseFloat(addProduct.Price)
        // addProduct.CategoryID = parseFloat(addProduct.CategoryID)
        console.log(formData)
        axios.post("https://localhost:5001/api/products", formData).then(history.push('/product'))
    }
    const onChange = e => {
        const { name, value } = e.target
        setAddProduct({ ...addProduct, [name]: value })
    }
    return (

        <div className="row">
            <div className="col-md-3"></div>
            <div className="col-md-6">
                <form autoComplete="off" noValidate onSubmit={add}>
                    <FormGroup >
                        <Label>Product Name</Label>
                        <Input name="Name" type="text" placeholder="Enter Product Name " onChange={onChange}></Input>
                    </FormGroup>
                    <FormGroup>
                        <Label>Product Description</Label>
                        <Input name="Description" type="text" placeholder="Enter Description " onChange={onChange}></Input>
                    </FormGroup>
                    <FormGroup>
                        <Label>Product Price</Label>
                        <Input name="Price" type="text" placeholder="Enter Price " onChange={onChange}></Input>
                    </FormGroup>
                    <FormGroup>
                        <Label>Product Category</Label>
                        <Input name="CategoryID" type="number" placeholder="Enter CategoryID " onChange={onChange}></Input>
                    </FormGroup>
                    <Button type="submit" className="btn btn-primary">
                        Submit
                </Button>
                    <Link to={"/product"}><Button className="btn btn-danger">Cancel</Button></Link>
                </form>
            </div>
            <div className="col-md-3"></div>
        </div>

    )
}

export default Add
