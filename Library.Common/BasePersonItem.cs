namespace Library.Common
{
    public abstract class BasePersonItem : BaseNameItem
    {
        public string Email { get; set; }
        public bool? IsAdmin { get; set; }
    }
}