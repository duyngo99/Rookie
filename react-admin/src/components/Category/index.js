import React, { useState, useEffect } from 'react'
import { useHistory } from 'react-router'
import axios from 'axios'
import { Table, Button } from 'reactstrap'


function Index() {
    
    const history = useHistory()
    const btnCreate = () => {
        history.push("/category/add")
    }
    
    const [categoryList, setCategoryList] = useState([])
    useEffect(() => {
        axios.get("https://slash1999.azurewebsites.net/api/categories").then(response => {
            setCategoryList(response.data)
            console.log(    "List")
            console.log(categoryList)
        }

        )
    }, [])

    return (
        <div className="row">
            <div className="col-md-2"></div>
            <div className="col-md-8" >
                <Table>
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

                                </tr>
                            )
                        }
                    </tbody>
                </Table>
                <Button color="danger" onClick={btnCreate}>Create Category</Button>
            </div>
            <div className="col-md-2"></div>
        </div>
    );
}

export default Index
