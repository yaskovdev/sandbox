using System;
using Microsoft.Azure.Management.Compute.Fluent.Models;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.Network.Fluent.Models;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace AzureHelloWorldVm
{
    internal static class Program
    {
        private static void Main()
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
            // the resources needed for the virtual machine.
            const string resourceGroupName = "AzureHelloWorldResourceGroup";
            var region = Region.USWest2;
            Console.WriteLine($"Creating resource group {resourceGroupName}...");
            var resourceGroup = azure.ResourceGroups.Define(resourceGroupName)
                .WithRegion(region)
                .Create();

            // Every virtual machine needs to be connected to a virtual network.
            const string virtualNetworkName = "AzureHelloWorldVirtualNetwork";
            const string virtualNetworkAddress = "10.0.0.0/16";
            const string subnetName = "AzureHelloWorldSubnet";
            const string subnetAddress = "10.0.0.0/24";
            Console.WriteLine($"Creating virtual network {virtualNetworkName}...");
            var network = azure.Networks.Define(virtualNetworkName)
                .WithRegion(region)
                .WithExistingResourceGroup(resourceGroup)
                .WithAddressSpace(virtualNetworkAddress)
                .WithSubnet(subnetName, subnetAddress)
                .Create();

            // You need a public IP to be able to connect to the virtual machine from the Internet.
            const string publicIpName = "HelloWorldPublicIp";
            Console.WriteLine($"Creating public IP {publicIpName} ...");
            var publicIp = azure.PublicIPAddresses.Define(publicIpName)
                .WithRegion(region)
                .WithExistingResourceGroup(resourceGroupName)
                .Create();

            // You need a network security group for controlling the access to the virtual machine.
            const string networkSecurityGroupName = "HelloWorldNetworkSecurityGroup";
            Console.WriteLine($"Creating Network Security Group {networkSecurityGroupName} ...");
            var networkSecurityGroup = azure.NetworkSecurityGroups.Define(networkSecurityGroupName)
                .WithRegion(region)
                .WithExistingResourceGroup(resourceGroupName)
                .Create();

            // You need a security rule for allowing the access to the virtual machine from the Internet.
            Console.WriteLine($"Creating a Security Rule for allowing the remote access");
            networkSecurityGroup.Update()
                .DefineRule("AllowRemoteDesktopProtocol")
                .AllowInbound()
                .FromAnyAddress()
                .FromAnyPort()
                .ToAnyAddress()
                .ToPort(3389)
                .WithProtocol(SecurityRuleProtocol.Tcp)
                .WithPriority(100)
                .WithDescription("Allow Remote Desktop Protocol")
                .Attach()
                .Apply();

            // Any virtual machine need a network interface for connecting to the virtual network.
            const string networkInterfaceName = "AzureHelloWorldNetworkInterface";
            Console.WriteLine($"Creating network interface {networkInterfaceName}...");
            var networkInterface = azure.NetworkInterfaces.Define(networkInterfaceName)
                .WithRegion(region)
                .WithExistingResourceGroup(resourceGroup)
                .WithExistingPrimaryNetwork(network)
                .WithSubnet(subnetName)
                .WithPrimaryPrivateIPAddressDynamic()
                .WithExistingPrimaryPublicIPAddress(publicIp)
                .WithExistingNetworkSecurityGroup(networkSecurityGroup)
                .Create();

            // Create the virtual machine.
            const string virtualMachineName = "HelloWorldVm";
            const string adminUser = "yaskovdev";
            const string adminPassword = "";
            Console.WriteLine($"Creating virtual machine {virtualMachineName}...");
            azure.VirtualMachines.Define(virtualMachineName)
                .WithRegion(region)
                .WithExistingResourceGroup(resourceGroup)
                .WithExistingPrimaryNetworkInterface(networkInterface)
                .WithLatestWindowsImage("MicrosoftWindowsServer", "WindowsServer", "2012-R2-Datacenter")
                .WithAdminUsername(adminUser)
                .WithAdminPassword(adminPassword)
                .WithComputerName(virtualMachineName)
                .WithSize(VirtualMachineSizeTypes.StandardDS2V2)
                // .WithExistingAvailabilitySet() TODO
                .Create();
        }
    }
}