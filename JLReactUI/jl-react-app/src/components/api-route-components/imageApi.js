import BaseApi from './baseApi';
import { getJwt } from '../helpers/jwtHelper';

export default class ImageApi extends BaseApi {

    IMAGE_URI = 'Image'

    getAccountImage() {
        let uri = this.IMAGE_URI + "/accountImage";
        return super.client_call(uri, null, "GET");
    }

    getFilmImage(id) {
        let uri = this.IMAGE_URI + "/filmImage/" + id;
        return super.client_call(uri, null, "GET");
    }

    getUserImage(id){
        let uri  = this.IMAGE_URI + "/userImage/" + id;
        return super.client_call(uri,null,"GET");
    }

    uploadUserAccountImage(image) {
        fetch("https://localhost:44327/api/Image/uploading",
            {
                method: "POST",
                body: image,
                headers: {                   
                    'Authorization': getJwt()
                }
            })     
    }
}