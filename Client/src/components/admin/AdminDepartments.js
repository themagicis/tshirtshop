import React, { Component } from 'react';
import {inject} from 'mobx-react'
import {NavLink} from 'react-router-dom';

@inject("api")
export default class AdminDepartments extends Component{
    constructor(props){
        super(props);
        this.state = {
            departments: []
        }
    }

    componentDidMount(){
        this.props.api.admin.getDepartments().then(resp =>{
            this.setState({departments:resp});
        });
    }

    render(){
        let match = this.props.match;
        let tableBody = this.state.departments.map(r => {
            return (
                <tr key={r.id}>
                    <td>{r.name}</td>
                    <td>{r.description}</td>
                    <td>
                        <NavLink className="btn btn-sm btn-info" to={`${match.url}/${r.name}/categories`}>Categories</NavLink>
                        &nbsp;&nbsp;
                        <button className="btn btn-sm btn-warning">Edit</button>
                        &nbsp;&nbsp;
                        <button className="btn btn-sm btn-danger">Delete</button>
                    </td>
                </tr>
            )
        })
        return (
            <div>
                <table className="table mt-3">
                    <thead>
                        <tr>
                            <th scope="col">Department Name</th>
                            <th scope="col">Department Description</th>
                            <th scope="col">Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {tableBody}
                    </tbody>
                </table>
                <h5>Add new department:</h5>
                <div className="row">
                    <div className="col-md-3">
                        <input type="text" className="form-control" placeholder="[name]"></input>
                    </div>
                    <div className="col-md-3">
                        <button className="btn btn-primary">Add</button>
                    </div>
                </div>
                <br />
            </div>
        )
    }
}