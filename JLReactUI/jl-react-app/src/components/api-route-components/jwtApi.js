import BaseApi from './baseApi';

export default class JwtApi extends BaseApi{

    JWT_URI = "Auth/jwtToken";

    getJwtToken(auth_data){
        return super.client_call(this.JWT_URI,auth_data,"POST")
    }
}