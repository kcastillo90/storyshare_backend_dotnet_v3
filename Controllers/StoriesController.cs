using storyshare_backend_dotnet_v3.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace storyshare_backend_dotnet_v3.Controllers
{
  [Route("")]  // /stories
  public class StoriesController
  {
    // property holding database context
    private readonly StoryContext stories;

    // constructor to receive db context
    public StoriesController(StoryContext storiesCtx){
      stories = storiesCtx;
    }

    [HttpGet] // get request to /stories
    public IEnumerable<Story> index(){
      // return all stories
      return stories.Stories.ToList();
    }

    [HttpPost] // post request to /stories
    public IEnumerable<Story> Post([FromBody]Story Story){
      // add a story
      stories.Stories.Add(Story);
      // save changes
      stories.SaveChanges();
      // return all stories
      return stories.Stories.ToList();
    }

    [HttpGet("{id}")] // get specific story by id
    public Story show(long id){
      return stories.Stories.FirstOrDefault(story => story.id == id);
    }

    [HttpPut("{id}")] // put request to story by id
    public IEnumerable<Story> update([FromBody]Story Story, long id){
      // retrieves story to be updated
      Story oldStory = stories.Stories.FirstOrDefault(story => story.id == id);
      // update properites
      oldStory.title = Story.title;
      oldStory.author = Story.author;
      oldStory.synopsis = Story.synopsis;
      oldStory.story = Story.story;
      oldStory.user = Story.user;
      // save changes
      stories.SaveChanges();
      //return list of stories
      return stories.Stories.ToList();
    }

    [HttpDelete("{id}")]  // delete request to story by id
    public IEnumerable<Story> destroy(long id){
      Story oldStory = stories.Stories.FirstOrDefault(story => story.id == id);
      // remove from db
      stories.Stories.Remove(oldStory);
      // save changes
      stories.SaveChanges();
      // return updated list of stories
      return stories.Stories.ToList();
    }
  }
}
