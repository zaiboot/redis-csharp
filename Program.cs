using System;
using FreeRedis;

namespace redis_csharp
{
  public class Program
  {
    private const string KEY_NAME = "Test01";
    public static void Main(string[] args)
    {
      using (var cli = new RedisClient("127.0.0.1:6379"))
      {
        //cli.Serialize = obj => JsonConvert.SerializeObject(obj);
        //cli.Deserialize = (json, type) => JsonConvert.DeserializeObject(json, type);
        cli.Notice += (s, e) => Console.WriteLine(e.Log); //print command log

        cli.RPush(KEY_NAME, "ONE ELEMENT");

        string value1 = cli.RPop(KEY_NAME);
        Console.WriteLine(value1);
      }

    }
  }
}
