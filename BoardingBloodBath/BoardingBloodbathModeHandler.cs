using System;
using System.Collections.Generic;
using ModLoader;
using UnityEngine;

namespace BoardingBloodbath
{
    [Mod]
    public class BoardingBloodbathModeHandler : MonoBehaviour
    {

        public static List<string> usersVoted = new List<string>();
        public static bool voteSucceded = false;

        public static void forceVote(string pName, int pSocket, bool setType, int type = 0)
        {
            if (voteSucceded || (setType && BoardingBloodbathGameMode.Instance.nextPreset == type))
            {
                BoardingBloodbathGameMode.Instance.wno.òäóæåòîððòä("broadcastChat", pSocket, new object[]
                {
                        1,
                        1,
                        "game",
                        "The gamemode has already won the vote!"
                });
            }
            else if (setType) {
                if (type <= BoardingBloodbathGameMode.Instance.presets.Count - 1)
                {
                    UI.Instance.GetComponent<WakeNetObject>().îëæêéïåðæìå("sendWarning", óëððîêðëóêó.îéäåéçèïïñí, new object[]
                    {
                        "all",
                        "Switching to Boarding Bloodbath mode next round..."
                    });
                    BoardingBloodbathGameMode.Instance.wno.òäóæåòîððòä("broadcastChat", pSocket, new object[]
                    {
                        1,
                        1,
                        "game",
                        $"Setting to preset {type}"
                    });
                    BoardingBloodbathGameMode.Instance.nextPreset = type;
                    voteSucceded = true;
                }
                else
                {
                    BoardingBloodbathGameMode.Instance.wno.òäóæåòîððòä("broadcastChat", pSocket, new object[]
                    {
                        1,
                        1,
                        "game",
                        $"Please enter a value from 0 to {BoardingBloodbathGameMode.Instance.presets.Count - 1}"
                    }); ;
                }
            }
            else
            {
                UI.Instance.GetComponent<WakeNetObject>().îëæêéïåðæìå("sendWarning", óëððîêðëóêó.îéäåéçèïïñí, new object[]
                {
                        "all",
                        "Switching to Boarding Bloodbath mode next round..."
                });
                voteSucceded = true;
                BoardingBloodbathGameMode.selectNextPreset();
            }
        }

        public static void vote(string pName, int pSocket)
        {
            Log.log("Handler - Got vote");
            if (usersVoted.Contains(pName))
            {
                Log.log("Already voted");
                BoardingBloodbathGameMode.Instance.wno.òäóæåòîððòä("broadcastChat", pSocket, new object[]
                {
                        1,
                        1,
                        "game",
                        "You have already Voted!"
                });
            }
            else
            {
                usersVoted.Add(pName);
                Log.log("New voter");
                // To User only
                BoardingBloodbathGameMode.Instance.wno.òäóæåòîððòä("broadcastChat", pSocket, new object[]
                {
                    1,
                    1,
                    "game",
                    "Your Vote has been counted."
                });

                // To all users
                BoardingBloodbathGameMode.Instance.wno.îëæêéïåðæìå("broadcastChat", óëððîêðëóêó.îéäåéçèïïñí, new object[]
                {
                    1,
                    1,
                    "game",
                    string.Concat(new string[]
                    {
                        "User has voted for 'BoardingBloodbath' (",
                        usersVoted.Count.ToString(),
                        "/",
                        Math.Ceiling(GameMode.Instance.Players.Count / 2.0).ToString(),
                        ")",
                        " using '!Vote BB' in chat!"
                    })
                });

                if (usersVoted.Count >= Math.Ceiling(GameMode.Instance.Players.Count / 2.0) && !voteSucceded)
                {
                    UI.Instance.GetComponent<WakeNetObject>().îëæêéïåðæìå("sendWarning", óëððîêðëóêó.îéäåéçèïïñí, new object[]
                    {
                        "all",
                        "Switching to Boarding Bloodbath mode next round..."
                    });
                    voteSucceded = true;
                    BoardingBloodbathGameMode.selectNextPreset();
                }
            }
        }
    }
}
