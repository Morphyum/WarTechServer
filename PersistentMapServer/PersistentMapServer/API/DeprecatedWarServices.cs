﻿using BattleTech;
using PersistentMapServer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersistentMapAPI.API {

    // Container to hold deprecated methods and keep them out of the main service class.
    public abstract class DeprecatedWarServices : IWarServices {

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public abstract List<string> GetActiveCompaniesPerFaction(string Faction, string MinutesBack);
        public abstract Dictionary<string, int> GetActiveFactions(string MinutesBack);
        public abstract int GetActivePlayers(string MinutesBack);
        public abstract List<HistoryResult> GetMissionResults(string MinutesBack, string MaxResults);
        public abstract Dictionary<string, List<string>> GetPlayerCompanies();
        public abstract List<CompanyActivity> GetPlayerActivity(string PlayerId);
        public abstract List<ShopDefItem> GetShopForFaction(string Faction);
        public abstract StarMap GetStarmap();
        public abstract string GetStartupTime();
        public abstract System GetSystem(string name);
        public abstract System PostMissionResult(MissionResult mresult, string CompanyName);
        public abstract string PostPurchaseForFaction(List<string> ids, string Faction);
        public abstract string PostSalvageForFaction(List<ShopDefItem> salvage, string Faction);

        public abstract ServiceDataSnapshot GetServiceDataSnapshot();
        public abstract Dictionary<string, UserInfo> GetConnections();
        public abstract void LoadTestData();
        public abstract string ResetStarMap();

        public System PostMissionResultDeprecated(string employer, string target, string systemName, string mresult) {
            logger.Warn("WARNING: Deprecated method invoked: PostMissionResultDeprecated");
            try {
                return PostMissionResult(new MissionResult((Faction)Enum.Parse(typeof(Faction), employer), (Faction)Enum.Parse(typeof(Faction), target), (BattleTech.MissionResult)Enum.Parse(typeof(BattleTech.MissionResult), mresult), systemName, 5, 0, 0), "UNKNOWN");

            } catch (Exception e) {
                logger.Info(e, "Failed to postMissionResult");                
                return null;
            }
        }

        public System PostMissionResultDeprecated2(string employer, string target, string systemName, string mresult, string difficulty) {
            logger.Warn("WARNING: Deprecated method invoked: PostMissionResultDeprecated2");
            try {
                return PostMissionResult(new MissionResult((Faction)Enum.Parse(typeof(Faction), employer), (Faction)Enum.Parse(typeof(Faction), target), (BattleTech.MissionResult)Enum.Parse(typeof(BattleTech.MissionResult), mresult), systemName, int.Parse(difficulty), 0, 0), "UNKNOWN");

            } catch (Exception e) {
                logger.Info(e, "Failed to postMissionResult");
                return null;
            }
        }

        public System PostMissionResultDeprecated3(string employer, string target, string systemName, string mresult, string difficulty, string rep) {
            logger.Warn("WARNING: Deprecated method invoked: PostMissionResultDeprecated3");
            try {
                return PostMissionResult(new MissionResult((Faction)Enum.Parse(typeof(Faction), employer), (Faction)Enum.Parse(typeof(Faction), target), (BattleTech.MissionResult)Enum.Parse(typeof(BattleTech.MissionResult), mresult), systemName, int.Parse(difficulty), int.Parse(rep), 0), "UNKNOWN");

            } catch (Exception e) {
                logger.Info(e, "Failed to postMissionResult");
                return null;
            }
        }

        public System PostMissionResultDepricated4(string employer, string target, string systemName, string mresult, string difficulty, string rep, string planetSupport) {
            logger.Warn("WARNING: Deprecated method invoked: PostMissionResultDepricated4");
            try {
                return PostMissionResult(new MissionResult((Faction)Enum.Parse(typeof(Faction), employer), (Faction)Enum.Parse(typeof(Faction), target), (BattleTech.MissionResult)Enum.Parse(typeof(BattleTech.MissionResult), mresult), systemName, int.Parse(difficulty), int.Parse(rep), int.Parse(planetSupport)), "UNKNOWN");
            } catch (Exception e) {
                logger.Info(e, "Failed to postMissionResult");
                return null;
            }
        }

        public System PostMissionResultDepricated5(MissionResult mresult) {
            logger.Warn("WARNING: Deprecated method invoked: PostMissionResultDepricated5");
            return PostMissionResult(mresult, "UNKNOWN");
        }

        public string PostPurchaseForFactionDepricated(string Faction, string ID) {
            logger.Warn("WARNING: Deprecated method invoked: PostPurchaseForFactionDepricated");
            Faction realFaction = (Faction)Enum.Parse(typeof(Faction), Faction);
            if (Holder.factionShops != null) {
                FactionShop shop = Holder.factionShops.FirstOrDefault(x => x.shopOwner == realFaction);
                if (shop != null) {
                    ShopDefItem item = shop.currentSoldItems.FirstOrDefault(x => x.ID.Equals(ID));
                    if (item != null) {
                        item.Count--;
                    }
                    shop.currentSoldItems.RemoveAll(x => x.Count <= 0);
                }
            }
            logger.Debug("${ID} 1 removed from shop for {Faction}");
            return ID + " 1 removed from shop for " + Faction;
        }

    }
}
