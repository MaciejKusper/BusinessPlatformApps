﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using FacebookETL;

namespace FacebookUtillity
{
    public class PageAnalyticsETL
    {
        public static void PopulateMeasures(string pageId, string pageAccessToken, string sqlConnection, string sqlSchema, string getDataUntil)
        {
            string page = pageId;
            string accessToken = pageAccessToken;
            string sqlConn = sqlConnection;
            string schema = sqlSchema;
            string until = getDataUntil;

            var pageContentTable = DataTableUtility.GetPageContentTable();
            var pageEngagementTable = DataTableUtility.GetPageEngagementTable();
            var pageImpressionsTable = DataTableUtility.GePageImpressionsTable();
            var pagePostsTable = DataTableUtility.GetPagePostsTable();
            var pagePostEngagement = DataTableUtility.GetPagePostEngagementTable();
            var pagePostImpressions = DataTableUtility.GetPagePostImpressionsTable();
            var pagePostReactionsTable = DataTableUtility.GetPagePostReactionsTable();
            var pagePostStoriesAndPeopleTalkingAboutThisTable = DataTableUtility.GetPagePostStoriesAndPeopleTalkingAboutThisTable();
            var pageReactionsTable = DataTableUtility.GetPageReactionsTable();
            var pageUserDemographicsTable = DataTableUtility.GetPageUserDemographicsTable();
            var pageVideoPosts = DataTableUtility.GetPageVideoPostsTable();
            var pageVideoViews = DataTableUtility.GetPageVideoViewsTable();
            var pageViewsTable = DataTableUtility.GetPageViewsTable();
            var clicksTable = DataTableUtility.GetPageClicksDataTable();
            var postsInfoTable = DataTableUtility.GetPostsInfoTable();

            var content = FacebookUtility.GetPageMetricAnalytics(page, until, accessToken, FacebookPageAnalyticsMetricGroups.PageContent).Result;
            PopulateNestedValues(pageContentTable, content, page);

            var engagement = FacebookUtility.GetPageMetricAnalytics(page, until, accessToken, FacebookPageAnalyticsMetricGroups.PageEngagement).Result;
            PopulateNestedValues(pageEngagementTable, engagement, page);

            var impressions = FacebookUtility.GetPageMetricAnalytics(page, until, accessToken, FacebookPageAnalyticsMetricGroups.PageImpressions).Result;
            PopulateNestedValues(pageImpressionsTable, impressions, page);

            var pagePosts = FacebookUtility.GetPageMetricAnalytics(page, until, accessToken, FacebookPageAnalyticsMetricGroups.PagePosts).Result;
            PopulateNestedValues(pagePostsTable, pagePosts, page);

            var pagePostsEngagement = FacebookUtility.GetPageMetricAnalytics(page, until, accessToken, FacebookPageAnalyticsMetricGroups.PagePostEngagement).Result;
            PopulateNestedValues(pagePostEngagement, pagePostsEngagement, page);

            var pagePostsReactions = FacebookUtility.GetPageMetricAnalytics(page, until, accessToken, FacebookPageAnalyticsMetricGroups.PagePostReactions).Result;
            PopulateNestedValues(pagePostReactionsTable, pagePostsReactions, page);

            var pageReactions = FacebookUtility.GetPageMetricAnalytics(page, until, accessToken, FacebookPageAnalyticsMetricGroups.PageReactions).Result;
            PopulateNestedValues(pageReactionsTable, pageReactions, page);

            var pagePostStories = FacebookUtility.GetPageMetricAnalytics(page, until, accessToken, FacebookPageAnalyticsMetricGroups.PagePostStoriesAndPeopleTalkingAboutThis).Result;
            PopulateNestedValues(pagePostStoriesAndPeopleTalkingAboutThisTable, pagePostStories, page);

            var pageUserDemographics = FacebookUtility.GetPageMetricAnalytics(page, until, accessToken, FacebookPageAnalyticsMetricGroups.PageUserDemographics).Result;
            PopulateNestedValues(pageUserDemographicsTable, pageUserDemographics, page);

            var pageVideoViewsObj = FacebookUtility.GetPageMetricAnalytics(page, until, accessToken, FacebookPageAnalyticsMetricGroups.PageVideoViews).Result;
            PopulateNestedValues(pageVideoViews, pageVideoViewsObj, page);

            var pageViews = FacebookUtility.GetPageMetricAnalytics(page, until, accessToken, FacebookPageAnalyticsMetricGroups.PageViews).Result;
            PopulateNestedValues(pageViewsTable, pageViews, page);

            var clicks = FacebookUtility.GetPageMetricAnalytics(page, until, accessToken, FacebookPageAnalyticsMetricGroups.PageCtaClicks).Result;
            PopulateNestedValues(clicksTable, clicks, page);

            var pagePostIds = FacebookUtility.GetPageMetricAnalytics(page, until, accessToken, FacebookPageAnalyticsMetricGroups.PagePostIds).Result;
            PopulatePostsInfo(postsInfoTable, pagePostIds, page);

            List<JObject> pagePostReactions = new List<JObject>();
            List<JObject> pageVideoPostsObj = new List<JObject>();
            List<JObject> pagePostsImpressions = new List<JObject>();
            if (pagePostIds != null)
            {
                foreach (var entry in pagePostIds)
                {
                    if (entry?["data"] != null)
                    {
                        foreach (var obj in entry["data"])
                        {
                            pagePostsImpressions.AddRange(FacebookUtility.GetPageMetricAnalytics(obj["id"].ToString(), until, accessToken, FacebookPageAnalyticsMetricGroups.PagePostImpressions).Result);
                            pageVideoPostsObj.AddRange(FacebookUtility.GetPageMetricAnalytics(obj["id"].ToString(), until, accessToken, FacebookPageAnalyticsMetricGroups.PageVideoPosts).Result);
                            pagePostReactions.AddRange(FacebookUtility.GetPageMetricAnalytics(obj["id"].ToString(), until, accessToken, FacebookPageAnalyticsMetricGroups.PagePostReactions).Result);
                        }
                    }
                }
            }
            PopulateNestedValues(pagePostReactionsTable, pagePostReactions, page);
            PopulateNestedValues(pageVideoPosts, pageVideoPostsObj, page);
            PopulateNestedValues(pagePostImpressions, pagePostsImpressions, page);

            SqlUtility.BulkInsert(sqlConn, pageContentTable, schema + "." + "STAGING_PageContent");
            SqlUtility.BulkInsert(sqlConn, pageEngagementTable, schema + "." + "STAGING_PageEngagement");
            SqlUtility.BulkInsert(sqlConn, pageImpressionsTable, schema + "." + "STAGING_PageImpressions");
            SqlUtility.BulkInsert(sqlConn, pagePostsTable, schema + "." + "STAGING_PagePost");
            SqlUtility.BulkInsert(sqlConn, pagePostEngagement, schema + "." + "STAGING_PagePostEngagement");
            SqlUtility.BulkInsert(sqlConn, pagePostImpressions, schema + "." + "STAGING_PagePostImpressions");
            SqlUtility.BulkInsert(sqlConn, pagePostReactionsTable, schema + "." + "STAGING_PagePostReactions");
            SqlUtility.BulkInsert(sqlConn, pagePostStoriesAndPeopleTalkingAboutThisTable, schema + "." + "STAGING_PagePostStoriesAndPeopleTalkingAboutThis");
            SqlUtility.BulkInsert(sqlConn, pageReactionsTable, schema + "." + "STAGING_PageReactions");
            SqlUtility.BulkInsert(sqlConn, pageUserDemographicsTable, schema + "." + "STAGING_PageUserDemographics");
            SqlUtility.BulkInsert(sqlConn, pageVideoPosts, schema + "." + "STAGING_PageVideoPosts");
            SqlUtility.BulkInsert(sqlConn, pageVideoViews, schema + "." + "STAGING_PageVideoViews");
            SqlUtility.BulkInsert(sqlConn, pageViewsTable, schema + "." + "STAGING_PageViews");
            SqlUtility.BulkInsert(sqlConn, clicksTable, schema + "." + "STAGING_Clicks");
            SqlUtility.BulkInsert(sqlConn, postsInfoTable, schema + "." + "STAGING_PagePostsInfo");
        }

