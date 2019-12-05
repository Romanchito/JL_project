import React, {Component} from 'react';


let film = {
    name:'',
    director:'',
    stars:'',
    country:'',
    releaseDate:'',
    worldwideGross:'',
    filmImage: null,
    reviews: []
    };
export class Film extends Component{

    constructor(props){
       
        super(props);
        this.state = { 
            film 
        };
    }

    componentDidMount(){
        this.refreshList();
    }

    refreshList(){
        fetch('https://localhost:44327/api/Films/6')
        .then(response => response.json())
        .then(data => {
            this.setState(JSON.parse(data));
        });
    }

   
    render(){
          return(
           
            <div>
    <p>{film.country}</p>
          </div>
        )
    }
}