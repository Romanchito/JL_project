import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import FilmApi from './api-route-components/FilmApi'

export class Home extends Component {

    constructor(props) {
        super(props);
        this.state = { films: [], countries: [], types: [], viewfilms: [] }
    }

    componentDidMount() {
        this.refreshList();
    }

    handleChange = () => {
        let val = document.getElementById('name-search-field').value;
        this.refreshList(val);
    }

    showBlock(id) {
        let array = ['country-box', 'type-box', 'name-box']
        for (let index = 0; index < array.length; index++) {
            if (array[index] !== id) {
                document.getElementById(array[index]).style.display = 'none';
            }
        }
        if (id !== 'name-box') {
            this.setState({viewfilms:this.state.films});
            this.workWithCategoriesBlock(id);
        }
        else {
            this.workWithNameBlock(id);
        }
    }

    workWithNameBlock = (id) => {
        if (document.getElementById(id).style.display === 'none') {
            document.getElementById(id).style.display = 'block';
        }
        else {
            document.getElementById(id).style.display = 'none';
            this.refreshList();
        }
    }

    workWithCategoriesBlock(id) {
        if (document.getElementById(id).style.display === 'none') {
            document.getElementById(id).style.display = 'block';
        }
        else {
            document.getElementById(id).style.display = 'none';
        }
    }

    addData = () => {
        let countriesResult = [];
        let typesResult = [];
        for (let film of this.state.films) {
            if (!countriesResult.includes(film.country)) {
                countriesResult.push(film.country);
            }
            if (!typesResult.includes(film.type)) {
                typesResult.push(film.type);
            }
        }
        this.setState({ types: typesResult });
        this.setState({ countries: countriesResult });
    }

    refreshList = (name) => {
        new FilmApi().getAllFilms(name)
            .then(d => {
                this.setState({ films: d, viewfilms: d }, () => this.addData());
            }
            );
    }

    filterCountry(country) {        
        let resultArray = [];
        for (let index = 0; index < this.state.films.length; index++) {
            if (this.state.films[index].country === country) {
                resultArray.push(this.state.films[index]);
            }
        }
        this.setState({ viewfilms: resultArray });
    }
    render() {
        return (

            <div className="main-films-block">
                <div className="main-search-block">
                    <input type="submit" onClick={() => this.showBlock('country-box')} className="submitB" />
                    <input type="submit" onClick={() => this.showBlock('type-box')} className="submitB" />
                    <input type="submit" onClick={() => this.showBlock('name-box')} className="submitB" />
                    <hr />
                    <div id="country-box" style={{ display: 'none' }}>
                        {
                            this.state.countries ? this.state.countries.map((ctr) => <h6  onClick={() => this.filterCountry(ctr)}>{ctr}</h6>) : ""
                        }
                    </div>
                    <div id="type-box" style={{ display: 'none' }}>
                        {
                            this.state.countries ? this.state.types.map((t) => <h6>{t}</h6>) : ""
                        }
                    </div>
                    <div id="name-box" style={{ display: 'none' }}>
                        <div className="form__group field">
                            <input type="input" id="name-search-field" onChange={() => { this.handleChange() }} placeholder="Name" />
                        </div>
                    </div>

                </div>
                {this.state.viewfilms.map((film) =>

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