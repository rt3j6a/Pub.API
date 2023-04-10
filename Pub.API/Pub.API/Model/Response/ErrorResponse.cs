namespace Pub.API.Model.Response {
    public class ErrorResponse {
        public int ErrorCode { get; set;}

        public string Message { get; set;}

        
        public override string ToString() {
            return string.Format("ErrorCode:{0}\nMessage:{1}", ErrorCode, Message);
        }

    }
}
