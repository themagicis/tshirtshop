import React, { Component } from 'react';
import { Alert } from 'reactstrap';
import {inject} from 'mobx-react'

@inject("api")
export default class Register extends Component{
    constructor(props) {
        super(props);
        this.state = {
          modal: false
        };

        this.authSvc = this.props.api.auth;
    
        this.register = this.register.bind(this);
        this.handleChange = this.handleChange.bind(this);

        this.state = {
            name: '',
            email: '',
            password: '',
            errors: []
        }
      }

    handleChange(event) {
        let obj = {};
        obj[event.target.name] = event.target.value;
        this.setState(obj);
    }

    register(){
        this.authSvc.register(this.state.name, this.state.email, this.state.password, this.state.picture).then(resp =>{
            if (resp.success){
                window.toastr.success(resp.message);
                this.props.history.push('/');
            } else{
                this.setState({errors:Object.keys(resp.errors).map((k) => resp.errors[k])});
                window.toastr.error(resp.message);
            }
        })
    }

    render(){
        let error = this.state.errors.length > 0 ? 
            <div className="row justify-content-center">
                <div className="col-6">
                    {this.state.errors.map((e, ind) => {
                        return (
                            <Alert key={ind} color="danger">
                                {e}
                            </Alert>
                        )
                    })}
                </div>
            </div> : '';
        return (
            <div className="container">
                {error}
                <div className="row justify-content-center">
                    <div className="col-6 mb-5">
                        <h2>Register</h2>
                        <form>
                            <div className="form-group">
                                <label htmlFor="picture">Name</label>
                                <input type="text" className="form-control" id="name" name="name" placeholder="Name" autoComplete="off"
                                    value={this.state.name} onChange={this.handleChange}/>
                            </div>
                            <div className="form-group">
                                <label htmlFor="email">Email</label>
                                <input type="text" className="form-control" id="email" name="email" placeholder="Email" autoComplete="off"
                                    value={this.state.email} onChange={this.handleChange}/>
                            </div>
                            <div className="form-group">
                                <label htmlFor="password">Password</label>
                                <input type="password" className="form-control" id="password" name="password" placeholder="Password" autoComplete="off"
                                    value={this.state.password} onChange={this.handleChange}/>
                            </div>
                            <div className="text-center">
                                <button type="button" className="btn btn-primary" onClick={this.register}>Register</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        )
    }
}