import React from 'react'
import { Form, FormGroup, Label, Input, Button } from 'reactstrap'
import { Link } from 'react-router-dom';

function add() {
    return (

        <div className="col-md-3">
            <Form>
                <FormGroup >
                    <Label>Product Name</Label>
                    <Input type="text" placeholder="Enter Product Name "></Input>
                </FormGroup>
                <FormGroup>
                    <Label>Product Description</Label>
                    <Input type="text" placeholder="Enter Description "></Input>
                </FormGroup>
                <FormGroup>
                    <Label>Product Price</Label>
                    <Input type="text" placeholder="Enter Price "></Input>
                </FormGroup>
                <Button type="submit" className="btn btn-primary">
                    Submit
                </Button>
                <Link to="/"><Button className="btn btn-danger">Cancel</Button></Link>
            </Form>
        </div>
    )
}

export default add
