import React, { useState } from 'react'
import { FormGroup, Label, Input, Button, } from 'reactstrap'
import { Link, useHistory, useParams } from 'react-router-dom';
import axios from 'axios'
function Update() {
    const history = useHistory()
    const [addCategory, setAddCategory] = useState({ Name: '' })
    let { id } = useParams()
    const add = (e) => {
        e.preventDefault()
        const formData = new FormData()
        formData.append("Name", addCategory.Name)
        axios.put(process.env.REACT_APP_LOCAL_CATEGORY + `/` + id, formData).then(window.location.href = "/category")
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

export default Update
