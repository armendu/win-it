namespace Common.Extensions
{
    public class ModelErrors
    {
        /// <summary>
        /// Use the following code to get the errors from the model binding inside the controllers.
        /// </summary>
        public void ShowErrors()
        {
//            var errors = ModelState.Values.SelectMany(v => v.Errors);
//            foreach (var error in errors)
//            {
//                _logger.Log(LogLevel.Error, $"The following error occurred: {error.ErrorMessage}\n");
//            }
        }
    }
}