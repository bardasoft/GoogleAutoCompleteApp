using GoogleAutoCompleteApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace GoogleAutoCompleteApp.Helpers
{
    public class SearchHelper
    {
        static HttpClient client = new HttpClient();
        static SearchHelper()
        {
            //client.BaseAddress = new Uri("http://suggestqueries.google.com/complete/search");
        }
        public static async Task<IEnumerable<string>> Search(string searchtext, string lang = "en")
        {
            try
            {
                var response = await client.GetAsync($"http://suggestqueries.google.com/complete/search?q={searchtext}&hl={lang}&client=firefox");
                if (!response.IsSuccessStatusCode)
                    return null;
                var result = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(await response.Content.ReadAsStringAsync());             
                
                return result[1].Select( s => ((JValue)s)?.Value?.ToString());                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
