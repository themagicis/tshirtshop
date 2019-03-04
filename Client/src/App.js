import React, { Component } from 'react';
import {BrowserRouter as Router, Switch} from 'react-router-dom';
import {Provider} from 'mobx-react'

import 'bootstrap/dist/css/bootstrap.min.css';
import 'toastr/build/toastr.min.css';
import './App.css';

import PublicRoute from './components/routing/PublicRoute'
import PrivateRoute from './components/routing/PrivateRoute'
import AdminRoute from './components/routing/AdminRoute'

import Header from './components/Header';
import Footer from './components/Footer';

import Home from './components/Home';
import AdminPanel from './components/admin/AdminPanel'
import NotFound from './components/NotFound'

import UserStore from './stores/UserStore';
import ApiStore from './stores/ApiStore';

import HttpBackend from './services/HttpBackend'

class App extends Component {
  constructor(props){
    super(props);
    this.userStore = new UserStore();
    this.apiStore = new ApiStore(new HttpBackend(this.userStore));
  }

  render() {
    return (
      <Provider user={this.userStore} api={this.apiStore}>
        <Router>
          <div>
            <div className="container">
              <Header />
              <div className="jumbotron p-3 p-md-5 text-white rounded bg-dark">
                    <div className="col-md-6 px-0">
                        <h1 className="display-4 font-italic">T-shirt Shop</h1>
                        <p className="lead my-3">The best online store for t-shirts.</p>
                    </div>
                </div>
            </div>
            
            <Switch>
              <PublicRoute exact path="/" component={Home} />
              
              <AdminRoute path="/admin" component={AdminPanel} user={this.userStore} />

              <PublicRoute component={NotFound} />
            </Switch>
            
            <Footer />
          </div>
        </Router>
      </Provider>
    );
  }
}

export default App;
