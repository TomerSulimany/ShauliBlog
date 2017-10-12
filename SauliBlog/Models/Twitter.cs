using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweetSharp;

namespace SauliBlog.Models
{
    public class Twitter
    {
        public int ID { get; set; }

        public int TweetsCount { get; set; }

        public List<TwitterStatus> getShauliTweets()
        {
            TwitterService service = new TwitterService();
            var tweets = service.Search(new SearchOptions { Q = "A" });
            List<TwitterStatus> resultList = new List<TwitterStatus>(tweets.Statuses);
            return resultList;
        }

        public Twitter()
        {
            TweetsCount = getShauliTweets().Count();
        }
    }
}