        public static void PopulateNestedValues(DataTable table, List<JObject> objects, string pageId)
        {
            foreach (var obj in objects)
            {
                foreach (var entry in obj["data"])
                {
                    foreach (var val in entry["values"])
                    {
                        if (val?["value"] != null && val["value"].Children().Count() >= 1)
                        {
                            Extract(table, pageId, entry, val);
                        }
                        else
                        {
                            DataRow row = table.NewRow();
                            if (table.Columns.Contains("EndTime")) { row["EndTime"] = val["end_time"]; }
                            row["Name"] = entry["name"];
                            row["Value"] = (val["value"] != null && string.IsNullOrEmpty(val["value"].ToString())) ||
                                            (val["value"] != null && val["value"].ToString() == "{}") ?
                                            DBNull.Value :
                                            (object)val["value"];
                            Debug.WriteLine(row["Value"]);
                            row["Period"] = entry["period"];
                            row["Title"] = entry["title"];
                            row["Description"] = entry["description"];
                            row["Id"] = entry["id"];
                            row["PageId"] = pageId;
                            table.Rows.Add(row);
                        }
                    }
                }
            }
        }

        private static void Extract(DataTable table, string pageId, JToken entry, JToken val)
        {
            foreach (var child in val["value"])
            {
                if (child.GetType() != typeof(JProperty) &&
                    child?["value"] != null &&
                    child["value"].Children().Count() >= 1)
                {
                    Extract(table, pageId, entry, child);
                }
                else
                {
                    var att = child as JProperty;

                    if (att.Value.Children().Count() >= 1)
                    {
                        ExtractChildren(table, pageId, entry, val, att);
                    }
                    else
                    {
                        DataRow row = table.NewRow();
                        if (table.Columns.Contains("EndTime")) { row["EndTime"] = val["end_time"]; }
                        row["Name"] = entry["name"];
                        row["Entry Name"] = att.Name;
                        row["Value"] = (att.Value != null && string.IsNullOrEmpty(att.Value.ToString())) ||
                                        (att.Value != null && att.Value.ToString() == "{}") ?
                                        DBNull.Value :
                                        (object)att.Value;
                        Debug.WriteLine(row["Value"]);
                        row["Period"] = entry["period"];
                        row["Title"] = entry["title"];
                        row["Description"] = entry["description"];
                        row["Id"] = entry["id"];
                        row["PageId"] = pageId;
                        table.Rows.Add(row);
                    }
                }
            }
        }

