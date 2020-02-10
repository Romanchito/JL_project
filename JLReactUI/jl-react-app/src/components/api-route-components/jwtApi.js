import BaseApi from './baseApi';

export default class JwtApi extends BaseApi {

    JWT_URI = "Auth/jwtToken";
    JWT_NAME = 'your-jwt';

    getJwtToken(auth_data) {
        return super.client_call(this.JWT_URI, auth_data, "POST");
    }

    getLocalStorageToken() {
        return localStorage.getItem(this.JWT_NAME);
    }

    setLocalStorageToken(data) {
        localStorage.setItem(this.JWT_NAME, data);
    }

    removeJwtToken(){
        localStorage.removeItem(this.JWT_NAME);
    }
}