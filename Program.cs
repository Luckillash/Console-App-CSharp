using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.SharePoint.Client;
using System.Net;

namespace Office365.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            GetFiles();
        }

        public static void GetFiles()
        {

            string sourceSiteUrl = "https://*********.sharepoint.com/sites/RecordCentre";

            string userName = "Sathish@*********.onmicrosoft.com";
            string password = "*********";

            var clientContext = new AuthenticationManager().GetACSAppOnlyContext("https://contoso.sharepoint.com/sites/dev", "app id", "app secret", AzureEnvironment.Production);

            Web web = clientContext.Web;
            clientContext.Load(web);
            clientContext.Load(web.Lists);
            clientContext.Load(web, wb => wb.ServerRelativeUrl);
            clientContext.ExecuteQuery();

            List list = web.Lists.GetByTitle("MyRecordLibrary");
            clientContext.Load(list);
            clientContext.ExecuteQuery();

            Folder folder = web.GetFolderByServerRelativeUrl(web.ServerRelativeUrl + "/MyRecordLibrary/Folder1/");
            clientContext.Load(folder);
            clientContext.ExecuteQuery();

            CamlQuery camlQuery = new CamlQuery();
            camlQuery.ViewXml = @"<View Scope='Recursive'>
                                     <Query>
                                     </Query>
                                 </View>";
            camlQuery.FolderServerRelativeUrl = folder.ServerRelativeUrl;
            ListItemCollection listItems = list.GetItems(camlQuery);
            clientContext.Load(listItems);
            clientContext.ExecuteQuery();

        }

    }
}




