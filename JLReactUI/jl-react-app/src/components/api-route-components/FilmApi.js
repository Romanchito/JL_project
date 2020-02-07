import BaseApi from './baseApi';

export default class FilmApi extends BaseApi {

    FILM_URI = 'Films'

    getAllFilmsByName(name) {
        if (name !== undefined && name !== null) {
            return super.client_call(`${this.FILM_URI}/Name/${name}`, null, "GET");
        }
    }
    getAllFilmsByType(type) {
        if (type !== undefined && type !== null) {
            return super.client_call(`${this.FILM_URI}/Type/${type}`, null, "GET");
        }
    }
    getAllFilmsByCountry(country) {
        if (country !== undefined && country !== null) {
            return super.client_call(`${this.FILM_URI}/Country/${country}`, null, "GET");
        }
    }

    getFilmsAttributes() {
        return super.client_call(`${this.FILM_URI}/Inform`, null, "GET");
    }

    getFilmById(id) {
        return super.client_call(`${this.FILM_URI}/${id}`, null, "GET");
    }
}