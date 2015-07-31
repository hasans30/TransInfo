using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.Common;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Net;
namespace BugBurnDown
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkCredential tfsCredential = CredentialCache.DefaultCredentials.GetCredential(new Uri("http://tempurl.org"),"Basic");

            TfsConfigurationServer configurationServer =
                TfsConfigurationServerFactory.GetConfigurationServer(new Uri("http://vstfmsn:8080/tfs"));

            CatalogNode configurationServerNode = configurationServer.CatalogNode;

            // Query the children of the configuration server node for all of the team project collection nodes
            ReadOnlyCollection<CatalogNode> tpcNodes = configurationServerNode.QueryChildren(
                    new Guid[] { CatalogResourceTypes.ProjectCollection },
                    false,
                    CatalogQueryOptions.None);


            foreach (CatalogNode tpcNode in tpcNodes)
            {
                Guid tpcId = new Guid(tpcNode.Resource.Properties["InstanceId"]);
                TfsTeamProjectCollection tpc = configurationServer.GetTeamProjectCollection(tpcId);
                Console.WriteLine("Collection: " + tpc.Name);


                // Get a catalog of team projects for the collection
                ReadOnlyCollection<CatalogNode> projectNodes = tpcNode.QueryChildren(
                    new[] { CatalogResourceTypes.TeamProject },
                    false, CatalogQueryOptions.None);

                // List the team projects in the collection
                foreach (CatalogNode projectNode in projectNodes)
                {
                    Console.WriteLine(" Team Project: " + projectNode.Resource.DisplayName);
                    
                }



                // Do your tpc work here.
            }




            /*

            TfsTeamProjectCollection tfs = new TfsTeamProjectCollection(
                    new Uri("http://vstfmsn:8080/tfs/MSN01"),
                    //new Uri("https://userfeedback.visualstudio.com/DefaultCollection"),
                    tfsCredential
                    );

            tfs.Authenticate();

            Console.WriteLine(tfs.HasAuthenticated);

            var workItemStore = tfs.GetService<WorkItemStore>();

            
            var project = workItemStore.Projects[0];

            QueryHierarchy queryHierarchy = project.QueryHierarchy;
            var queryFolder = queryHierarchy as QueryFolder;
            QueryItem queryItem = queryFolder["My Queries"];
            queryFolder = queryItem as QueryFolder;

            var myQuery = queryFolder["UIF_MsnSports"] as QueryDefinition;

            Dictionary<string, string> variables = new Dictionary<string, string>();
            variables.Add("project", "UIF");
            var wiCollection = workItemStore.Query(myQuery.QueryText, variables);
            Console.WriteLine("total items:" + wiCollection.Count);
            */

        }
    }
}
