using Newtonsoft.Json;

namespace KFitServer.Services
{
    public class YoutubeServices
    {
        private const string youTubeVideo = "https://www.youtube.com/watch?v=";
        private const string youTubeAPI = "https://www.googleapis.com/youtube/v3/playlistItems?";


        public static async Task<YPlaylist> GetVideoOnPlaylistAsync(string playlistId)
        {
            var parameter = new Dictionary<string, string>()
            {
                ["key"] = "AIzaSyB6fEqlel3KUkVJHTsi9hYrjAxVycgHUEA",
                ["playlistId"] = playlistId,
                ["part"] = "snippet",
                /*["fields"] = "items/snippet(title,description,thumbnails/standard/url)"*/

            };

            string fullUrl = MakeUrlFromQuery(parameter);

            var result = await new HttpClient().GetStringAsync(fullUrl);

            if (result != null)
            {
                return JsonConvert.DeserializeObject<YPlaylist>(result);
            }

            return null;
        }

        private static string MakeUrlFromQuery(Dictionary<string, string> parameter)
        {
            if (string.IsNullOrEmpty(youTubeAPI))
            {
                throw new ArgumentNullException(nameof(youTubeAPI));
            }

            if (parameter == null || parameter.Count == 0)
            {
                return youTubeAPI;
            }

            return parameter.Aggregate(youTubeAPI,
                (accumulated, kvp) => string.Format($"{accumulated}{kvp.Key}={kvp.Value}&"));
        }
    }
}
