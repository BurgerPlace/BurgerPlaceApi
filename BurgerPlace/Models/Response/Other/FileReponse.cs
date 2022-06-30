namespace BurgerPlace.Models.Response.Other
{
    public class FileReponse
    {
        public class CantUpload
        {
            public string message { get; set; } = "Can't upload file";
        }

        public class InvalidFileType
        {
            public string message { get; set; } = "Invalid type of file";
        }

        public class Success
        {
            private static string _fileName = null!;
            private static string _outputName = null!;
            public Success(string fileName, string outputName)
            {
                _fileName = fileName;
                _outputName = outputName;
            }
            public Success(string fileName)
            {
                _fileName = fileName;
                _outputName = _fileName;
            }
            public string message { get; set; } = $"Successfully uploaded file {_fileName} as {_outputName}";
        }

        public class InvalidLenght
        {
            public string message { get; set; } = "Invalid file length";
        }
    }
}
