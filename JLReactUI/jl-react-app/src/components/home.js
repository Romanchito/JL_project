import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import FilmApi from './api-route-components/FilmApi'

export class Home extends Component {

    constructor(props) {
        super(props);
        this.state = { films: [] }
    }

    componentDidMount() {
        this.refreshList();
    }

    refreshList() {
        new FilmApi().getAllFilms()            
            .then(d => {
                this.setState({ films: d })
            }
            );
    }

    render() {
        const { films } = this.state;
        return (

            <div className="main_films_block">
                {films.map((film) =>

                    <div key={film.id} className="film_block">
                        <Link key={film.id} to={{ pathname: `/film/${film.id}` }}>
                            <img alt={film.name + " image"} src={film.filmImageUrl} />
                        </Link>
                    </div>
                )}
            </div>
        )
    }
}