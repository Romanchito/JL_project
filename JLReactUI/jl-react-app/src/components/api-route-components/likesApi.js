import BaseApi from './baseApi';

export default class LikeApi extends BaseApi {

    LIKE_URI = "Likes";    

    addLike(like) {
        return super.client_call(this.LIKE_URI + "/newLike", like, "POST");
    }
}