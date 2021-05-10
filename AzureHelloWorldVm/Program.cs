using System;
using System.Linq;
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
            var region = Region.EuropeWest;
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

            // You need a network security group for controlling the access to the virtual machine.
            const string networkSecurityGroupName = "HelloWorldNetworkSecurityGroup";
            Console.WriteLine($"Creating Network Security Group {networkSecurityGroupName} ...");
            var networkSecurityGroup = azure.NetworkSecurityGroups.Define(networkSecurityGroupName)
                .WithRegion(region)
                .WithExistingResourceGroup(resourceGroupName)
                .Create();

            // You need a security rule for allowing the access to the virtual machine from the Internet.
            Console.WriteLine($"Creating a Security Rule for allowing the SSH access");
            networkSecurityGroup.Update()
                .DefineRule("AllowSshProtocol")
                .AllowInbound()
                .FromAnyAddress()
                .FromAnyPort()
                .ToAnyAddress()
                .ToPort(22)
                .WithProtocol(SecurityRuleProtocol.Tcp)
                .WithPriority(100)
                .WithDescription("Allow SSH Protocol")
                .Attach()
                .Apply();

            Console.WriteLine($"Creating a Security Rule for allowing access to 8080 port");
            networkSecurityGroup.Update()
                .DefineRule("AllowPort8080")
                .AllowInbound()
                .FromAnyAddress()
                .FromAnyPort()
                .ToAnyAddress()
                .ToPort(8080)
                .WithProtocol(SecurityRuleProtocol.Tcp)
                .WithPriority(110)
                .WithDescription("Allow Access To 8080 Port")
                .Attach()
                .Apply();

            foreach (var index in Enumerable.Range(1, 3))
            {
                // You need a public IP to be able to connect to the virtual machine from the Internet.
                var publicIpName = "HelloWorldPublicIp" + index;
                Console.WriteLine($"Creating public IP {publicIpName} ...");
                var publicIp = azure.PublicIPAddresses.Define(publicIpName)
                    .WithRegion(region)
                    .WithExistingResourceGroup(resourceGroupName)
                    .Create();

                // Any virtual machine need a network interface for connecting to the virtual network.
                var networkInterfaceName = "AzureHelloWorldNetworkInterface" + index;
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
                var virtualMachineName = "HelloWorldVm" + index;
                const string adminUser = "yaskovdev";
                Console.WriteLine($"Creating virtual machine {virtualMachineName}...");
                azure.VirtualMachines.Define(virtualMachineName)
                    .WithRegion(region)
                    .WithExistingResourceGroup(resourceGroup)
                    .WithExistingPrimaryNetworkInterface(networkInterface)
                    .WithLatestLinuxImage("Canonical", "UbuntuServer", "18.04-LTS")
                    .WithRootUsername(adminUser)
                    .WithSsh(
                        "ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAACAQDQwcxcOA8RJQIklcDuImn/MoQdzOVEGfpXz7g20AEyMUhVvq7HtuFV4Ck36xEM0gl6oheG+8E4MP2gnK4zGOEN3isjauUKfe7R6lx2wPWxU/JHPpkc5k3R1IsVbaZCWoBeEfp5+qr5xwXTIZkhzZlXH48oryp/ugmGl9ng77+1DOBZeVQ65M0XpK45R//yH5nG9X+hdp9bOzwgSbPuhXMqoXkK8/Tn3UbVe4RSiNAJb1l3ykluWUbLeqAPWvv4dXWdtnLEPeS/tRKJjrh5VUGSLkVQp+66OvYoVUDsMzHkUSF1rEvnBDBWPhVE5dw/BgLXss0Ac6TXMaGQUF84iMgfyX/Z7dM/5sJ+IpY2omO5ujfLda2R2VPjrmxjDhmX4ZCX9NgWxUUodTObrmzxPcpmooqIHtO4ldBUkGNgML4HAUB0sgQBT5Miq968KczJf7sH0acVDVXAFAuRV0yrhxK2PrSH7sqF6VizDvKoulMakBUuZO36VpYYACMJQN6teDebSrKjjnnF+zzcta/1233LK7i6F0I7yhxal/G0GElyRd9P4J7vJqaK8aivGNMdOt8fo12d5irxa6Vk2NtDhMZa4xlYtuNs+8pacXsyzvMRxWtXGLYoFrBxLWfreP74qAaIIF5nCyoJ+Q5nPvhzeBmSLjaoPn7581G5mq6WnX0JLw== yaskovdev@MacBook-Pro-Sergey.local")
                    .WithComputerName(virtualMachineName)
                    .WithSize(VirtualMachineSizeTypes.StandardB1s)
                    .Create();
            }
        }
    }
}