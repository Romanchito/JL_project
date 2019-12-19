import BaseApi from './baseApi';

export default class ReviewsApi extends BaseApi {

    REVIEW_URI = "Reviews";

    getAllReviewsOfUser() {
        return super.client_call(this.REVIEW_URI + "/reviewsOfUser", null, "GET");
    }

    addReview(review) {
        return super.client_call(this.REVIEW_URI + "/newReview", review, "POST" );
    }

}