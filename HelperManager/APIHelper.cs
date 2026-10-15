using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HelperManager
{
  public static class APIHelper
  {
    public static async Task<string> GetAPI(string path)
    {
      try
      {
        using (HttpClient client = new HttpClient())
        {
          HttpResponseMessage response = await client.GetAsync(path);
          response.EnsureSuccessStatusCode();
          return await response.Content.ReadAsStringAsync();
        }
      }
      catch (Exception)
      {
        throw;
      }
    }


  }
}
