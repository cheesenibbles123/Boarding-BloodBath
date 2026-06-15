using System;
using UnityEngine;
using ModLoader;
using Harmony;

namespace BoardingBloodbath
{
    [Mod]
    public class ShipsHandler : MonoBehaviour
    {
        public static WakeNetObject wno;
        void Start()
        {
            wno = GameMode.Instance.handler.åñîïòíæêêåî;
        }
        public static void spawnShip(Ships ship, int team)
        {
            Log.log("Spawning ship " + ship + " for team " + team);
            if (GameMode.Instance.teamIsShip[team] || GameMode.Instance.winning)
            {
                return;
            }
            else
            {
                getconstruction().ships[(int)ship].ëéíìðñîòçïï[team] = true;
                for (int i = 0; i < GameMode.Instance.teamParents.Length; i++)
                {
                    GameMode.Instance.teamIsShip[i] = GameMode.Instance.isShip(i);
                }
                getconstruction().GetComponent<WakeNetObject>().îëæêéïåðæìå("allBuildShip", óëððîêðëóêó.îéäåéçèïïñí, new object[]
                {
                    Enum.GetName(typeof(Ships), ship).ToLower(),
                    team
                });
            }
        }

        public static void Reset()
        {
            Log.logger.Log("Resetting ships");
            foreach (ShipConstructInfo shipConstructInfo in getconstruction().ships)
            {
                for (int j = 0; j < 8; j++)
                {
                    shipConstructInfo.ëéíìðñîòçïï[j] = false;
                }
            }
            Traverse.Create(getconstruction()).Field("currentTeamShip").SetValue(new string[8]);
        }

        public static ShipConstruction getconstruction()
        {
            return UI.Instance.shipYard.GetComponent<ShipConstruction>();
        }

        public enum Ships
        {
            Hoy,
            Schooner,
            Galleon,
            Cruiser,
            Junk,
            Gunboat,
            Cutter,
            Brig,
            Bombketch,
            Carrack,
            Xebec,
            Bombvessel
        }
    }
}
