import React from 'react';
import { Redirect } from "react-router-dom";
import { getJwt } from '../helpers/jwtHelper';

export default class BaseApi{
    BASE_URI = 'https://localhost:44327/api/'

    client_call(apiURI){
        console.log('FINAL URI: ' + this.BASE_URI + apiURI);
        console.log('JWT: ' + getJwt());
        return fetch(this.BASE_URI + apiURI,
            {
                headers: {
                    "Content-Type": "application/json",
                    'Accept': 'application/json',
                    'Authorization' : getJwt() 
                }
            }).then(response => response.json())
    }    
}

