import React, {Component} from 'react';
import { withRouter } from "react-router-dom";
import {getJwt} from './helpers/jwtHelper';

class AuthComponent extends Component {
    constructor(props){
        super(props);
        this.state = {
            user: undefined
        };
    }

    componentDidMount(){
        this.getUser();
    }

    getUser(){
        const jwtToken= getJwt();
        if(!jwtToken) {
            this.setState({
                user: null
            });
            return;
        }

        
        return jwtToken;
      

    }

    render() {
        const { user } = this.state;
        if (user === undefined) {
          return (
            <div>
              Loading...
            </div>
          );
        }
    
        if (user === null) {
          this.props.history.push('/log');
        }
    
        return this.props.children;
      }

}
export default withRouter(AuthComponent)
