import React from 'react';
import {Route, Redirect} from 'react-router-dom';

function AdminRoute ({component: Component, user, ...rest}) {
  return (
    <Route
      {...rest}
      render={(props) => 
        user.isAuthenticated && user.isAdmin
          ? <Component {...props} />
          : <Redirect to={{pathname: '/', state: {from: props.location}}} />}
    />
  )
}

export default AdminRoute
