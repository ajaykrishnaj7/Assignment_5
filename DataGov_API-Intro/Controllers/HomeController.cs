using DataGov_API_Intro.DataAccess;
using DataGov_API_Intro.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataGov_API_Intro.Controllers
{
    public class HomeController : Controller
    {
        HttpClient httpClient;

        static string BASE_URL = "https://api.ftc.gov/v0";
        static string API_KEY = "haeV87u8y41GaQLUfqOTKyLx596euJAK3MFWbFap"; //Add your API key here inside ""

        //static string BASE_URL = "https://data.cdc.gov/api/views/hk9y-quqm/rows.json";
        // Obtaining the API key is easy. The same key should be usable across the entire
        // data.gov developer network, i.e. all data sources on data.gov.
        // https://www.nps.gov/subjects/developer/get-started.htm

        public ApplicationDbContext dbContext;

        public HomeController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public async Task<IActionResult> Index()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            // string NATIONAL_PARK_API_PATH = BASE_URL + "/parks?limit=20";
            string DNC_COMPLAINTS_API_PATH = BASE_URL + "/hsr-early-termination-notices";
           JsonResult  dncComplaintsData = null;

            // Parks parks = null;
            DNCComplaints dncComplaints = null;

            httpClient.BaseAddress = new Uri(DNC_COMPLAINTS_API_PATH);
            //httpClient.BaseAddress = new Uri(BASE_URL);

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(DNC_COMPLAINTS_API_PATH)
                                                        .GetAwaiter().GetResult();
                //HttpResponseMessage response = httpClient.GetAsync(BASE_URL)
                //                                        .GetAwaiter().GetResult();

                string a = String.Empty;

                if (response.IsSuccessStatusCode)


                {

                     a = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    a = a.Replace("-", String.Empty);
                   // dncComplaintsData = a ;

                  


                }

                if (!a.Equals(""))
                {
                    // JsonConvert is part of the NewtonSoft.Json Nuget package
                   var b = JsonConvert.DeserializeObject<D>(a);

                    dbContext.DNCComplaints.AddRange(b.data);
                    await dbContext.SaveChangesAsync();

                }

                
            }
            catch (Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
            }

            return View(dncComplaints);
        }
    }
}




public class Rootobject
{
    public Meta meta { get; set; }
    public object[][] data { get; set; }
}

public class Meta
{
    public View view { get; set; }
}

public class View
{
    public string transaction_number { get; set; }
    public string title { get; set; }
    public string created { get; set; }
    public string updated { get; set; }
    public string acquiredparty { get; set; }
    public int acquiringparty { get; set; }
    
}

public class License
{
    public string name { get; set; }
    public string termsLink { get; set; }
}

public class Metadata
{
    public Custom_Fields custom_fields { get; set; }
    public string[] availableDisplayTypes { get; set; }
}

public class Custom_Fields
{
    public DataQuality DataQuality { get; set; }
    public CommonCore CommonCore { get; set; }
}

public class DataQuality
{
    public string UpdateFrequency { get; set; }
    public string GeographicCoverage { get; set; }
}

public class CommonCore
{
    public string ContactEmail { get; set; }
    public string Footnotes { get; set; }
    public string ContactName { get; set; }
    public string ProgramCode { get; set; }
    public string Publisher { get; set; }
    public string BureauCode { get; set; }
}

public class Owner
{
    public string id { get; set; }
    public string displayName { get; set; }
    public string screenName { get; set; }
    public string type { get; set; }
    public string[] flags { get; set; }
}

public class Query
{
}

public class Tableauthor
{
    public string id { get; set; }
    public string displayName { get; set; }
    public string screenName { get; set; }
    public string type { get; set; }
    public string[] flags { get; set; }
}

public class Approval
{
    public int reviewedAt { get; set; }
    public bool reviewedAutomatically { get; set; }
    public string state { get; set; }
    public int submissionId { get; set; }
    public string submissionObject { get; set; }
    public string submissionOutcome { get; set; }
    public int submittedAt { get; set; }
    public int workflowId { get; set; }
    public Submissiondetails submissionDetails { get; set; }
    public Submissionoutcomeapplication submissionOutcomeApplication { get; set; }
    public Submitter submitter { get; set; }
}

public class Submissiondetails
{
    public string permissionType { get; set; }
}

public class Submissionoutcomeapplication
{
    public int endedAt { get; set; }
    public int failureCount { get; set; }
    public int startedAt { get; set; }
    public string status { get; set; }
}

public class Submitter
{
    public string id { get; set; }
    public string displayName { get; set; }
}

public class Column
{
    public int id { get; set; }
    public string name { get; set; }
    public string dataTypeName { get; set; }
    public string fieldName { get; set; }
    public int position { get; set; }
    public string renderTypeName { get; set; }
    public Format format { get; set; }
    public string[] flags { get; set; }
    public string description { get; set; }
    public int tableColumnId { get; set; }
    public Cachedcontents cachedContents { get; set; }
}

public class Format
{
    public string view { get; set; }
    public string align { get; set; }
}

public class Cachedcontents
{
    public string non_null { get; set; }
    public object largest { get; set; }
    public string _null { get; set; }
    public Top[] top { get; set; }
    public object smallest { get; set; }
    public string cardinality { get; set; }
}

public class Top
{
    public object item { get; set; }
    public string count { get; set; }
}

public class Grant
{
    public bool inherited { get; set; }
    public string type { get; set; }
    public string[] flags { get; set; }
}