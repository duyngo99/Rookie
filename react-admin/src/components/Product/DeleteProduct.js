import React from 'react'
import { Form, FormGroup, Label, Input, Button } from 'reactstrap'
import { Link, useHistory } from 'react-router-dom';
import { useEffect, useState } from 'react';

function DeleteProduct() {
    const history = useHistory()


    return (
        <div className="col-md-3">
            <Form>
                <FormGroup>
                    <Label> Are you sure that you want to delete this product ?</Label>


                </FormGroup>
            </Form>

        </div>
    )
}

export default DeleteProduct
