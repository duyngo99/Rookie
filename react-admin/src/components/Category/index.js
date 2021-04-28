import React, { useState, useEffect } from 'react'
import { useHistory } from 'react-router'
import axios from 'axios'
import { Table, Button } from 'reactstrap'



function Index() {
    
    const [categoryList, setCategoryList] = useState([])
    const history = useHistory()
    const btnCreate = () => {
        history.push("/category/add")
    }

    const btnDelete = (id) => {
        axios.delete("https://localhost:5001/api/categories/" + id).then(setCategoryList(categoryList.filter(x => x.categoryID != id)))
    }

    const btnUpdate = (id) => {
        history.push('/category/update/' + id)
    }

    useEffect(() => {
        axios.get(process.env.REACT_APP_LOCAL_CATEGORY).then(response => {
            setCategoryList(response.data)
        })
    }, [])

    return (
        <div style={{ display: "flex", flexDirection: "row", flexWrap: "wrap", justifyContent: 'center' }}>
            <div className="col-md-1" />

            <div className="col-md-10" >
                <Table >
                    <thead>
                        <tr>
                            <th>CategoryName</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            categoryList.map(category =>
                                <tr>
                                    <td>{category.name}</td>
                                    <Button onClick={() => btnDelete(category.categoryID)} color="danger">Delete</Button>
                                    <Button onClick={() => btnUpdate(category.categoryID)} color="success">Update</Button>
                                </tr>
                            )
                        }
                    </tbody>
                </Table>
                <Button color="danger" onClick={btnCreate} >Create Category</Button>

            </div>
            <div className="col-md-1"></div>
        </div>
    );
}

export default Index
