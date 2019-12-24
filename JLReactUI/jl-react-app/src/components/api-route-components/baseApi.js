import { getJwt } from '../helpers/jwtHelper';

export default class BaseApi {
    BASE_URI = 'https://localhost:44327/api/'

    async  client_call(apiURI, data, method_type) {

        let FINAL_URI = this.BASE_URI + apiURI;     
        const res = fetch(FINAL_URI,
            {
                method: method_type,
                body: data,
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
                return response.json()
            })

        return await res;
    }
}

