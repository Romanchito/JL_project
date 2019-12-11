import React, { Component } from 'react';
import { Review } from './review';
import FilmApi from './api-route-components/FilmApi';

export class Film extends Component {
    
    constructor(props) {
        super(props);
        this.state = { film: {}};

    }

    componentDidMount() {         
        this.refreshList();        
    }

    refreshList() {
        const id = this.props.match.params.id;
        new FilmApi().getFilmById(id)
        .then(response => response.json())
        .then(data => {
            this.setState({ film: data });
        });

        
      
    }

    render(id) {

        const film = this.state.film;
           
        return (

            <div className="main_film_block">
                <div className="film_information_block">
                    <div className="image_film_block">
                        <img alt={film.name} src={film.filmImageUrl} />
                    </div>
                    <div className="main_inform_film_block">
                        <h1>{film.name}</h1>
                        <hr />
                        <ul>
                            <h3><li>Director: {film.director}</li></h3>
                            <h3><li>Stars: {film.stars}</li></h3>
                            <h3><li>Date of release: {film.releaseDate}</li></h3>
                            <h3><li>World gross: {film.worldwideGross}$</li></h3>
                            <h3><li>Country: {film.country}</li></h3>
                        </ul>

                    </div>
                </div>

                <Review id = { this.props.match.params.id} />
                
            </div>
        )
    }
}