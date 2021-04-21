import React, {useEffect, useState} from 'react'
import { Form, FormGroup, Label, Input, Button, DropdownMenu, DropdownItem, } from 'reactstrap'
import { Link, useHistory } from 'react-router-dom';
import { Dropdown } from 'bootstrap';
import axios from 'axios'
function Add() {
    const history = useHistory()
    const [addProduct,setAddProduct] = useState({
        Name : '',Price : 0, Description : '', CategoryID : 0, RatingAVG:0, 
    })
    const add = () => {
        // e.preventDefault()
        // const formData = new FormData()
        // formData.append('ProductName',addProduct.productName)
        // formData.append('Price',parseFloat( addProduct.price))
        // formData.append('Description',addProduct.description)
        // formData.append('RatingAVG',parseFloat  (addProduct.ratingAVG))
        // formData.append('CategoryID',parseInt(addProduct.categoryID))
        addProduct.Price=parseFloat(addProduct.Price)
        addProduct.CategoryID=parseFloat(addProduct.CategoryID)
        console.log(addProduct)
        axios.post("https://localhost:5001/api/products",addProduct).then(history.push('/product'))
    }
    const onChange = e => {
        const {name,value}=e.target
        setAddProduct({...addProduct,[name]:value})
    }
    return (
        

        <div style={{ display: 'flex', justifyContent: 'center' }}>
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
                    <Input name="Price" type="text" placeholder="Enter Price "onChange={onChange}></Input>
                </FormGroup>
                <FormGroup>
                    <Label>Product Category</Label>
                    <Input name="CategoryID" type="number" placeholder="Enter CategoryID "onChange={onChange}></Input>
                </FormGroup>
                <Button type="submit" className="btn btn-primary">
                    Submit
                </Button>
                <Link to={"/product"}><Button className="btn btn-danger">Cancel</Button></Link>
            </form>
        </div>
    )
}

export default Add
