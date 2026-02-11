using System.ComponentModel.DataAnnotations;
using TshirtMaker.Models.Core;

namespace TshirtMaker.Models.Social
{
    public class Post : BaseEntity
    {

        [Required]
        public Guid PosterId { get; set; }

        [Required]
        public Guid DesignId { get; set; }

        [MaxLength(100,ErrorMessage ="Title Exceeded The Lenght limit")]
        public string? Description { get; set; }

        public int LikesCount
        {
            get
            {
                if (Likes == null)
                {
                    return 0;

                }
                return Likes.Count;
            }

        }
        public int CommentsCount
        {
            get
            {
                return Comments.Count;
            }

        }


        public int BookmarksCount
        {
            get
            {
                return Bookmarks.Count;
            }
        }

        public int RemixCount { get; set; } = 0;

        public bool AllowRemix { get; set; } = true;

        public virtual ICollection<Like> Likes { get; set; } = [];

        public virtual ICollection<Comment> Comments { get; set; } = [];

        public virtual ICollection<Bookmark> Bookmarks { get; set; } = [];

        public virtual User? Poster { get; set; }
        public virtual Design? Design { get; set; }
    }
}
