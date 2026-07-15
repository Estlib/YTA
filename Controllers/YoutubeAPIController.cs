//Copyright(C) 2026 Estlib

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <https://gnu.org>.
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTA.DBDomain.Models;

namespace YTA.Controllers
{
    public class YoutubeAPIController
    {
        private const string AppName = "YTA";
        private const string YTSdata = "YTAPIDATA\\ycs.json";

        /// <summary>
        /// login method
        /// </summary>
        /// <returns>service</returns>
        public async Task<YouTubeService> GetYouTubeServiceAsync()
        {
            UserCredential credential;

            using FileStream stream = new FileStream(YTSdata, FileMode.Open, FileAccess.Read);

            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.FromStream(stream).Secrets,
                new[] {
                    YouTubeService.Scope.YoutubeUpload,
                    YouTubeService.Scope.YoutubeReadonly,
                    YouTubeService.Scope.Youtube
                },
                "user",
                CancellationToken.None,
                new FileDataStore("YTA.Auth.Store"));
            YouTubeService ytServ = new YouTubeService(
                new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = AppName,
                }
            );
            return ytServ;

        }

        /// <summary>
        /// Reads channel details from api
        /// </summary>
        /// <returns></returns>
        public async Task<YTLoggedUser> GetChannelDetails()
        {
            YouTubeService ytServ = await GetYouTubeServiceAsync();
            var request = ytServ.Channels.List("snippet,statistics");
            request.Mine = true;
            var response = await request.ExecuteAsync();
            var channel = response.Items.FirstOrDefault();
            if (channel == null)
            {
                string message = $"This account has no channels. To create a channel, please use browser.";
                string title = "Error";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string avatarURL = channel.Snippet.Thumbnails.Default__.Url;
            if (channel.Snippet.Thumbnails.High != null)
            {
                avatarURL = channel.Snippet.Thumbnails.High.Url;
            }
            if (channel.Snippet.Thumbnails.Medium != null)
            {
                avatarURL = channel.Snippet.Thumbnails.Medium.Url;
            }
            YTLoggedUser user = new YTLoggedUser()
            {
                ChannelID = channel.Id,
                ChannelName = channel.Snippet.Title,
                AvatarLink = avatarURL,
                SubCount = channel.Statistics.SubscriberCount,
                VideoCount = channel.Statistics.VideoCount,
                ChannelViewCount = channel.Statistics.ViewCount
            };
            return user;
        }

        public async Task<string> CreateContent(YTContent content)
        {
            YouTubeService ytServ = await GetYouTubeServiceAsync();
            //switch (content.ThisMediaIs)
            //{
            //    case MediaType.Video:
            Video video = new Video
            {
                Snippet = new VideoSnippet
                {
                    Title = content.Title,
                    Description = content.Description,
                    CategoryId = content.CategoryID.ToString(),
                },
                Status = new VideoStatus
                {
                    PrivacyStatus = content.Privacy.ToString().ToLower(),
                    SelfDeclaredMadeForKids = content.SelfDeclaredMadeForKids,
                    ContainsSyntheticMedia = content.ContainsSyntheticMedia
                },
            };

            if (!string.IsNullOrWhiteSpace(content.VideoTags))
            {
                string[] cleantags = content.VideoTags.Split(',');
                for (int i = 0; i < cleantags.Length; i++)
                {
                    cleantags[i] = cleantags[i].Trim();
                    cleantags[i] = cleantags[i].TrimEnd();
                }
                video.Snippet.Tags = cleantags;
            }
            using FileStream fileStream = new FileStream(content.LocalVideoPath, FileMode.Open, FileAccess.Read);

            var uploadRequest = ytServ.Videos.Insert(video, "snippet,status", fileStream, "video/*");
            IUploadProgress progress = await uploadRequest.UploadAsync();
            if (progress.Status == UploadStatus.Failed)
            {
                string message = $"Upload has failed. See details:\n\n{progress.Exception?.Message}";
                string title = "Error";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            if (uploadRequest.ResponseBody == null)
            {
                string message = $"No response from the API at all. Is null";
                string title = "Error";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(uploadRequest.ResponseBody.Id))
            {
                string message = $"API did not send back video id";
                string title = "Error";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                await SetVideoToLists(uploadRequest.ResponseBody.Id, content.ListsIds);
            }
            catch (Exception ex)
            {
                string message = $"Cannot add video to playlists\n\n{ex.Message}";
                string title = "Error";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return uploadRequest.ResponseBody.Id;
        }

        public async Task ThumbnailUpload(string videoID, string filepath)
        {
            if (string.IsNullOrWhiteSpace(videoID))
            {
                string message = $"Cannot upload thumbnail. Video ID not provided.";
                string title = "Error";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!File.Exists(filepath))
            {
                string message = $"Image no longer exists at defined path.";
                string title = "Error";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            YouTubeService ytServ = await GetYouTubeServiceAsync();
            string whatFileType = "";
            switch (Path.GetExtension(filepath).ToLower())
            {
                case ".png":
                    whatFileType = "image/png";
                    break;
                case ".jpg":
                    whatFileType = "image/jpeg";
                    break;
                case ".jpeg":
                    whatFileType = "image/jpeg";
                    break;
                case ".webp":
                    whatFileType = "image/webp";
                    break;
                default:
                    whatFileType = $"{Path.GetExtension(filepath).ToLower()}";
                    string message = $"Program is unable to understand this filetype:\n\n{whatFileType}\n\nPlease use PNG or JPG.";
                    string title = "Error";
                    var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }
            using FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            var thumbReq = ytServ.Thumbnails.Set(videoID, fileStream, whatFileType);
            IUploadProgress progress = await thumbReq.UploadAsync();
            if (progress.Status == UploadStatus.Failed)
            {
                string message = $"Uploading thumbnail has failed. See details:\n\n{progress.Exception}";
                string title = "Error";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task DiscardYoutubeOAuthToken()
        {
            FileDataStore tokenHere = new FileDataStore("YTA.Auth.Store");
            await tokenHere.ClearAsync();
            string title = "Infos";
            string message = "Token erased, Need to verify credentials with google again.";
            var result2 = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public async Task<List<YTCategory>> GetCategories(string regionCode = "EE")
        {
            YouTubeService ytServ = await GetYouTubeServiceAsync();
            var req = ytServ.VideoCategories.List("snippet");
            req.RegionCode = regionCode;

            List<YTCategory> categories = new List<YTCategory>();
            try
            {

                var res = await req.ExecuteAsync();
                categories = res.Items.Where(x => x.Snippet.Assignable == true)
                    .Select(x => new YTCategory
                    {
                        ID = x.Id,
                        Name = x.Snippet.Title
                    })
                    .OrderBy(x => x.Name)
                    .ToList();
                
            }
            catch (TaskCanceledException tx)
            {
                string title = "Warning";
                string message = $"Task was cancelled, see why\n\n{tx.Message}";
                var result2 = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (Exception ex)
            {
                string title = "Error";
                string message = $"Retreiving categories has failed, see why\n\n{ex.Message}";
                var result2 = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return categories;
        }

        public async Task<List<YTPlaylist>> GetLists()
        {
            YouTubeService ytServ = await GetYouTubeServiceAsync();
            List<YTPlaylist> allLists = new List<YTPlaylist>();
            string nextPageToken = null;
            try
            {
                do
                {
                    var req = ytServ.Playlists.List("snippet,contentDetails,status");
                    req.Mine = true;
                    req.MaxResults = 50;
                    req.PageToken = nextPageToken;

                    var res = await req.ExecuteAsync();

                    if (res.Items != null)
                    {
                        foreach (var item in res.Items)
                        {
                            YTPlaylist thisList = new YTPlaylist();
                            thisList.ListID = item.Id;
                            thisList.ListName = item.Snippet?.Title;
                            thisList.ListDescription = item.Snippet?.Description;
                            thisList.ListVideoCount = item.ContentDetails?.ItemCount;

                            if (item.Snippet?.Thumbnails?.Medium?.Url != null)
                            {
                                thisList.ListThumbLink = item.Snippet?.Thumbnails?.Medium?.Url;
                            }
                            else
                            {
                                thisList.ListThumbLink = item.Snippet?.Thumbnails?.Default__?.Url;
                            }

                            if (item.Status.PrivacyStatus == "private")
                            {
                                thisList.ListPrivacy = PrivacyType.Private;
                            }
                            else if (item.Status.PrivacyStatus == "public")
                            {
                                thisList.ListPrivacy = PrivacyType.Public;
                            }
                            else if (item.Status.PrivacyStatus == "unlisted")
                            {
                                thisList.ListPrivacy = PrivacyType.Unlisted;
                            }
                            else
                            {
                                thisList.ListPrivacy = PrivacyType.Undefined;
                            }
                            allLists.Add(thisList);
                        }
                    }
                    nextPageToken = res.NextPageToken;
                }
                while (!string.IsNullOrEmpty(nextPageToken));
            }
            catch (Exception ex)
            {
                string title = "Error";
                string message = $"Unable to retreive playlists, see why\n\n{ex.Message}";
                var result2 = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return allLists.OrderBy(l => l.ListName).ToList();
        }

        public async Task SetVideoToLists(string videoID, string? listToSetToID) 
        {
            if (string.IsNullOrWhiteSpace(listToSetToID))
            {
                return;
            }
            YouTubeService ytServ = await GetYouTubeServiceAsync();
            string[] listOfListsIDs = listToSetToID.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (var ID in listOfListsIDs)
            {

            PlaylistItem thisAssignees = new PlaylistItem()
            {
                Snippet = new PlaylistItemSnippet
                {
                    PlaylistId = ID,
                    ResourceId = new ResourceId
                    {
                        Kind = "youtube#video",
                        VideoId = videoID
                    }
                }
            };
            var req = ytServ.PlaylistItems.Insert(thisAssignees, "snippet");
            await req.ExecuteAsync();
            }    
        }
    }
}