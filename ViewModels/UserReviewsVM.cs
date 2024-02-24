namespace InterakcjeMiedzyLekami.ViewModels
{
    public class UserReviewsVM
    {
        /// <summary>
        /// dodac mozliwosc przechowywania wartosci null public string? {get; set; } = null
        /// ?????????????
        /// </summary>
        public string UserName { get; set; } = null!;
        public string Review { get; set; } = null!;
        public DateTime CreationDate { get; set; } 
    }
}
