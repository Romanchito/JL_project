import React, { Component } from 'react';
import { Review } from './review';
import FilmApi from './api-route-components/FilmApi';

export class Film extends Component {

    constructor(props) {
        super(props);
        this.state = { film: {} };

    }

    componentDidMount() {
        this.refreshList();
    }

    refreshList() {
        const id = this.props.match.params.id;
        new FilmApi().getFilmById(id)
            .then(data => {
                this.setState({ film: data });
            });
    }

    render() {

        const film = this.state.film;

        return (

            <div className="main_film_block">
                <div className="film_information_block">
                    <div className="both-block"></div>
                    <div className="image_film_block">
                        <img className="image" alt={film.name} src={film.filmImageUrl} />
                    </div>
                    <div className="main_inform_film_block">
                        <div className="inform-container">
                            {film.name}
                            <hr />
                            <ul>
                                <li>Director: {film.director}</li>
                                <li>Stars: {film.stars}</li>
                                <li>Date of release: {film.releaseDate}</li>
                                <li>World gross: {film.worldwideGross}$</li>
                                <li>Country: {film.country}</li>
                            </ul>
                        </div>
                    </div>
                    <div className="both-block"></div>
                </div>

                <Review id={this.props.match.params.id} />

            </div>
        )
    }
}