        private static void ExtractChildren(DataTable table, string pageId, JToken entry, JToken val, JProperty att)
        {
            if (att.Value.Children().Count() >= 1)
            {
                foreach (var c in att.Value.Children())
                {
                    ExtractChildren(table, pageId, entry, val, c as JProperty);
                }
            }
            else
            {
                DataRow row = table.NewRow();
                if (table.Columns.Contains("EndTime")) { row["EndTime"] = val["end_time"]; }
                row["Name"] = entry["name"];
                // Only merge names for age/gender
                if (entry["name"].ToString().ToLower().Contains("page_cta_clicks_by_age_gender_logged_in_unique"))
                {
                    row["Entry Name"] = att.Name + " " + (att.Parent.Parent as JProperty).Name;
                }
                else
                {
                    row["Entry Name"] = att.Name;
                }
                row["Value"] = (att.Value != null && string.IsNullOrEmpty(att.Value.ToString())) ||
                               (att.Value != null && att.Value.ToString() == "{}") ?
                               DBNull.Value :
                               (object)att.Value;
                Debug.WriteLine(row["Value"]);
                row["Period"] = entry["period"];
                row["Title"] = entry["title"];
                row["Description"] = entry["description"];
                row["Id"] = entry["id"];
                row["PageId"] = pageId;
                table.Rows.Add(row);
            }
        }

        public static void PopulatePostsInfo(DataTable table, List<JObject> objects, string pageId)
        {
            foreach (var obj in objects)
            {
                foreach (var val in obj["data"])
                {
                    if (val?["to"] != null &&
                        (val["to"]["data"] as JArray).Count > 0)
                    {
                        foreach (var t in val["to"]["data"])
                        {
                            DataRow row = table.NewRow();
                            row["Id"] = val["id"];
                            row["PageId"] = pageId;
                            row["Message"] = val["message"];
                            row["Created Time"] = val["created_time"];
                            row["Updated Time"] = val["updated_time"];
                            row["Icon"] = val["icon"];
                            row["Link"] = val["link"];
                            row["Name"] = val["name"];
                            row["Object"] = val["object_id"];
                            row["Permalink URL"] = val["permalink_url"];
                            row["Picture"] = val["picture"];
                            row["Source"] = val["source"];
                            row["Shares"] = val["shares"]["count"];
                            row["To Id"] = t["id"];
                            row["To Name"] = t["name"];
                            row["Type"] = val["type"];
                            row["Status Type"] = val["status_type"];
                            row["Is Hidden"] = val["is_hidden"];
                            row["Is Published"] = val["is_published"];
                            row["Story"] = val["story"];
                            table.Rows.Add(row);
                        }

                    }
                    else
                    {
                        DataRow row = table.NewRow();
                        row["Id"] = val["id"];
                        row["PageId"] = pageId;
                        row["Message"] = val["message"];
                        row["Created Time"] = val["created_time"];
                        row["Updated Time"] = val["updated_time"];
                        row["Icon"] = val["icon"];
                        row["Link"] = val["link"];
                        row["Name"] = val["name"];
                        row["Object"] = val["object_id"];
                        row["Permalink URL"] = val["permalink_url"];
                        row["Picture"] = val["picture"];
                        row["Source"] = val["source"];
                        row["Shares"] = val["shares"]?["count"];
                        row["To Id"] = DBNull.Value;
                        row["To Name"] = DBNull.Value;
                        row["Type"] = val["type"];
                        row["Status Type"] = val["status_type"];
                        row["Is Hidden"] = val["is_hidden"];
                        row["Is Published"] = val["is_published"];
                        row["Story"] = val["story"];
                        table.Rows.Add(row);
                    }
                }
            }
        }
    }
}
