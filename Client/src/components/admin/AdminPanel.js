import React, { Component } from 'react';
import {NavLink, Route} from 'react-router-dom';

import AdminAttributes from './AdminAttributes'
import AdminAttributeValues from './AdminAttributeValues'
import AdminDepartments from './AdminDepartments'
import AdminCategories from './AdminCategories'

export default class AdminPanel extends Component{
    constructor(props){
        super(props);
    }
    render(){
        let match = this.props.match;
        return (
            <div className="container">
                <h2>TShirtShop Admin</h2>
                <div className="row">
                    <div className="col-12">
                        <ul className="nav nav-pills">
                            <li className="nav-item">
                                <NavLink  className="nav-link" to={`${match.url}/attributes`}>Attributes</NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink  className="nav-link" to={`${match.url}/departments`}>Departments</NavLink>
                            </li>
                        </ul>
                    </div>
                </div>
                <div className="row">
                    <div className="col-12">
                        <Route exact path={`${match.path}/attributes`} component={AdminAttributes} />
                        <Route path={`${match.path}/attributes/:name/values`} component={AdminAttributeValues} />
                        <Route exact path={`${match.path}/departments`} component={AdminDepartments} />
                        <Route path={`${match.path}/departments/:name/categories`} component={AdminCategories} />
                    </div>
                </div>
            </div>
        )
    }
}