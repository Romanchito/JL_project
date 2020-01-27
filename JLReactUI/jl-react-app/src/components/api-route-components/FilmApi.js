import BaseApi from './baseApi';

export default class FilmApi extends BaseApi {

    FILM_URI = 'Films'

    getAllFilms() {
        return super.client_call(this.FILM_URI, null, "GET");
    }

    getFilmById(id) {
        return super.client_call(this.FILM_URI + "/" + id, null, "GET");
    }
}