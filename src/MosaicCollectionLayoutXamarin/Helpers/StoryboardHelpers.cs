using UIKit;

namespace MosaicCollectionLayoutXamarin.Helpers
{
    public static class StoryboardHelpers
    {
        private static readonly AppDelegate AppDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;

        public static T CreateViewController<T>(string storyboardName, string viewControllerStoryBoardId = null) where T : UIViewController
        {
            var storyboard = UIStoryboard.FromName(storyboardName, null);

            if (string.IsNullOrEmpty(viewControllerStoryBoardId))
            {
                return (T)storyboard.InstantiateInitialViewController();
            }

            return (T)storyboard.InstantiateViewController(viewControllerStoryBoardId);
        }
    }
}