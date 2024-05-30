namespace PloomesAPI.Model.ViewModel
{
    public class MongoLogsViewModel
    {
        public Log logs { get; set; }
        public enum Log
        {
            ClienteLog,
            UsuarioLog
        }
    }
}
