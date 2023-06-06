using PropertyChanged;

namespace Bookstore_visually
{
    [AddINotifyPropertyChangedInterface]
    public class CommentInfoBase
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Text { get; set; }
        public int IdComment { get; set; }
    }
}