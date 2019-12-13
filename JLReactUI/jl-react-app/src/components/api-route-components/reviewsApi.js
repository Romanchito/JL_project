import BaseApi from './baseApi';

export default class ReviewsApi extends BaseApi {

    JWT_URI = "Reviews";

    getAllReviewsOfUser() {
        return super.client_call(this.JWT_URI + "/reviewsOfUser", null, "Get")
    }
}