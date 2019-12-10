import BaseApi from './baseApi';

export default class FilmApi extends BaseApi {

    FILM_URI = 'Films'

   
    getAllFilms(){
        return super.client_call(this.FILM_URI);
    }
}