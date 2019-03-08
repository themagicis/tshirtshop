import React, { Component } from 'react';
import {inject} from 'mobx-react'

@inject("api")
export default class AdminCategories extends Component{
    constructor(props){
        super(props);
        this.state = {
            categories: []
        }
    }

    componentDidMount(){
        this.props.api.admin.getCategories(this.props.match.params.name).then(resp =>{
            this.setState({categories:resp});
        });
    }

    render(){
        let tableBody = this.state.categories.map(r => {
            return (
                <tr key={r.id}>
                    <td>{r.name}</td>
                    <td>{r.description}</td>
                    <td>
                        <button className="btn btn-sm btn-warning">Edit</button>
                        &nbsp;&nbsp;
                        <button className="btn btn-sm btn-danger">Delete</button>
                    </td>
                </tr>
            )
        })
        return (
            <div>
                <h4 className="my-3">Editing values for attribute: {this.props.match.params.name}</h4>
                <table className="table mt-3">
                    <thead>
                        <tr>
                            <th scope="col">Category Name</th>
                            <th scope="col">Category Description</th>
                            <th scope="col">Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {tableBody}
                    </tbody>
                </table>
                <h5>Add new category:</h5>
                <div className="row">
                    <div className="col-md-3">
                        <input type="text" className="form-control" placeholder="[value]"></input>
                    </div>
                    <div className="col-md-3">
                        <button className="btn btn-primary">Add</button>
                    </div>
                </div>
                <br/>
            </div>
        )
    }
}