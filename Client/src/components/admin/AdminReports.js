import React, { Component } from 'react';
import {Link} from 'react-router-dom';
import {inject} from 'mobx-react'

@inject("api")
export default class AdminReports extends Component{
    constructor(props){
        super(props);
        this.state = {
            reports: []
        }
    }

    componentDidMount(){
        this.props.api.admin.getReports().then(resp =>{
            this.setState({reports:resp});
        });
    }

    render(){
        let reports = this.state.reports.map(r => {
            return (
                <tr key={r.id}>
                    <td>{r.id}</td>
                    <td>{r.reportedBy}</td>
                    <td>{new Intl.DateTimeFormat('en-GB', { 
                            year: 'numeric', 
                            month: 'long', 
                            day: '2-digit',
                            hour: '2-digit',
                            minute: '2-digit'
                            }).format(new Date(r.reportedDate))}</td>
                    <td>
                        <button className="btn btn-sm btn-primary">Lock</button>
                    </td>
                </tr>
            )
        })
        return (
            <table className="table mt-3">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Repored By</th>
                        <th scope="col">Date Reported</th>
                        <th scope="col">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {reports}
                </tbody>
            </table>
        )
    }
}