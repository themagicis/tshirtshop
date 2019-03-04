import React from 'react';
import {Route, Redirect} from 'react-router-dom';

function PrivateRoute ({component: Component, user, ...rest}) {
  return (
    <Route
      {...rest}
      render={(props) => 
        user.isAuthenticated === true
          ? <Component {...props} />
          : <Redirect to={{pathname: '/', state: {from: props.location}}} />}
    />
  )
}

export default PrivateRoute
