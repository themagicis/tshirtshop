import React, { Component } from 'react';
import {withRouter, Link} from 'react-router-dom';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Alert } from 'reactstrap';

import logo from '../logo.png'
import {inject, observer} from 'mobx-react'

@inject("user", "api")
@observer
class Header extends Component {
    constructor(props) {
        super(props);
        this.state = {
          modal: false
        };
        this.authSvc = this.props.api.auth;
    
        this.toggle = this.toggle.bind(this);
        this.login = this.login.bind(this);
        this.logout = this.logout.bind(this);
        this.handleChange = this.handleChange.bind(this);

        this.state = {
            email: '',
            password: '',
            error: ''
        }
      }
    
    toggle() {
        this.setState({
            modal: !this.state.modal
        });
    }

    handleChange(event) {
        let obj = {};
        obj[event.target.name] = event.target.value;
        this.setState(obj);
    }

    login(){
        this.setState({error:''});
        this.authSvc.login(this.state.email, this.state.password).then(resp =>{
            if (resp.success){
                this.toggle();
                this.props.user.setInfo({
                    ...resp.user,
                    token: resp.token,
                })
                window.toastr.success(resp.message);
            } else{
                this.setState({error:resp.message});
            }
        });
    }

    logout(){
        this.props.user.clearInfo();
        this.props.history.push('/');
    }

    render(){
        var isAuthenticated = this.props.user.isAuthenticated;
        let btnAction = isAuthenticated ? 
            <Button outline color="secondary" onClick={this.logout}>Log out</Button> :
            <Button outline color="success" onClick={this.toggle}>Log in</Button>
        let btnRegister = isAuthenticated ? '' : 
            <Link to={'/register'}>
                <Button outline color="info" onClick={this.register}>Register</Button>
            </Link>;
        let error = this.state.error ? 
            <div className="row justify-content-center">
                <div className="col-auto">
                    <Alert color="danger">
                        {this.state.error}
                    </Alert>
                </div>
            </div> : '';

        return (
            <header className="blog-header py-2">
                <div className="row flex-nowrap justify-content-between align-items-center">
                    <div className="col-4">
                    </div>
                    <div className="col-4 text-center">
                        <Link className="blog-header-logo text-dark" to="/">
                            <img src={logo} alt="Logo" width="120" height="40"/>
                            <span className="blog-header-title">Shop</span>
                        </Link>
                    </div>
                    <div className="col-4 d-flex justify-content-end align-items-center">
                        {btnRegister}
                        &nbsp;&nbsp;&nbsp;
                        {btnAction}
                    </div>
                </div>
                <Modal isOpen={this.state.modal} toggle={this.toggle}>
                    <ModalHeader toggle={this.toggle}>Log in to T-shirt Shop</ModalHeader>
                    <ModalBody>
                        {error}
                        <div className="row justify-content-center">
                            <div className="col-auto">
                                <form>
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
                                </form>
                            </div>
                        </div>
                    </ModalBody>
                    <ModalFooter>
                        <Button color="primary" onClick={this.login}>Login</Button>
                        {' '}
                        <Button color="secondary" onClick={this.toggle}>Cancel</Button>
                    </ModalFooter>
                </Modal>

            </header>
        )
    }
  }

  export default withRouter(Header)