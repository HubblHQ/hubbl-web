namespace hubbl.web.models.network
{
    public class EmptyResponse : BaseResponse {

        public EmptyResponse() {
            this.status = Constants.Status.OK;
        }

    }
}