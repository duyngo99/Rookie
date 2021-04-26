import React, { useState } from 'react'
import { FormGroup, Label, Input, Button, } from 'reactstrap'
import { Link, useHistory, useParams } from 'react-router-dom';
import axios from 'axios'
function Update() {
    const imgDefault = '/img/noImage.jpg'
    const history = useHistory()
    const [updateProduct, setUpdateProduct] = useState({
        Name: '', Price: 0, Description: '', CategoryID: 0, RatingAVG: 0, Image: '', ImageFile: null, ImageSrc: ''
    })
    let { id } = useParams()
    const update = (e) => {
        e.preventDefault()
        const formData = new FormData()
        formData.append('Name', updateProduct.Name)
        formData.append('Price', parseFloat(updateProduct.Price))
        formData.append('Description', updateProduct.Description)
        formData.append('RatingAVG', updateProduct.RatingAVG)
        formData.append('CategoryID', parseInt(updateProduct.CategoryID))
        formData.append('Image', updateProduct.Image)
        formData.append('ImageFile', updateProduct.ImageFile)
        console.log(updateProduct)
        axios.put(process.env.REACT_APP_LOCAL_PRODUCT + `/` + id, formData).then(window.location.href = "/product")
    }
    const onChange = e => {
        const { name, value } = e.target
        setUpdateProduct({ ...updateProduct, [name]: value })
    }

    const showPreview = e => {
        if (e.target.files && e.target.files[0]) {
            let imageFile = e.target.files[0];
            const reader = new FileReader();
            reader.onload = x => {
                setUpdateProduct({
                    ...updateProduct,
                    ImageFile: imageFile,
                    ImageSrc: x.target.result
                })
            };
            reader.readAsDataURL(imageFile)
        }
        else {
            setUpdateProduct({
                ...updateProduct,
                ImageFile: null,
                ImageSrc: imgDefault
            })
        }
    }

    return (

        <div className="row">
            <div className="col-md-3"></div>
            <div className="col-md-6">
                <form autoComplete="off" noValidate onSubmit={update}>
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
                        <Input name="CategoryID" type="text" placeholder="Enter CategoryID " onChange={onChange}></Input>
                    </FormGroup>
                    <FormGroup>
                        <Label>Product Image</Label>
                        <Input type="file" accept="image/*" onChange={showPreview}></Input>
                        <img src={updateProduct.ImageSrc} width="100px"></img>
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

export default Update
