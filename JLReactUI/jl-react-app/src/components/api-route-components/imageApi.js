import BaseApi from './baseApi';

export default class ImageApi extends BaseApi {

    USER_URI = 'Image'


    getUserImage() {
        let uri = this.USER_URI + "/userImage";
        return super.client_call(uri, null, "GET");
    }

    getFilmImage(id) {
        let uri = this.USER_URI + "/filmImage/" + id;
        return super.client_call(uri, null, "GET");
    }

    uploadUserAccountImage(image) {
        let uri = this.USER_URI + "/uploading/";
        return super.client_call(uri, image, "POST");
    }
}