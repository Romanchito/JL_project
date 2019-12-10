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
        console.log(new FilmApi().getAllFilms());
        fetch('https://localhost:44327/api/Films')
            .then(response => response.json())
            .then(data => {
                this.setState({ films: data });
            });
    }


    render() {
        const { films } = this.state;
        return (

            <div className="main_film_block">
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