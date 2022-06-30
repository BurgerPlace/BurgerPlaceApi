namespace BurgerPlace.Models.Response.Other
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

        public class RemovedName
        {
            private static string _name = null!;
            public RemovedName(string name)
            {
                _name = name;
            }

            public string message { get; set; } = $"Successfully removed item {_name}";
        }

        public class Updated
        {
            public string message { get; set; } = "Successfully updated item";
        }
        public class Created
        {
            public string message { get; set; } = "Successfully created new item";
        }
    }
}
