using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using MosaicCollectionLayoutXamarin.Models;
using MosaicCollectionLayoutXamarin.TableView.Cells;
using UIKit;

namespace MosaicCollectionLayoutXamarin.TableView.Sources
{
    public class PostTableViewSource : UITableViewSource
    {
        private List<Post> _posts;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var postItem = _posts.ElementAtOrDefault(indexPath.Row);
            var cell = tableView.DequeueReusableCell(PostViewCell.Key, indexPath) as PostViewCell;

            cell?.SetData(postItem);

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _posts?.Count ?? 0;
        }

        public override nint NumberOfSections(UITableView tableView) => 1;

        public void SetData(List<Post> posts)
        {
            _posts = posts;
        }
    }
}