import React, {Component} from 'react';
import {Link} from 'react-router-dom';
import {inject} from 'mobx-react'

@inject("api")
class Home extends Component{
    constructor(props){
        super(props);
        this.state = {
            currentIndex: 0,
        }
    }

    componentDidMount(){
    }

    render(){
        return (
            <div className="container">
                <div className="col-12 bg-light mb-3 rounded">
                    <div id="carouselExampleIndicators" className="carousel slide">
                        <ol className="carousel-indicators">
                        </ol>
                        <div className="carousel-inner">
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}

export default Home