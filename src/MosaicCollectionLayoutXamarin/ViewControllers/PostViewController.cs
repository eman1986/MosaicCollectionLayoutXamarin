using System;
using System.Collections.Generic;
using MosaicCollectionLayoutXamarin.Models;
using MosaicCollectionLayoutXamarin.TableView.Cells;
using MosaicCollectionLayoutXamarin.TableView.Sources;
using UIKit;

namespace MosaicCollectionLayoutXamarin.ViewControllers
{
    public partial class PostViewController : UIViewController
    {
        private List<Post> _posts;
        private PostTableViewSource _postTableViewSource;

        public PostViewController()
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected PostViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _postTableViewSource = new PostTableViewSource();

            tblMain.RegisterNibForCellReuse(PostViewCell.Nib, PostViewCell.Key);
            tblMain.Source = _postTableViewSource;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            _posts = new List<Post>
            {
                new Post
                {
                    ProfileImageUrl = "https://randomuser.me/api/portraits/men/32.jpg",
                    Fullname = "Tom Sanders",
                    PostDate = "Moments Ago",
                    HeaderText = "Love the new office, great job to everyone who made it possible.",
                    ImageUrls = new List<string>
                    {
                        "https://images.unsplash.com/photo-1562184647-5772b5348713?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1395&q=80"
                    }
                },
                new Post
                {
                    ProfileImageUrl = "https://randomuser.me/api/portraits/women/68.jpg",
                    Fullname = "Lisa Sims",
                    PostDate = "1 Hour Ago",
                    HeaderText = "Happy birthday Lizzy, hope you have a wonderful birthday.",
                    ImageUrls = new List<string>
                    {
                        "https://images.unsplash.com/photo-1504196606672-aef5c9cefc92?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1350&q=80",
                        "https://images.unsplash.com/photo-1464349153735-7db50ed83c84?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1431&q=80"
                    }
                },
                new Post
                {
                    ProfileImageUrl = "https://randomuser.me/api/portraits/men/61.jpg",
                    Fullname = "Robert Hill",
                    PostDate = "5 Hours Ago",
                    HeaderText = "I'd like to welcome Mindy Thomas to the company as our new HR Manager, welcome aboard.",
                    ImageUrls = new List<string>
                    {
                        "https://images.unsplash.com/photo-1533745848184-3db07256e163?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1489&q=80",
                        "https://images.unsplash.com/photo-1489533119213-66a5cd877091?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1351&q=80",
                        "https://images.unsplash.com/photo-1560265036-021b3652b490?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1490&q=80"
                    }
                },
                new Post
                {
                    ProfileImageUrl = "https://randomuser.me/api/portraits/women/76.jpg",
                    Fullname = "Lisa Sims",
                    PostDate = "1 Day Ago",
                    HeaderText =
                        "Love the new computers we got in the office, now we can work faster and better.",
                    ImageUrls = new List<string>
                    {
                        "https://images.unsplash.com/photo-1517694712202-14dd9538aa97?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1350&q=80",
                        "https://images.unsplash.com/photo-1547658718-1cdaa0852790?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=700&q=80",
                        "https://images.unsplash.com/photo-1461749280684-dccba630e2f6?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1350&q=80",
                        "https://images.unsplash.com/photo-1498050108023-c5249f4df085?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1352&q=80",
                        "https://images.unsplash.com/photo-1484788984921-03950022c9ef?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1404&q=80",
                        "https://images.unsplash.com/photo-1542744173-05336fcc7ad4?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1291&q=80"
                    }
                }
            };

            _postTableViewSource.SetData(_posts);
            InvokeOnMainThread(tblMain.ReloadData);
        }
    }
}