import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import FilmApi from './api-route-components/FilmApi'

export class Home extends Component {

    constructor(props) {
        super(props);
        this.state = { films: [], countries: [], types: [] }
    }

    componentDidMount() {
        this.refreshList();
    }

    showCountriesBlock(id) {
        if (id === 'country-box') {
            document.getElementById('type-box').style.display = 'none';
        }
        else {
            document.getElementById('country-box').style.display = 'none';
        }
        this.addData(id);

        let e = document.getElementById(id);
        if (e.style.display === 'none') {
            e.style.display = 'block';
        }
        else {
            e.style.display = 'none';
        }
    }

    addData = (id) => {
        let result = [];
        for (let film of this.state.films) {
            if (id === 'country-box') {
                if (!result.includes(film.country)) {
                    result.push(film.country);
                }
            }
            else {
                if (!result.includes(film.type)) {
                    result.push(film.type);
                }
            }
        }
        if (id === 'country-box') this.setState({ countries: result });
        else this.setState({ types: result })
    }



    refreshList = () => {
        new FilmApi().getAllFilms()
            .then(d => {
                this.setState({ films: d })
            }
            );

    }

    render() {
        return (

            <div className="main-films-block">
                <div className="main-search-block">
                    <input type="submit" onClick={() => this.showCountriesBlock('country-box')} className="submitB" value="Country" />
                    <input type="submit" onClick={() => this.showCountriesBlock('type-box')} className="submitB" value="Type" />
                    <input type="submit" className="submitB" value="Name" />
                    <hr />
                    <div id="country-box" style={{ display: 'none' }}>
                        {
                            this.state.countries ? this.state.countries.map((ctr) => <h6>{ctr}</h6>) : ""
                        }
                    </div>
                    <div id="type-box" style={{ display: 'none' }}>
                        {
                            this.state.countries ? this.state.types.map((t) => <h6>{t}</h6>) : ""
                        }
                    </div>
                    <div id="name-box" style={{ display: 'block' }}>
                        <div class="form__group field">
                            <input type="input" class="name-search-field" placeholder="Name" name="name" id='name' required />                            
                        </div>
                    </div>

                </div>
                {this.state.films.map((film) =>

                    <div key={film.id} className="film-block">
                        <Link key={film.id} to={{ pathname: `/film/${film.id}` }}>
                            <img alt={film.name + " image"} src={film.filmImageUrl} />
                        </Link>
                    </div>

                )}
            </div>
        )
    }
}