using TshirtMaker.Models.Core;
using TshirtMaker.Models.Social;
using TshirtMaker.PublicData;
using TshirtMaker.Tests;

namespace TshirtMaker.Services;

public class TestDataService
{


    public List<Design> GetAllDesigns() => TestUsers.Designs;


}
