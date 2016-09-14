using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatSharp;

namespace mbotim
{
    class Program
    {
        static void Main(string[] args)
        {

            RunProgram();
        }


        static void RunProgram()
        {
            var client = new IrcClient("irc.chat.twitch.tv", new IrcUser("bobmock", "bobmock", "oauth:5ocuript05144wymuousvl0powre3n"));

            client.ConnectionComplete += (s, e) =>
            { client.JoinChannel("#mrhappyismissing"); Console.WriteLine("Connected!"); };

            client.ChannelMessageRecieved += (s, e) =>
            {
                Console.WriteLine(e.PrivateMessage.User+": "+e.PrivateMessage.Message);
                //Console.WriteLine(e.PrivateMessage.ToString());
                var channel = client.Channels[e.PrivateMessage.Source];

                if (e.PrivateMessage.Message == ".list")
                {
                    Console.WriteLine("!list called");
                    //channel.SendMessage(string.Join(", ", channel.Users.Select(u => u.Nick)));
                }
                else if (e.PrivateMessage.Message.StartsWith(".ban "))
                {
                    if (!channel.UsersByMode['@'].Contains(client.User))
                    {
                        //channel.SendMessage("I'm not an op here!");
                        return;
                    }
                    var target = e.PrivateMessage.Message.Substring(5);
                    //client.WhoIs(target, whois => channel.ChangeMode("+b *!*@" + whois.User.Hostname));
                }
                else if (e.PrivateMessage.Message.Contains("!hype"))
                {
                    channel.SendMessage("HYPE HYPE HYPE");
                }
            };

            client.UserJoinedChannel += (s, e) =>
            {
                Console.WriteLine(e.User.Nick + " joined channel.");
                e.Channel.SendMessage("KappaRoss / " + e.User.Nick + "!");
            };

            client.ConnectAsync();

            while (true) ; // Waste CPU cycles


        }
    }
}
