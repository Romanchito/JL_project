import BaseApi from './baseApi';

export default class UserApi extends BaseApi {

    USER_URI = 'Users';

    getUserByLogin(user_login) {
        let uri = this.USER_URI + "/" + user_login;
        return super.client_call(uri, null, "GET");
    }

    addNewUser(user) {
        let uri = this.USER_URI + "/newUser"
        return super.client_call(uri, user, "POST");
    }

    updateUser(user, id) {
        let uri = this.USER_URI + "/updatingUser/" + id;
        return super.client_call(uri, user, "PUT");
    }
}