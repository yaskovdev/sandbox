using System;
using Microsoft.Azure.Management.Compute.Fluent.Models;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace AzureHelloWorldVm
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the management client. This will be used for all the operations
            // that we will perform in Azure.
            var credentials = SdkContext.AzureCredentialsFactory
                .FromFile("./azure-auth.properties");

            var azure = Azure.Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithDefaultSubscription();

            // First of all, we need to create a resource group where we will add all
            // the resources needed for the virtual machine
            const string resourceGroupName = "AzureHelloWorldResourceGroup";
            var location = Region.USWest2;
            Console.WriteLine($"Creating resource group {resourceGroupName}...");
            var resourceGroup = azure.ResourceGroups.Define(resourceGroupName)
                .WithRegion(location)
                .Create();

            // Every virtual machine needs to be connected to a virtual network.
            const string virtualNetworkName = "AzureHelloWorldVirtualNetwork";
            const string virtualNetworkAddress = "10.0.0.0/16";
            const string subnetName = "AzureHelloWorldSubnet";
            const string subnetAddress = "10.0.0.0/24";
            Console.WriteLine($"Creating virtual network {virtualNetworkName}...");
            var network = azure.Networks.Define(virtualNetworkName)
                .WithRegion(location)
                .WithExistingResourceGroup(resourceGroup)
                .WithAddressSpace(virtualNetworkAddress)
                .WithSubnet(subnetName, subnetAddress)
                .Create();

            // Any virtual machine need a network interface for connecting to the
            // virtual network.
            const string networkInterfaceName = "AzureHelloWorldNetworkInterface";
            Console.WriteLine($"Creating network interface {networkInterfaceName}...");
            var networkInterface = azure.NetworkInterfaces.Define(networkInterfaceName)
                .WithRegion(location)
                .WithExistingResourceGroup(resourceGroup)
                .WithExistingPrimaryNetwork(network)
                .WithSubnet(subnetName)
                .WithPrimaryPrivateIPAddressDynamic()
                .Create();

            // Create the virtual machine
            const string virtualMachineName = "HelloWorldVm";
            const string adminUser = "yaskovdev";
            const string adminPassword = "";
            Console.WriteLine($"Creating virtual machine {virtualMachineName}...");
            azure.VirtualMachines.Define(virtualMachineName)
                .WithRegion(location)
                .WithExistingResourceGroup(resourceGroup)
                .WithExistingPrimaryNetworkInterface(networkInterface)
                .WithLatestWindowsImage("MicrosoftWindowsServer", "WindowsServer", "2012-R2-Datacenter")
                .WithAdminUsername(adminUser)
                .WithAdminPassword(adminPassword)
                .WithComputerName(virtualMachineName)
                .WithSize(VirtualMachineSizeTypes.StandardDS2V2)
                .Create();
        }
    }
}