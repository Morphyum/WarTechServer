﻿using BattleTech;
using PersistentMapServer.Objects;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace PersistentMapAPI {
    [ServiceContract(Name = "WarServices")]
    public interface IWarServices {

        [OperationContract]
        [WebGet(UriTemplate = Routing.GetStarMap, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        StarMap GetStarmap();

        [OperationContract]
        [WebGet(UriTemplate = Routing.GetSystem, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        System GetSystem(string name);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = Routing.PostMissionResult, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        System PostMissionResult(MissionResult mresult, string CompanyName);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = Routing.PostSalvageForFaction, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string PostSalvageForFaction(List<ShopDefItem> salvage, string Faction);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = Routing.PostPurchaseForFaction, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string PostPurchaseForFaction(List<string> ids, string Faction);

        [OperationContract]
        [WebGet(UriTemplate = Routing.GetMissionResults, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        List<HistoryResult> GetMissionResults(string MinutesBack, string MaxResults);

        [OperationContract]
        [WebGet(UriTemplate = Routing.GetActivePlayers, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        int GetActivePlayers(string MinutesBack);

        [OperationContract]
        [WebGet(UriTemplate = Routing.GetActiveFactions, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        Dictionary<string, int> GetActiveFactions(string MinutesBack);

        [OperationContract]
        [WebGet(UriTemplate = Routing.GetActiveCompaniesPerFaction, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        List<string> GetActiveCompaniesPerFaction(string Faction, string MinutesBack);

        [OperationContract]
        [WebGet(UriTemplate = Routing.GetStartupTime, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        string GetStartupTime();

        [OperationContract]
        [WebGet(UriTemplate = Routing.GetShopForFaction, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        List<ShopDefItem> GetShopForFaction(string Faction);

        [OperationContract]
        [WebGet(UriTemplate = Routing.GetPlayerCompanies, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        Dictionary<string, List<string>> GetPlayerCompanies();

        [OperationContract]
        [WebGet(UriTemplate = Routing.GetPlayerHistory, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        List<CompanyActivity> GetPlayerActivity(string PlayerId);

        // Admin functions
        [OperationContract]
        [WebGet(UriTemplate = Routing.GetServiceDataSnapshot, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        ServiceDataSnapshot GetServiceDataSnapshot();

        [OperationContract]
        [WebGet(UriTemplate = Routing.GetConnections, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        Dictionary<string, UserInfo> GetConnections();

        [OperationContract]
        [WebGet(UriTemplate = Routing.LoadTestData, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        void LoadTestData();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = Routing.ResetStarMap, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string ResetStarMap();

        //DEPRECATED
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = Routing.PostPurchaseForFactionDepricated, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string PostPurchaseForFactionDepricated(string Faction, string ID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = Routing.PostMissionResultDepricated5, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        System PostMissionResultDepricated5(MissionResult mresult);

        [OperationContract]
        [WebGet(UriTemplate = Routing.PostMissionResultDepricated4, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        System PostMissionResultDepricated4(string employer, string target, string systemName, string mresult, string difficulty, string rep, string planetSupport);

        [OperationContract]
        [WebGet(UriTemplate = Routing.PostMissionResultDepricated3, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        System PostMissionResultDeprecated3(string employer, string target, string systemName, string mresult, string difficulty, string rep);


        [OperationContract]
        [WebGet(UriTemplate = Routing.PostMissionResultDepricated2, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        System PostMissionResultDeprecated2(string employer, string target, string systemName, string mresult, string difficulty);

        [OperationContract]
        [WebGet(UriTemplate = Routing.PostMissionResultDepricated, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        System PostMissionResultDeprecated(string employer, string target, string systemName, string mresult);


    }
}
