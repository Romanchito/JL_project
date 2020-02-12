import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import FilmApi from './api-route-components/FilmApi'

export class Home extends Component {


    constructor(props) {
        super(props);
        this.state = { films: [], countries: [], types: [], filmApi: new FilmApi() }
    }

    componentDidMount() {
        this.getFilmsAttributes();
    }

    showBlock(id) {
        let array = ['country-box', 'type-box', 'name-box']
        for (let index = 0; index < array.length; index++) {
            if (array[index] !== id) {
                document.getElementById(array[index]).style.display = 'none';
            }
        }

        if (document.getElementById(id).style.display === 'none') {
            document.getElementById(id).style.display = 'block';
        }
        else {
            document.getElementById(id).style.display = 'none';
        }
    }

    getFilmsAttributes = () => {
        this.state.filmApi.getFilmsAttributes().then(d => {
            this.setState({ countries: d.filmCountries, types: d.filmTypes });
        })
    }

    getFilmsByName = (name) => {
        this.state.filmApi.getAllFilmsByName(name)
            .then(d => {
                this.setState({ films: d });
            }
            );
    }

    handleChange = () => {
        let val = document.getElementById('name-search-field').value;
        this.getFilmsByName(val);
    }

    compareToSearchValue = (val) => {
        let searchVal = document.getElementById('name-search-field').value.split('');
        for (let index = 0; index < searchVal.length; index++) {
            if (searchVal[index].toLowerCase() === val.toLowerCase()) {
                return true;
            }
        }
        return false;
    }

    getFilmsByTypes = (type) => {
        this.state.filmApi.getAllFilmsByType(type).then(
            d => {
                this.setState({ films: d });
            }
        )
    }

    getFilmsByCountries = (country) => {
        this.state.filmApi.getAllFilmsByCountry(country).then(
            d => {
                this.setState({ films: d });
            }
        )
    }

    render() {
        return (
            <div className="main-films-block">
                <div className="main-search-block">
                    <input type="submit" onClick={this.showBlock.bind(this, 'country-box')} className="submitB" value="Country" />
                    <input type="submit" onClick={this.showBlock.bind(this, 'type-box')} className="submitB" value="Type" />
                    <input type="submit" onClick={this.showBlock.bind(this, 'name-box')} className="submitB" value="Name" />
                    <hr />
                    <div id="country-box" style={{ display: 'none' }}>
                        {
                            this.state.countries ? this.state.countries.map((ctr) => <h6 key={ctr} onClick={this.getFilmsByCountries.bind(this, ctr)}>{ctr}</h6>) : ""
                        }
                    </div>
                    <div id="type-box" style={{ display: 'none' }}>
                        {
                            this.state.countries ? this.state.types.map((type) => <h6 key={type} onClick={this.getFilmsByTypes.bind(this, type)}>{type}</h6>) : ""
                        }
                    </div>
                    <div id="name-box" style={{ display: 'none' }}>
                        <div>
                            <input type="input" id="name-search-field" onChange={this.handleChange} placeholder="Name" />
                        </div>
                    </div>

                </div>
                {this.state.films.map((film) =>

                    <div key={film.id} className="film-block">
                        <Link key={film.id} to={{ pathname: `/film/${film.id}` }}>
                            <img alt={film.name + " image"} src={film.filmImageUrl} />
                        </Link>
                        <table className="film-name-table">
                            <tbody>
                                <tr>
                                    {
                                        film.name.split('').map((s, index = 0) =>
                                            this.compareToSearchValue(s) ?
                                                <td key={index++} className="film-name-td-found">{s}</td> :
                                                <td key={index++}>{s}</td>
                                        )
                                    }
                                </tr>
                            </tbody>
                        </table>
                    </div>

                )}
            </div>
        )
    }
}