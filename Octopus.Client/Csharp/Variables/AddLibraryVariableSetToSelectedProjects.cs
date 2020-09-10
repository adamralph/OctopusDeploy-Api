// If using .net Core, be sure to add the NuGet package of System.Security.Permissions
//#r "path\to\Octopus.Client.dll"

using Octopus.Client;
using Octopus.Client.Model;

// Declare working varibles
var octopusURL = "https://youroctourl";
var octopusAPIKey = "API-YOURAPIKEY";
string spaceName = "default";
string projectName = "MyProject";
string librarySetName = "MyLibrarySet";

// Create repository object
var endpoint = new OctopusServerEndpoint(octopusURL, octopusAPIKey);
var repository = new OctopusRepository(endpoint);
var client = new OctopusClient(endpoint);

try
{
    // Get space
    var space = repository.Spaces.FindByName(spaceName);
    var repositoryForSpace = client.ForSpace(space);

    // Get project
    var project = repositoryForSpace.Projects.FindByName(projectName);

    // Get libarary set
    var librarySet = repositoryForSpace.LibraryVariableSets.FindByName(librarySetName);

    // Include library set to project
    project.IncludedLibraryVariableSetIds.Add(librarySet.Id);

    // Update project
    repositoryForSpace.Projects.Modify(project);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    return;
}