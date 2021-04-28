import React, { useState } from 'react'
import { FormGroup, Label, Input, Button, } from 'reactstrap'
import { Link, useHistory } from 'react-router-dom';
import axios from 'axios'
function Add() {
    const history = useHistory()
    const [addCategory, setAddCategory] = useState({ Name: '' })
    const add = (e) => {
        e.preventDefault()
        const formData = new FormData()
        formData.append("Name", addCategory.Name)
        axios.post(process.env.REACT_APP_LOCAL_CATEGORY, formData).then(window.location.href = "/category")
        // axios.post(process.env.REACT_LOCAL_CATEGORY, formData).then(history.push('/category'))
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
                        <Input name="Name" type="text" placeholder="Enter Category Name " onChange={onChange}></Input>
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
