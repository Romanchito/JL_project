import React, {Component} from 'react';
import { Link } from 'react-router-dom';

export  class NotFoundPage extends Component{
    render(){
        return( <div>
            
            <p style={{textAlign:"center", color:"green"}}>
              <Link to="/">Go to Home </Link>
            </p>
          </div>
        );
    }
}
