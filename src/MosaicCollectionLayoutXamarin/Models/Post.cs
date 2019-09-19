using System.Collections.Generic;

namespace MosaicCollectionLayoutXamarin.Models
{
    public class Post
    {
        public string ProfileImageUrl { get; set; }

        public string Fullname { get; set; }

        public string PostDate { get; set; }

        public string HeaderText { get; set; }

        public List<string> ImageUrls { get; set; }
    }
}