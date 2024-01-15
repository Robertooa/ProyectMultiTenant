namespace ProyectMultiTenant.CrossCutting
{
    public class Constants
    {
        public class ProcessMessage
        {
            public const string MSG_PROCESS_SUCCESSFULLY_COMPLETED = "Proceso realizado satisfactoriamente.";
            public const string MSG_PROCESS_SUCCESSFULLY_COMPLETED_WITH_NUMBER = "Proceso realizado satisfactoriamente.\nNro registro : {0}";
            public const string MSG_RECORD_NOT_FOUND = "Error no existe el registro contactese con el Administrador";
        }
        public class Token
        {
            public const int INDEX_START_OF_TOKEN = 7;
        }
        public class StateCodeResult
        {
            public const int STATE_CODE_OK = 200;
            public const int BAD_REQUEST = 400;
            public const int UNAUTHORIZED_ACCESS = 401;
            public const int FILE_NOT_FOUND = 404;
            public const int ERROR_EXCEPTION = 500;
        }
    }
}
