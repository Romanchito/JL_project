import { getJwt } from '../helpers/jwtHelper';

export default class BaseApi {
    BASE_URI = 'https://localhost:44327/api/'

    client_call(apiURI, data) {

        console.log('JWT: ' + getJwt());
        console.log('data: ' + data);
        
        let FINAL_URI = '';
        if (data === undefined) {
            FINAL_URI = this.BASE_URI + apiURI;
        }

        if (data !== undefined && data !== null) {
            FINAL_URI = this.BASE_URI + apiURI + "/" + data;
        }

        console.log('FINAL URI: ' + FINAL_URI);

        return fetch(FINAL_URI,
            {
                headers: {
                    "Content-Type": "application/json",
                    'Accept': 'application/json',
                    'Authorization': getJwt()
                }
            }).then(response => {
                if (response.status === 401) {
                    console.log("Non authorization!");
                    localStorage.clear();
                    window.location.href = '/log';
                }
                else {
                    return response;
                }
            })
    }
}

