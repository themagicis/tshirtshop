import React from 'react';
import {Route} from 'react-router-dom';

function PublicRoute ({component: Component, user, ...rest}) {
  return (
    <Route
      {...rest}
      render={(props) => 
        <Component {...props} />}
    />
  )
}

export default PublicRoute
