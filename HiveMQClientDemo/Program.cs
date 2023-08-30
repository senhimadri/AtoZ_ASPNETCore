using HiveMQtt.Client;
using HiveMQtt.Client.Options;
using HiveMQtt.MQTT5.ReasonCodes;
using HiveMQtt.MQTT5.Types;
using System.Text.Json;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var options = new HiveMQClientOptions
        {
            Host = "42c21f4d5e1a4f8ea706b6ad09576d7e.s1.eu.hivemq.cloud",
            Port = 8883,
            UseTLS = true,
            UserName = "dipankar",
            Password = "C3SmLZbGNtk#md^y"
        };
        var client = new HiveMQClient(options);


        Console.WriteLine($"Connecting to {options.Host} on port {options.Port}");
        HiveMQtt.Client.Results.ConnectResult connectResult;
        try
        {
            connectResult = await client.ConnectAsync().ConfigureAwait(false);
            if (connectResult.ReasonCode == ConnAckReasonCode.Success)
            {
                Console.WriteLine($"Connected Successfull {connectResult}");
            }
            else
            {
                Console.WriteLine($"Connected Successfull {connectResult}");
                Environment.Exit(-1);
            }
        }
        catch (System.Net.Sockets.SocketException e)
        {
            Console.WriteLine($"Error connecting to the MQTT Broker with the folling socket Error:  {e.Message}");
            Environment.Exit(-1);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error connecting to the MQTT Broker with the folling Error:  {e.Message}");
            Environment.Exit(-1);
        }

        //Message Handler
        client.OnMessageReceived += (sender, args) =>
        {
            string received_messege = args.PublishMessage.PayloadAsString;
            Console.WriteLine(received_messege);
        };

        await client.SubscribeAsync("ibos").ConfigureAwait(false);

        double temperature = 25.1;
        double humidity = 77.5;
        var rand = new Random();

        Console.WriteLine("Publishing Message");

        while (true)
        {
            double currentTemperature = temperature + rand.NextDouble();
            double currentHumidity = humidity + rand.NextDouble();

            var msg = JsonSerializer.Serialize(
                        new
                        {
                            temperature = currentTemperature,
                            humidity = currentHumidity
                        });

            var result = await client.PublishAsync("ibos/telemetry", msg, QualityOfService.AtLeastOnceDelivery)
                            .ConfigureAwait(false);
        }
    }
}