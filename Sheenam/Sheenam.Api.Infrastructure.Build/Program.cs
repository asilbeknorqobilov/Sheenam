using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;


var githubPipeline = new GithubPipeline
{
    Name = "Sheenam Build Pipeline",
    OnEvents = new Events
    {
        PullRequest = new PullRequestEvent()
        {
            Branches = new string[] { "master" }
        },

        Push = new PushEvent
        {
            Branches = new string[] { "master" }
        }
    },

    Jobs = new Dictionary<string, Job>
    {
        ["Build"] = new Job
        {
            RunsOn = BuildMachines.Windows2022,

            Steps = new List<ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.GithubTask>
            {
                new CheckoutTaskV2
                {
                    Name = "Checking out Code"
                },

                new SetupDotNetTaskV1
                {
                    Name = "Seting Up .Net",
                    TargetDotNetVersion = new TargetDotNetVersion
                    {
                        DotNetVersion = "9.0.200"
                    }
                },

                new RestoreTask()
                {
                    Name = "Restoring Nuget Packages"
                },

                new DotNetBuildTask
                {
                    Name = "Building Projects"
                },

                new TestTask
                {
                    Name = "Running Tests"
                }
            }
        }
    }
};

var client=new ADotNetClient();

client.SerializeAndWriteToFile(
    adoPipeline:githubPipeline,
    path:"C:\\Users\\Konstantinopol\\Desktop\\Sheenam\\Sheenam\\github\\build.yml");