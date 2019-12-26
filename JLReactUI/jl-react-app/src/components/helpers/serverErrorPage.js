import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class ErrorPage extends Component {

  render() {
    return (

      <div className="status-code-text">
  <p id="status_number">{this.props.match.params.code} status code</p>
        <p style={{ textAlign: "center", color: "green" }}>
          <Link to="/">Go to Home </Link>
        </p>
      </div>
    );
  }
}
 