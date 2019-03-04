import React, {Component} from 'react';
import {Link} from 'react-router-dom';
import {inject, observer} from 'mobx-react'

@inject("user")
@observer
class Footer extends Component{
    render(){
        return (
            <footer className="blog-footer">
                <p>Blog template built for <a href="https://getbootstrap.com/">Bootstrap</a> by <a href="https://twitter.com/mdo">@mdo</a>.</p>
                <p>
                    {this.props.user.isAdmin && <Link to="/admin">Admin</Link>}
                </p>
            </footer>
        )
    }
}

export default Footer