import React, {Component} from 'react';
  
let axios;
export class Film extends Component{
        
    constructor(props){        
        super(props);
        this.state = {film:{}};
    }

    componentDidMount(){ 
        const {match: {params}} = this.props;    
        axios.get(`/api/users/${params.id}`)
        .then(({ data: user }) => {
          console.log('user', user);
    
          this.setState({ user });
        });   
        this.refreshList();
    }

    refreshList(){
       
        fetch('https://localhost:44327/api/Films/6')
        .then(response => response.json())
        .then(data => {
            this.setState({film:data});
        });


    }

   
    render(){
        
        const {film} = this.state;

          return(
           
            <div>
    <p>{film.country}</p>
    <p>i</p>
          </div>
        )
    }
}