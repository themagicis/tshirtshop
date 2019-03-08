import React, { Component } from 'react';
import {inject} from 'mobx-react'
import {NavLink} from 'react-router-dom';

@inject("api")
export default class AdminAttributes extends Component{
    constructor(props){
        super(props);
        this.state = {
            attributes: []
        }
    }

    componentDidMount(){
        this.props.api.admin.getAttributes().then(resp =>{
            this.setState({attributes:resp});
        });
    }

    render(){
        let match = this.props.match;
        let tableBody = this.state.attributes.map(r => {
            return (
                <tr key={r.id}>
                    <td>{r.name}</td>
                    <td>
                        <NavLink className="btn btn-sm btn-info" to={`${match.url}/${r.name}/values`}>Attributes</NavLink>
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
                            <th scope="col">Attribute Name</th>
                            <th scope="col">Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {tableBody}
                    </tbody>
                </table>
                <h5>Add new attribute value:</h5>
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