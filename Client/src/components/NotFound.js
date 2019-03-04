import React from 'react';
import {Link} from 'react-router-dom';

function NotFound(props) {
    return (
        <div className="container">
            <div className="row">
                <div className="col-12">
                    <h1>This page does not exist. Click <Link to="/">here</Link> to go the home page.</h1>
                </div>
            </div>
        </div>
    )
}

export default NotFound