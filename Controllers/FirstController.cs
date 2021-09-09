using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections;

namespace storyshare_backend_dotnet_v3.Controllers
{
  public class FirstController: Controller
  {
    // This route would match /first/first
    public Hashtable First(){
      return new Hashtable(){{"Result", "You made a Route"}};
    }
  }
}
