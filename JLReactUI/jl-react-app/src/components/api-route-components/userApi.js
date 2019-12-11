import BaseApi from './baseApi';

export default class UserApi extends BaseApi {

    USER_URI = 'Users'

   
    getUserByLogin(user_login){
        return super.client_call(this.USER_URI, user_login);
    }

    addNewUser(user){
        return super.client_call(this.USER_URI, user);
    }
}