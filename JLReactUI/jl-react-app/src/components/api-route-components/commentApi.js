import BaseApi from './baseApi';

export default class CommentApi extends BaseApi {

    COMMENT_URI = "Comments";

    getAllCommentsOfReview(id) {
        return super.client_call(this.COMMENT_URI + "/review/" + id, null, "GET");
    }

    addComment(comment) {
        return super.client_call(this.COMMENT_URI + "/newComment", comment, "POST");
    }
}