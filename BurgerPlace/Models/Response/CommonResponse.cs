namespace BurgerPlace.Models.Response
{
    public class CommonResponse
    {
        public class Duplicated
        {
            public string message { get; set; } = "Item with this data already exists";
        }
        public class NotFoundWithThisId
        {
            public string message { get; set; } = "Item with this id can't be found!";
        }
        public class NotFoundWithThisName
        {
            public string message { get; set; } = "Item with this name can't be found!";
        }
    }
}
