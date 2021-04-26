import React, { useState,useEffect } from 'react'
import { FormGroup, Label, Input, Button, } from 'reactstrap'
import { Link, useHistory } from 'react-router-dom';
import axios from 'axios'
import Select from 'react-select'
function Add() {

    const imgDefault = '/img/noImage.jpg'
    const history = useHistory()
    const [addProduct, setAddProduct] = useState({
        Name: '', Price: 0, Description: '', CategoryID: 0, RatingAVG: 0, Image: '', ImageFile: null, ImageSrc: ''
    })
    const [categoryList, setCategoryList] = useState([])
    useEffect(() => {
        axios.get(process.env.REACT_APP_LOCAL_CATEGORY).then(response => {
            setCategoryList(response.data)
        })
    }, [])
    var option=[]
    categoryList.map(x=>option.push({value:x.categoryID,label:x.name}))
    const add = (e) => {
        e.preventDefault()
        addProduct.CategoryID=addProduct.CategoryID.value
        const formData = new FormData()
        formData.append('Name', addProduct.Name)
        formData.append('Price', parseFloat(addProduct.Price))
        formData.append('Description', addProduct.Description)
        formData.append('RatingAVG', addProduct.RatingAVG)
        formData.append('CategoryID', parseInt(addProduct.CategoryID))
        formData.append('Image', addProduct.Image)
        formData.append('ImageFile', addProduct.ImageFile)
        console.log(addProduct)
        axios.post(process.env.REACT_APP_LOCAL_PRODUCT, formData).then(window.location.href = "/product")
    }

    const showPreview = e => {
        if (e.target.files && e.target.files[0]) {
            let imageFile = e.target.files[0];
            const reader = new FileReader();
            reader.onload = x => {
                setAddProduct({
                    ...addProduct,
                    ImageFile: imageFile,
                    ImageSrc: x.target.result
                })
            };
            reader.readAsDataURL(imageFile)
        }
        else {
            setAddProduct({
                ...addProduct,
                ImageFile: null,
                ImageSrc: imgDefault
            })
        }
    }

    const onChange = e => {
        const { name, value } = e.target
        setAddProduct({ ...addProduct, [name]: value })
    }
    const onSelect=CategoryID=>{
        setAddProduct({...addProduct,CategoryID})
    }
    const {CategoryID}=addProduct
    return (

        <div className="row">
            <div className="col-md-1"></div>
            <div className="col-md-10">
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
                        {/* <Label>Product Category</Label>
                        <Input name="CategoryID" type="" placeholder="Enter CategoryID " onChange={onChange}></Input> */}
                        <Select options={option}
                            onChange={onSelect}
                            value={CategoryID} 
                        />

                   
                    </FormGroup>
                    <FormGroup>
                        <Label>Product Image</Label>
                        <Input type="file" accept="image/*" onChange={showPreview}></Input>
                        <img src={addProduct.ImageSrc} width="100px"></img>
                    </FormGroup>
                    <Button type="submit" className="btn btn-primary">
                        Submit
                </Button>
                    <Link to={"/product"}><Button className="btn btn-danger">Cancel</Button></Link>
                </form>
            </div>
            <div className="col-md-1"></div>
        </div>

    )
}

export default Add
