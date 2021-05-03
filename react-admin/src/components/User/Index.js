import React, { useEffect, useState } from 'react'
import { useHistory , Link} from 'react-router-dom'
import axios from 'axios'

import { Button, Table } from 'reactstrap'

export default function User() {
    const [userList, setUserList] = useState([])
    useEffect(() => {
        axios.get(process.env.REACT_APP_LOCAL_USER).then(response => {
            setUserList(response.data)
        })
    }, [])


    return (
        <div div className="row">
            <div className="col-md-1"></div>
            <div className="col-md-10" >
                <Table bordered className="table table-hover ">
                    <thead>
                        <tr className="table-warning">
                            <th>UserID</th>
                            <th>UserName</th>
                            <th>Password</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            userList.map(User =>
                                <tr>
                                    <td>{User.id}</td>
                                    <td>{User.name}</td>
                                    <td>{User.password}</td>
                                </tr>
                            )
                        }
                    </tbody>
                </Table>
                <Link to={"/"}><Button className="btn btn-danger">Cancel</Button></Link>

            </div>
            <div className="col-md-1"></div>
        </div>
    )
}
