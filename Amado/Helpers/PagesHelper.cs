namespace Amado.Helpers
{
    public class PagesHelper
    {
        public static string IsPageActive(string currentPage, string targetPage)
        {
            return currentPage == targetPage ? "active" : "";
        }
    }
}
