import React, { Component } from 'react';
import {NavLink, Route} from 'react-router-dom';

import AdminReports from './AdminReports'

export default class AdminPanel extends Component{
    constructor(props){
        super(props);
        this.state = {
            page: 'reports'
        }
    }
    render(){
        let match = this.props.match;
        return (
            <div className="container">
                <div className="row">
                    <div className="col-12">
                        <ul className="nav nav-pills">
                            <li className="nav-item">
                                <NavLink  className="nav-link" activeClassName='active' to={`${match.url}/reports`}>Reports</NavLink>
                            </li>
                        </ul>
                    </div>
                </div>
                <div className="row">
                    <div className="col-12">
                        <Route exact path={`${match.path}/`} component={AdminReports} />

                        <Route path={`${match.path}/reports`} component={AdminReports} />
                    </div>
                </div>
            </div>
        )
    }
}