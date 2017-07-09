using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;

namespace HelloWorldRansom
{
    public class Ransom
    {
        public IEnumerable<string> GetRansomNote(string demand, string languageOfVictimsRichParents, string flickrKey, string translateKey)
        {
            //Translate the demand into the language of the victims rich parents.
            demand = Translate(demand, "EN", languageOfVictimsRichParents, translateKey);

            //Get rid of any commas or full stops etc
            demand = Regex.Replace(demand, "[^A-Za-z _]", "").ToUpper();

            //Get Library of letter images from Flickr One Letter Pool.
            //https://www.flickr.com/groups/oneletter/pool/
            var letterImages = new List<string>();
            var uri = @"https://api.flickr.com/services/rest/?method=flickr.photos.search&per_page=500&group_id=27034531%40N00&extras=tags&api_key=" + flickrKey + "&format=json";

            ImageModel library;
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(uri).Result;
                var result = response.Content.ReadAsStringAsync().Result.ToString();
                result = result.Substring(14, result.Length - 15); //HACK: Remove the "jsonFlickrApi(" from the start and the ")" from the end
                library = JsonConvert.DeserializeObject<ImageModel>(result);
            }

            foreach (var letter in demand)
            {
                //Find each matching letter image from the library based on the title and return a url for it
                IEnumerable<string> letterImage = library.Photos.Photo
                    .Where(p => p.Title.ToUpper() == "LETTER " + letter.ToString() || p.Title.ToUpper() == letter.ToString())
                    .Select(p => @"http://farm" + p.Farm + ".static.flickr.com/" + p.Server + "/" + p.Id + "_" + p.Secret + "_s.jpg");

                if (letterImage.Any())
                {
                    //Because Random isn't very Random without this
                    Thread.Sleep(10);

                    //Choose random letter out of the matching possibilities
                    letterImages.Add(letterImage.ElementAt(new Random().Next(0, letterImage.Count())));
                }
                else
                {
                    //null will be translated to a space of 75px
                    letterImages.Add(null);
                }
            }

            return letterImages;
        }

        private static string Translate(string text, string from, string to, string translateKey)
        {
            //Call to Microsoft Translator Api with secure token
            string uri = "https://api.microsofttranslator.com/v2/http.svc/Translate?appid=Bearer%20" + GetToken(translateKey) + "&text=" + System.Net.WebUtility.HtmlEncode(text) + "&from=" + from + "&to=" + to;

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(uri).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                
                //Parse value from Xml result
                var xmlResult = XDocument.Parse(result);

                return xmlResult.Root.Value;
            }
        }

        private static string GetToken(string translateKey)
        {
            //HACK: Need this empty Content to make the POST call.
            var values = new Dictionary<string, string>
            {
                { "", "" }
            };
            var content = new FormUrlEncodedContent(values);

            //Call to Azure Cognitive Services with subscription key to get a Bearer Token
            using (var client = new HttpClient())
            {
                var response = client.PostAsync("https://api.cognitive.microsoft.com/sts/v1.0/issueToken?Subscription-Key=" + translateKey, content).Result;

                return response.Content.ReadAsStringAsync().Result;
            }
        }

        private struct ImageModel
        {
            public Photos Photos { get; set; }
            public string Stat { get; set; }
        }

        private struct Photos
        {
            public int Page { get; set; }
            public int Pages { get; set; }
            public int PerPage { get; set; }
            public int Total { get; set; }
            public List<Photo> Photo { get; set; }
        }

        private struct Photo
        {
            public string Id { get; set; }
            public string Owner { get; set; }
            public string Secret { get; set; }
            public string Server { get; set; }
            public string Farm { get; set; }
            public string Title { get; set; }
            public int IsPublic { get; set; }
            public int IsFriend { get; set; }
            public int IsFamily { get; set; }
            public string Tags { get; set; }
        }

    }
}
